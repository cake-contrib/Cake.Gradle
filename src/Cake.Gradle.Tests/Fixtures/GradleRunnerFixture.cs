using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;
using Cake.Testing;
using Cake.Testing.Fixtures;

namespace Cake.Gradle.Tests.Fixtures
{
    public class GradleRunnerFixture : ToolFixture<GradleRunnerSettings>
    {
        private GradleRunner _tool;

        public GradleRunnerFixture() :
            this(Verbosity.Normal) { }

        public GradleRunnerFixture(Verbosity verbosity) : base("gradle")
        {
            _tool = new GradleRunner(FileSystem, Environment, ProcessRunner, Tools, verbosity);
        }

        protected override void RunTool()
        {
            _tool.Run();
        }

        // ReSharper disable once UnusedMethodReturnValue.Global
        public GradleRunnerFixture WithLogLevel(GradleLogLevel logLevel)
        {
            _tool = _tool.WithLogLevel(logLevel);
            return this;
        }

        public GradleRunnerFixture WithTask(string task)
        {
            _tool = _tool.WithTask(task);
            return this;
        }

        public GradleRunnerFixture WithTask(params string[] tasks)
        {
            _tool = _tool.WithTask(tasks);
            return this;
        }

        public GradleRunnerFixture WithArguments(string arguments)
        {
            _tool = _tool.WithArguments(arguments);
            return this;
        }

        public GradleRunnerFixture FromPath(DirectoryPath path)
        {
            _tool = _tool.FromPath(path);
            return this;
        }

        /// <summary>
        /// Ensures that the environment is a windows environment.
        /// <para>WARNING: As calling this method will remove the existing Environment
        /// and replace it with a new one, this method should be called first, before any
        /// other modifications are made.</para>
        /// </summary>
        public void GivenAWindowsEnvironment()
        {
            Environment = FakeEnvironment.CreateWindowsEnvironment();

            // re-create everything that had a reference to the old environment
            // see https://github.com/cake-build/cake/blob/main/src/Cake.Testing/Fixtures/ToolFixture%602.cs#L78
            FileSystem = new FakeFileSystem(Environment);
            Globber = new Globber(FileSystem, Environment);
            Tools = new ToolLocator(Environment, new ToolRepository(Environment), new ToolResolutionStrategy(FileSystem, Environment, Globber, Configuration, new NullLog()));

            // re-create the tool (as it, too, had a reference to the environment
            _tool = new GradleRunner(FileSystem, Environment, ProcessRunner, Tools);
        }
    }
}
