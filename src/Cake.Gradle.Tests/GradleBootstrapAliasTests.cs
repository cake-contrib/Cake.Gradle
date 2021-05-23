using System;

using Cake.Core;
using Cake.Gradle.Tests.Fixtures;

using Shouldly;
using Xunit;

namespace Cake.Gradle.Tests
{
    public class GradleBootstrapAliasTests
    {
        private readonly GradleBootstrapAliasFixture _fixture;

        public GradleBootstrapAliasTests()
        {
            _fixture = new GradleBootstrapAliasFixture();
        }

        [Fact]
        public void BootstrapCurrent_WithContextSetToNull_Throws()
        {
            Action action = () =>
            {
                ((CakeContext)null).BootstrapCurrentGradle();
            };

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void BootstrapNightly_WithContextSetToNull_Throws()
        {
            Action action = () =>
            {
                ((CakeContext)null).BootstrapNightlyGradle();
            };

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void BootstrapVersion_WithContextSetToNull_Throws()
        {
            Action action = () =>
            {
                ((CakeContext)null).BootstrapGradle("7.0.2");
            };

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void BootstrapVersion_WithoutArgs_Throws()
        {
            Action action = () =>
            {
                _fixture.Context.BootstrapGradle(null);
            };

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void BootstrapCurrent_Produces_Throws()
        {
            Action action = () =>
            {
                _fixture.Context.BootstrapGradle(null);
            };

            action.ShouldThrow<ArgumentNullException>();
        }
    }
}
