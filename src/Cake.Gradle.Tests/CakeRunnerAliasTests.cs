using System;
using AutoFixture;
using AutoFixture.AutoMoq;
using Cake.Core;
using Shouldly;
using Xunit;

namespace Cake.Gradle.Tests
{
    public class CakeRunnerAliasTests
    {
        [Fact]
        public void Gradle_WithContextSetToNull_Throws()
        {
            Action action = () =>
            {
                ((CakeContext)null).Gradle();
            };

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Gradle_WithoutArgs_ReturnsTheRunner()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var context = fixture.Create<ICakeContext>();

            var actual = context.Gradle();

            actual.ShouldNotBeNull();
            actual.ShouldBeAssignableTo<GradleRunner>();
        }
    }
}
