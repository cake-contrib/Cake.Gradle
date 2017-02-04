using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Gradle
{
    /// <summary>
    /// A wrapper around Gradle functionality within a Cake build script.
    /// </summary>
    public class GradleRunner : Tool<GradleRunnerSettings>
    {
        private const string TasksSeparator = " ";

        private readonly Verbosity _cakeVerbosityLevel;
        private GradleLogLevel? _logLevel;
        private string _tasks = string.Empty;
        private string _arguments = string.Empty;
        private DirectoryPath _workingDirectoryPath;
        private readonly IFileSystem _fileSystem;
        private readonly ICakeEnvironment _environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="GradleRunner" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system</param>
        /// <param name="environment">The environment</param>
        /// <param name="processRunner">The process runner</param>
        /// <param name="toolLocator">The tool locator</param>
        /// <param name="cakeVerbosityLevel">Specifies the current Cake verbosity level</param>
        public GradleRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner,
            IToolLocator toolLocator, Verbosity cakeVerbosityLevel = Verbosity.Normal)
            : base(fileSystem, environment, processRunner, toolLocator)
        {
            _cakeVerbosityLevel = cakeVerbosityLevel;
            _fileSystem = fileSystem;
            _environment = environment;
        }

        private string WrapperExecutableName
            => _environment.Platform.Family == PlatformFamily.Windows ? "gradlew.bat" : "gradlew";

        private string PlainExecutableName
            => _environment.Platform.Family == PlatformFamily.Windows ? "gradle.bat" : "gradle";

        private bool IsGradleWrapperUsed
        {
            get
            {
                if (_workingDirectoryPath == null) return false;

                var wrapperExecutable = _workingDirectoryPath.GetFilePath(PlainExecutableName);
                return File.Exists(wrapperExecutable.FullPath);
            }
        }

        /// <summary>
        /// Gets this tools name.
        /// </summary>
        protected override string GetToolName()
        {
            return "Gradle Runner";
        }

        /// <summary>
        /// Gets the name of the tool executable, prefers wrapper over plain Gradle.
        /// </summary>
        /// <returns>The tool executable name</returns>
        protected override IEnumerable<string> GetToolExecutableNames()
        {
            if (IsGradleWrapperUsed)
            {
                yield return WrapperExecutableName;
            }
            else
            {
                yield return PlainExecutableName;
            }
        }

        /// <summary>
        /// Gets the path to the Gradle wrapper, if available.
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        protected override IEnumerable<FilePath> GetAlternativeToolPaths(GradleRunnerSettings settings)
        {
            return GetToolExecutableNames().Select(e => GetWorkingDirectory(settings).GetFilePath(e));
/*
            if (!IsGradleWrapperAvailable) return base.GetAlternativeToolPaths(settings);

            var wrapper = GetWorkingDirectory(settings).GetFilePath(GradleWrapperExecutableName);
            return new[] {wrapper};*/
        }

        /// <summary>
        /// Sets the gradle logging level
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public GradleRunner WithLogLevel(GradleLogLevel logLevel)
        {
            _logLevel = logLevel;
            return this;
        }

        /// <summary>
        /// Specifies a Gradle task to be run.
        /// </summary>
        /// <param name="task">task name</param>
        public GradleRunner WithTask(string task)
        {
            _tasks += task + TasksSeparator;
            return this;
        }

        /// <summary>
        /// Specifies Gradle tasks to be run.
        /// </summary>
        /// <param name="tasks">task names</param>
        public GradleRunner WithTask(params string[] tasks)
        {
            _tasks += string.Join(TasksSeparator, tasks) + TasksSeparator;
            return this;
        }

        /// <summary>
        /// Specifies arguments to be passed to the Gradle executable.
        /// </summary>
        /// <param name="arguments">arguments</param>
        public GradleRunner WithArguments(string arguments)
        {
            _arguments = arguments;
            return this;
        }

        /// <summary>
        /// Specifies the path the Gradle project resides in.
        /// </summary>
        /// <param name="path">The path to the Gradle project</param>
        /// <returns></returns>
        public GradleRunner FromPath(DirectoryPath path)
        {
            _workingDirectoryPath = path;
            return this;
        }

        /// <summary>
        /// Starts the Gradle run.
        /// </summary>
        public void Run()
        {
            var settings = new GradleRunnerSettings();
            var args = GetGradleArguments();
            Run(settings, args);
        }

        private ProcessArgumentBuilder GetGradleArguments()
        {
            // USAGE: gradle [option...] [task...]
            var args = new ProcessArgumentBuilder();

            AppendLogLevel(args);

            if (!string.IsNullOrWhiteSpace(_arguments))
            {
                args.Append(_arguments.Trim());
            }

            if (!string.IsNullOrWhiteSpace(_tasks))
            {
                args.Append(_tasks.Trim());
            }

            return args;
        }

        private void AppendLogLevel(ProcessArgumentBuilder args)
        {
            if (!_logLevel.HasValue)
            {
                switch (_cakeVerbosityLevel)
                {
                    case Verbosity.Quiet:
                        _logLevel = GradleLogLevel.Quiet;
                        break;
                    case Verbosity.Minimal:
                        _logLevel = GradleLogLevel.Default;
                        break;
                    case Verbosity.Normal:
                        _logLevel = GradleLogLevel.Default;
                        break;
                    case Verbosity.Verbose:
                        _logLevel = GradleLogLevel.Info;
                        break;
                    case Verbosity.Diagnostic:
                        _logLevel = GradleLogLevel.Debug;
                        break;
                    default:
                        throw new ArgumentException("Unsupported Cake Verbosity: " + _cakeVerbosityLevel);
                }
            }

            switch (_logLevel)
            {
                case GradleLogLevel.Quiet:
                    args.Append("--quiet");
                    break;
                case GradleLogLevel.Debug:
                    args.Append("--debug");
                    break;
                case GradleLogLevel.Info:
                    args.Append("--info");
                    break;
                case GradleLogLevel.Default:
                    // no logging option
                    break;
                default:
                    throw new ArgumentException("Unsupported gradle log level: " + _logLevel);
            }
        }

        /// <summary>
        /// Gets the working directory.
        /// Uses the directory specified with 'FromPath()'.
        /// Otherwise defaults to the currently set working directory.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>The working directory for the tool.</returns>
        protected override DirectoryPath GetWorkingDirectory(GradleRunnerSettings settings)
        {
            if (_workingDirectoryPath == null)
            {
                return base.GetWorkingDirectory(settings);
            }

            if (!_fileSystem.Exist(_workingDirectoryPath))
            {
                throw new DirectoryNotFoundException(
                    $"Working directory path not found [{_workingDirectoryPath.FullPath}]");
            }

            return _workingDirectoryPath;
        }
    }
}