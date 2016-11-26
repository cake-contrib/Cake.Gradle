using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Testing.Fixtures;

namespace Cake.Gradle.Tests
{
    public class GradleRunnerFixture : ToolFixture<GradleRunnerSettings>
    {
        private Verbosity _level = Verbosity.Normal;
        private GradleLogLevel? _gradleLogLevel;

        public GradleRunnerFixture() : base("gradle") { }

        protected override void RunTool()
        {
            var tool = new GradleRunner(FileSystem, Environment, ProcessRunner, Tools, _level);
            if (_gradleLogLevel.HasValue) tool.WithLogLevel(_gradleLogLevel.Value);
            tool.Run();
        }

        public GradleRunnerFixture WithLogLevel(GradleLogLevel logLevel)
        {
            _gradleLogLevel = logLevel;
            return this;
        }
    }
}
