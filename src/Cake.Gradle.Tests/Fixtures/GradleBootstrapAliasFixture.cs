using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

using Moq;

namespace Cake.Gradle.Tests.Fixtures
{
    public class GradleBootstrapAliasFixture : GradleBootstrapperFixture
    {
        internal ICakeContext Context { get; }

        public GradleBootstrapAliasFixture()
        {
            var arguments = new Mock<ICakeArguments>();
            var registry = new Mock<IRegistry>();
            var dataService = new Mock<ICakeDataService>();
            var globber = new Mock<IGlobber>();
            var processRunner = new Mock<IProcessRunner>();
            var tools = new Mock<IToolLocator>();


            Context = new CakeContext(
                FileSystem,
                Environment,
                globber.Object,
                Log,
                arguments.Object,
                processRunner.Object,
                registry.Object,
                tools.Object,
                dataService.Object,
                Configuration);
        }
    }
}
