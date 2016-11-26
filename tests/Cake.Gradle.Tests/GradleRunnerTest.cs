using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void No_Install_Settings_Should_Use_Correct_Argument_Provided_In_NpmInstallSettings()
        {
            var result = _fixture.Run();
            result.Args.Length.ShouldBe(0);
        }

        [Theory]
        [InlineData(GradleLogLevel.Info, "--info")]
        [InlineData(GradleLogLevel.Quiet, "--quiet")]
        [InlineData(GradleLogLevel.Debug, "--debug")]
        [InlineData(GradleLogLevel.LifecycleAndHigher, "")]
        public void Custom_LogLevel_Should_Be_Applied(GradleLogLevel logLevel, string args)
        {
            _fixture.WithLogLevel(logLevel);

            var result = _fixture.Run();

            result.Args.ShouldBe($"{args}");
        }
    }
}
