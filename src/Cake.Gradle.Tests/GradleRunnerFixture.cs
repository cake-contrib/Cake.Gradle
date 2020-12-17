using Cake.Core.IO;
using Cake.Testing.Fixtures;

namespace Cake.Gradle.Tests
{
    public class GradleRunnerFixture : ToolFixture<GradleRunnerSettings>
    {
        private GradleRunner tool;

        public GradleRunnerFixture() : base("gradle")
        {
            tool = new GradleRunner(FileSystem, Environment, ProcessRunner, Tools);
        }

        protected override void RunTool()
        {
            tool.Run();
        }

        // ReSharper disable once UnusedMethodReturnValue.Global
        public GradleRunnerFixture WithLogLevel(GradleLogLevel logLevel)
        {
            tool = tool.WithLogLevel(logLevel);
            return this;
        }

        public GradleRunnerFixture WithTask(string task)
        {
            tool = tool.WithTask(task);
            return this;
        }

        public GradleRunnerFixture WithArguments(string arguments)
        {
            tool = tool.WithArguments(arguments);
            return this;
        }

        public GradleRunnerFixture FromPath(DirectoryPath path)
        {
            tool = tool.FromPath(path);
            return this;
        }
    }
}
