using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Gradle
{
    public class GradleRunner : Tool<GradleRunnerSettings>
    {
        /// <summary>
        /// Get a Gradle runner
        /// </summary>
        public GradleRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools, Verbosity logVerbosity) : base(fileSystem, environment, processRunner, tools)
        {
            throw new NotImplementedException();
        }

        protected override string GetToolName()
        {
            return "Gradle Runner";
        }

        /// <summary>
        /// Gets the name of the tool executable
        /// </summary>
        /// <returns>The tool executable name</returns>
        protected override IEnumerable<string> GetToolExecutableNames()
        {
            yield return "gradlew";
            yield return "gradlew.bat";
        }
    }
}