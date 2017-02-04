using Shouldly;
using Xunit;

namespace Cake.Gradle.Tests
{
    public class GradleRunnerTest
    {
        private readonly GradleRunnerFixture _fixture;

        public GradleRunnerTest()
        {
            _fixture = new GradleRunnerFixture();
        }

        [Fact]
        public void Run_NoArguments_CallsGradleWithoutArguments()
        {
            var result = _fixture.Run();
            result.Args.Length.ShouldBe(0);
        }

        [Fact]
        public void Run_WithTask_CallsGradleWithTask()
        {
            var result = _fixture.WithTask("task").Run();
            result.Args.ShouldBe("task");
        }

        [Fact]
        public void Run_WithArguments_CallsGradleWithArguments()
        {
            var result = _fixture.WithArguments("--argument").Run();
            result.Args.ShouldBe("--argument");
        }

        [Theory]
        [InlineData(GradleLogLevel.Info, "--info")]
        [InlineData(GradleLogLevel.Quiet, "--quiet")]
        [InlineData(GradleLogLevel.Debug, "--debug")]
        [InlineData(GradleLogLevel.Default, "")]
        public void Run_WithLogLevel_CallsGradleWithCustomLogLevel(GradleLogLevel logLevel, string args)
        {
            _fixture.WithLogLevel(logLevel);

            var result = _fixture.Run();

            result.Args.ShouldBe($"{args}");
        }

        [Fact]
        public void Run_WithoutLogLevel_CallsGradleWithDefaultLogLevel()
        {
            var result = _fixture.Run();
            result.Args.ShouldNotContain("--quiet");
            result.Args.ShouldNotContain("--info");
            result.Args.ShouldNotContain("--debug");
        }
    }
}
