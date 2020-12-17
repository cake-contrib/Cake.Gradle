using Cake.Core.IO;
using Cake.Testing.Fixtures;

namespace Cake.Gradle.Tests
{
    public class GradleRunnerFixture : ToolFixture<GradleRunnerSettings>
    {
        private GradleRunner _tool;

        public GradleRunnerFixture() : base("gradle")
        {
            _tool = new GradleRunner(FileSystem, Environment, ProcessRunner, Tools);
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
    }
}
