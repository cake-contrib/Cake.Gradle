using System;

using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Gradle.Tests.Fixtures;
using Cake.Testing;

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
        public void Run_WithMultipleTasks_CallsGradleWithTasks()
        {
            var result = _fixture.WithTask("task1", "task2").Run();
            result.Args.ShouldBe("task1 task2");
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

        [Fact]
        public void Run_WithGradleNotPresent_AndNoWorkingDirectory_Throws()
        {
            _fixture.GivenGradleDoesNotExist();

            Action result = () =>
            {
                _fixture.Run();
            };

            result.ShouldThrow<CakeException>().Message.ShouldContain("Could not locate executable.");
        }

        [Fact]
        public void Run_WithNonExistingWorkDir_Throws()
        {
            var nonExitingDir = new DirectoryPath(Guid.NewGuid().ToString("D"));

            Action result = () =>
            {
                _fixture.FromPath(nonExitingDir).Run();
            };

            result.ShouldThrow<System.IO.DirectoryNotFoundException>();
        }

        [Fact]
        public void Run_WithGradleNotPresent_ButGradleWInWorkingDirectory_RunsGradleW()
        {
            var workDir = _fixture.FileSystem.CreateDirectory("/project");
            var expected = workDir.Path.CombineWithFilePath(new FilePath("gradlew")).FullPath;
            _fixture.GivenGradleDoesNotExist();
            _fixture.GivenGradleWExistIn(workDir.Path);

            var result = _fixture.FromPath(workDir.Path).Run();

            result.Path.FullPath.ShouldBe(expected);
        }

        [Fact]
        public void Run_OnWindows_WithGradleNotPresent_ButGradleWInWorkingDirectory_RunsGradleWBat()
        {
            var workDir = _fixture.FileSystem.CreateDirectory("C:\\project");
            var expected = workDir.Path.CombineWithFilePath(new FilePath("gradlew.bat")).FullPath;
            _fixture.GivenAWindowsEnvironment();
            _fixture.GivenGradleDoesNotExist();
            _fixture.GivenGradleWExistIn(workDir.Path);

            var result = _fixture.FromPath(workDir.Path).Run();

            result.Path.FullPath.ShouldBe(expected);
        }

        [Fact]
        public void Run_OnWindows_WithGradleNotPresent_ButGradleExePresent_RunsGradleExe()
        {
            _fixture.GivenAWindowsEnvironment();
            _fixture.GivenGradleDoesNotExist();
            _fixture.GivenGradleExeExistsAsADefaultTool();

            var result = _fixture.Run();

            result.Path.GetFilename().FullPath.ShouldBe("gradle.exe");
        }

        [Theory]
        [InlineData(Verbosity.Diagnostic, "--debug")]
        [InlineData(Verbosity.Minimal, "")]
        [InlineData(Verbosity.Normal, "")]
        [InlineData(Verbosity.Quiet, "--quiet")]
        [InlineData(Verbosity.Verbose, "--info")]
        public void Run_WithCakeVerbosity_Appends_Corresponding_LogLevel(Verbosity verbosity, string expected)
        {
            var fixture = new GradleRunnerFixture(verbosity);

            var result = fixture.Run();

            result.Args.ShouldBe(expected);
        }
    }
}
