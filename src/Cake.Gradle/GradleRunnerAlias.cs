using System;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Gradle
{
    /// <summary>
    /// Provides a wrapper around Gradle functionality within a Cake build script.
    /// </summary>
    [CakeAliasCategory("Gradle")]
    public static class GradleRunnerAlias
    {
        /// <summary>
        /// Get a Gradle runner.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="GradleRunner"/> instance.</returns>
        /// <example>
        /// <para>Run 'gradle --version'.</para>
        /// <para>Cake task:</para>
        /// <code>
        /// <![CDATA[
        /// Task("Gradle-Version")
        ///     .Does(() =>
        /// {
        ///     Gradle.WithArguments("--version").Run();
        /// });
        /// ]]>
        /// </code>
        /// <para>Run 'gradle hello' in a specific folder.</para>
        /// <para>Note: if you have a gradle wrapper setup in the specified path, this one will be used.</para>
        /// <para>Cake task:</para>
        /// <code>
        /// <![CDATA[
        /// Task("Gradle-Hello")
        ///     .Does(() =>
        /// {
        ///     Gradle.FromPath("./example").WithTask("hello").Run();
        /// });
        /// ]]>
        /// </code>
        /// <para>Run 'gradle hello' in a specific folder with default log level.</para>
        /// <para>Note: if no log level is set, it is derived from the Cake verbosity (which is set to 'verbose' in build.ps1).</para>
        /// <para>Cake task:</para>
        /// <code>
        /// <![CDATA[
        /// Task("Gradle-Hello-WithDefaultLogLevel")
        ///     .Does(() =>
        /// {
        ///     Gradle.FromPath("./example").WithTask("hello").WithLogLevel(GradleLogLevel.Default).Run();
        /// });
        /// ]]>
        /// </code>
        /// <para>Run 'gradle --offline --build-file build.gradle hello' in a specific folder.</para>
        /// <para>Cake task:</para>
        /// <code>
        /// <![CDATA[
        /// Task("Gradle-Hello-WithArguments")
        ///     .Does(() =>
        /// {
        ///     Gradle.FromPath("./example").WithTask("hello").WithArguments("--offline --build-file build.gradle").Run();
        /// });
        /// ]]>
        /// </code>
        /// </example>
        [CakePropertyAlias]
        public static GradleRunner Gradle(this ICakeContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return new GradleRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, context.Log.Verbosity);
        }
    }
}
