using System;

using Cake.Core.IO;
using Cake.Core.Tooling;
using Cake.Testing;
using Cake.Testing.Fixtures;

namespace Cake.Gradle.Tests
{
    /// <summary>
    /// Contains extension methods for <see cref="ToolFixture{TToolSettings}"/>.
    /// </summary>
    public static class ToolFixtureExtensions
    {
        /// <summary>
        /// Ensures that gradle does not exist under the tool settings tool path.
        /// </summary>
        /// <typeparam name="TToolSettings">The type of the tool settings.</typeparam>
        /// <typeparam name="TFixtureResult">The type of the fixture result.</typeparam>
        /// <param name="fixture">The fixture.</param>
        public static void GivenGradleDoesNotExist<TToolSettings, TFixtureResult>(
            this ToolFixture<TToolSettings, TFixtureResult> fixture)
            where TToolSettings : ToolSettings, new()
            where TFixtureResult : ToolFixtureResult
        {
            if (fixture == null)
            {
                throw new ArgumentNullException(nameof(fixture));
            }

            var file = fixture.FileSystem.GetFile(fixture.DefaultToolPath);
            if (file.Exists)
            {
                file.Delete();
            }
        }

        /// <summary>
        /// Ensures that gradleW does exist in the working directory
        /// </summary>
        /// <typeparam name="TToolSettings">The type of the tool settings.</typeparam>
        /// <typeparam name="TFixtureResult">The type of the fixture result.</typeparam>
        /// <param name="fixture">The fixture.</param>
        /// <param name="workDir">The directory to create the tool in.</param>
        public static void GivenGradleWExistIn<TToolSettings, TFixtureResult>(
            this ToolFixture<TToolSettings, TFixtureResult> fixture,
            DirectoryPath workDir)
            where TToolSettings : ToolSettings, new()
            where TFixtureResult : ToolFixtureResult
        {
            if (fixture == null)
            {
                throw new ArgumentNullException(nameof(fixture));
            }

            if (workDir == null)
            {
                throw new ArgumentNullException(nameof(workDir));
            }


            foreach (string tool in new[]{ "gradlew", "gradlew.bat" })
            {
                var file = fixture.FileSystem.GetFile(
                    workDir.CombineWithFilePath(new FilePath(tool)));
                if (!file.Exists)
                {
                    fixture.FileSystem.CreateFile(file.Path);
                }
            }
        }
    }
}
