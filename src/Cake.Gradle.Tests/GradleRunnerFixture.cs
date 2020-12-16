using Cake.Testing.Fixtures;

namespace Cake.Gradle.Tests
{
    public class GradleRunnerFixture : ToolFixture<GradleRunnerSettings>
    {
        private GradleLogLevel? _gradleLogLevel;
        private string _task;
        private string _arguments;

        public GradleRunnerFixture() : base("gradle") { }

        protected override void RunTool()
        {
            var tool = new GradleRunner(FileSystem, Environment, ProcessRunner, Tools);
            if (_gradleLogLevel.HasValue) tool.WithLogLevel(_gradleLogLevel.Value);
            if (!string.IsNullOrEmpty(_task)) tool.WithTask(_task);
            if (!string.IsNullOrEmpty(_arguments)) tool.WithArguments(_arguments);
            tool.Run();
        }

        public GradleRunnerFixture WithLogLevel(GradleLogLevel logLevel)
        {
            _gradleLogLevel = logLevel;
            return this;
        }

        public GradleRunnerFixture WithTask(string task)
        {
            _task = task;
            return this;
        }

        public GradleRunnerFixture WithArguments(string arguments)
        {
            _arguments = arguments;
            return this;
        }
    }
}
