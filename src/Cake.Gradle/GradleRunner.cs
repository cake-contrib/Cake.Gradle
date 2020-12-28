using System;
using System.Collections.Generic;
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

        private readonly List<string> _arguments = new List<string>();
        private readonly List<GradleProperty> _properties = new List<GradleProperty>();

        private readonly Verbosity _cakeVerbosityLevel;
        private readonly IFileSystem _fileSystem;
        private readonly ICakeEnvironment _environment;
        private GradleLogLevel? _logLevel;
        private string _tasks = string.Empty;
        private DirectoryPath _workingDirectoryPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="GradleRunner" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="toolLocator">The tool locator.</param>
        /// <param name="cakeVerbosityLevel">Specifies the current Cake verbosity level.</param>
        public GradleRunner(
            IFileSystem fileSystem,
            ICakeEnvironment environment,
            IProcessRunner processRunner,
            IToolLocator toolLocator,
            Verbosity cakeVerbosityLevel = Verbosity.Normal)
            : base(fileSystem, environment, processRunner, toolLocator)
        {
            _cakeVerbosityLevel = cakeVerbosityLevel;
            _fileSystem = fileSystem;
            _environment = environment;
        }

        private string[] GradleWrapperExecutable
            => _environment.Platform.Family == PlatformFamily.Windows ? new[] { "gradlew.bat" } : new[] { "gradlew" };

        private string[] GradlePlainExecutable
            => _environment.Platform.Family == PlatformFamily.Windows ? new[] { "gradle.bat", "gradle.exe" } : new[] { "gradle" };

        private bool IsGradleWrapperUsed
        {
            get
            {
                if (_workingDirectoryPath == null)
                {
                    return false;
                }

                var possibleWrappers = GradleWrapperExecutable.Select(x => _workingDirectoryPath.GetFilePath(x));
                return possibleWrappers.Any(_fileSystem.Exist);
            }
        }

        /// <summary>
        /// Sets the gradle logging level.
        /// </summary>
        /// <param name="logLevel">The logging level.</param>
        /// <returns>The <c>GradleRunner</c> for fluent re-use.</returns>
        public GradleRunner WithLogLevel(GradleLogLevel logLevel)
        {
            _logLevel = logLevel;
            return this;
        }

        /// <summary>
        /// Specifies a Gradle task to be run.
        /// </summary>
        /// <param name="task">task name.</param>
        /// <returns>The <c>GradleRunner</c> for fluent re-use.</returns>
        public GradleRunner WithTask(string task)
        {
            _tasks += task + TasksSeparator;
            return this;
        }

        /// <summary>
        /// Specifies Gradle tasks to be run.
        /// </summary>
        /// <param name="tasks">task names.</param>
        /// <returns>The <c>GradleRunner</c> for fluent re-use.</returns>
        public GradleRunner WithTask(params string[] tasks)
        {
            _tasks += string.Join(TasksSeparator, tasks) + TasksSeparator;
            return this;
        }

        /// <summary>
        /// Specifies arguments to be passed to the Gradle executable.
        /// </summary>
        /// <param name="arguments">arguments.</param>
        /// <returns>The <c>GradleRunner</c> for fluent re-use.</returns>
        public GradleRunner WithArguments(params string[] arguments)
        {
            _arguments.AddRange(arguments);
            return this;
        }

        /// <summary>
        /// Adds a project property to the arguments.
        /// </summary>
        /// <param name="key">The key to set.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="secretValue">Set to <c>true</c>, if value is secret and should not show in logs.</param>
        /// <returns>The <c>GradleRunner</c> for fluent re-use.</returns>
        public GradleRunner WithProjectProperty(string key, string value, bool secretValue = false)
        {
            _properties.Add(new GradleProperty
            {
                PropertyType = GradleProperty.Type.Project,
                Key = key,
                Value = value,
                SecretValue = secretValue,
            });

            return this;
        }

        /// <summary>
        /// Adds a system-property to the arguments.
        /// </summary>
        /// <param name="key">The key to set.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="secretValue">Set to <c>true</c>, if value is secret and should not show in logs.</param>
        /// <returns>The <c>GradleRunner</c> for fluent re-use.</returns>
        public GradleRunner WithSystemProperty(string key, string value, bool secretValue = false)
        {
            _properties.Add(new GradleProperty
            {
                PropertyType = GradleProperty.Type.System,
                Key = key,
                Value = value,
                SecretValue = secretValue,
            });

            return this;
        }

        /// <summary>
        /// Specifies the path the Gradle project resides in.
        /// </summary>
        /// <param name="path">The path to the Gradle project.</param>
        /// <returns>The <c>GradleRunner</c> for fluent re-use.</returns>
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

        /// <summary>
        /// Gets this tools name.
        /// </summary>
        /// <returns>The tools name.</returns>
        protected override string GetToolName()
        {
            return "Gradle Runner";
        }

        /// <summary>
        /// Gets the name of the tool executable, prefers wrapper over plain Gradle.
        /// </summary>
        /// <returns>The tool executable name.</returns>
        protected override IEnumerable<string> GetToolExecutableNames()
        {
            return IsGradleWrapperUsed ? GradleWrapperExecutable : GradlePlainExecutable;
        }

        /// <summary>
        /// Gets the paths to the Gradle wrapper, if available.
        /// </summary>
        /// <param name="settings">The <see cref="GradleRunnerSettings"/>.</param>
        /// <returns>Paths to the Gradle wrapper.</returns>
        protected override IEnumerable<FilePath> GetAlternativeToolPaths(GradleRunnerSettings settings)
        {
            if (!IsGradleWrapperUsed)
            {
                return base.GetAlternativeToolPaths(settings);
            }

            var workDir = GetWorkingDirectory(settings);
            return GradleWrapperExecutable.Select(x => workDir.GetFilePath(new FilePath(x)));
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
                throw new System.IO.DirectoryNotFoundException(
                    $"Working directory path not found [{_workingDirectoryPath.FullPath}]");
            }

            return _workingDirectoryPath;
        }

        private ProcessArgumentBuilder GetGradleArguments()
        {
            // USAGE: gradle [option...] [task...]
            var args = new ProcessArgumentBuilder();

            AppendLogLevel(args);

            foreach (var property in _properties)
            {
                var prefix = property.PropertyType == GradleProperty.Type.System ? "-D" : "-P";
                var key = prefix + property.Key;

                if (property.SecretValue)
                {
                    args.AppendSwitchQuotedSecret(key, "=", property.Value);
                }
                else
                {
                    args.AppendSwitchQuoted(key, "=", property.Value);
                }
            }

            foreach (var arg in _arguments)
            {
                args.Append(arg.Trim());
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
    }
}
