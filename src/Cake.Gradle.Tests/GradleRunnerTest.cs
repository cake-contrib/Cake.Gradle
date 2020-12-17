using System;

using Cake.Core;
using Cake.Core.IO;
using Cake.Testing;

using Shouldly;
using Xunit;

namespace Cake.Gradle.Tests
{
    public class GradleRunnerTest
    {
        private readonly GradleRunnerFixture fixture;

        public GradleRunnerTest()
        {
            fixture = new GradleRunnerFixture();
        }

        [Fact]
        public void Run_NoArguments_CallsGradleWithoutArguments()
        {
            var result = fixture.Run();
            result.Args.Length.ShouldBe(0);
        }

        [Fact]
        public void Run_WithTask_CallsGradleWithTask()
        {
            var result = fixture.WithTask("task").Run();
            result.Args.ShouldBe("task");
        }

        [Fact]
        public void Run_WithArguments_CallsGradleWithArguments()
        {
            var result = fixture.WithArguments("--argument").Run();
            result.Args.ShouldBe("--argument");
        }

        [Theory]
        [InlineData(GradleLogLevel.Info, "--info")]
        [InlineData(GradleLogLevel.Quiet, "--quiet")]
        [InlineData(GradleLogLevel.Debug, "--debug")]
        [InlineData(GradleLogLevel.Default, "")]
        public void Run_WithLogLevel_CallsGradleWithCustomLogLevel(GradleLogLevel logLevel, string args)
        {
            fixture.WithLogLevel(logLevel);

            var result = fixture.Run();

            result.Args.ShouldBe($"{args}");
        }

        [Fact]
        public void Run_WithoutLogLevel_CallsGradleWithDefaultLogLevel()
        {
            var result = fixture.Run();
            result.Args.ShouldNotContain("--quiet");
            result.Args.ShouldNotContain("--info");
            result.Args.ShouldNotContain("--debug");
        }

        [Fact]
        public void Run_WithGradleNotPresent_AndNoWorkingDirectory_Throws()
        {
            fixture.GivenGradleDoesNotExist();

            Action result = () =>
            {
                fixture.Run();
            };

            result.ShouldThrow<CakeException>().Message.ShouldContain("Could not locate executable.");
        }

        [Fact]
        public void Run_WithNonExistingWorkDir_Throws()
        {
            var nonExitingDir = new DirectoryPath(Guid.NewGuid().ToString("D"));

            Action result = () =>
            {
                fixture.FromPath(nonExitingDir).Run();
            };

            result.ShouldThrow<System.IO.DirectoryNotFoundException>();
        }

        [Fact]
        public void Run_WithGradleNotPresent_ButGradleWInWorkingDirectory_RunsGradleW()
        {
            var workDir = fixture.FileSystem.CreateDirectory("/project");
            var expected = workDir.Path.CombineWithFilePath(new FilePath("gradlew")).FullPath; // or gradleW.bat
            fixture.GivenGradleDoesNotExist();
            fixture.GivenGradleWExistIn(workDir.Path);

            var result = fixture.FromPath(workDir.Path).Run();

            result.Path.FullPath.ShouldStartWith(expected); // ShouldStartWith() - to capture gradleW.bat, too.
        }
    }
}
