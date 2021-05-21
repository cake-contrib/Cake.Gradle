using System;

using Cake.Core;
using Cake.Core.Annotations;
using Cake.Gradle.Bootstrap;

using JetBrains.Annotations;

namespace Cake.Gradle
{
    /// <summary>
    /// Provides a way to bootstrap a local gradle installation.
    /// </summary>
    /// <example>
    /// <para>get the current gradle version.</para>
    /// <code>
    /// <![CDATA[
    /// Task("Bootstrap-Gradle")
    ///     .Does(() =>
    /// {
    ///     BootstrapGradle("7.0.2");
    /// });
    /// ]]>
    /// </code>
    /// </example>
    [CakeAliasCategory("Gradle")]
    [PublicAPI]
    public static class GradleBootstrapAlias
    {
        /// <summary>
        /// Bootstrap a specific version of gradle.
        /// <para>
        /// The version must be a complete match and exist in the gradle list of versions.
        /// I.e. The <c>json</c> at https://services.gradle.org/versions/all
        /// must contain an element whose version matches exactly the given version.
        /// </para>
        /// </summary>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// Task("Bootstrap-Gradle-7")
        ///     .Does(() =>
        /// {
        ///     BootstrapGradle("7.0.2");
        /// });
        /// ]]>
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="version">The version to get.</param>
        [CakeMethodAlias]
        public static void BootstrapGradle(this ICakeContext context, string version)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (version == null)
            {
                throw new ArgumentNullException(nameof(version));
            }

            var versionIdentifier = GradleVersionIdentifier.ForVersion(version);
            BootstrapGradle(context, versionIdentifier);
        }

        /// <summary>
        /// Bootstrap the current gradle version into the tools folder.
        /// <para>
        /// Be aware that this is always the current version, as found in
        /// https://services.gradle.org/versions/current
        /// So the versions of gradle might change between usages.
        /// </para>
        /// </summary>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// Task("Bootstrap-Current-Gradle")
        ///     .Does(() =>
        /// {
        ///     BootstrapCurrentGradle();
        /// });
        /// ]]>
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        [CakeMethodAlias]
        public static void BootstrapCurrentGradle(this ICakeContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            BootstrapGradle(context, GradleVersionIdentifier.Current);
        }

        /// <summary>
        /// Bootstrap the nightly gradle version into the tools folder.
        /// <para>
        /// Be aware that this is always the nightly version, as found in
        /// https://services.gradle.org/versions/nightly
        /// So the versions of gradle might change between usages.
        /// Also, as this is the nightly version, it is entirely possible
        /// that this version might not be completely stable.
        /// </para>
        /// </summary>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// Task("Bootstrap-Nightly-Gradle")
        ///     .Does(() =>
        /// {
        ///     BootstrapNightlyGradle();
        /// });
        /// ]]>
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        [CakeMethodAlias]
        public static void BootstrapNightlyGradle(this ICakeContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            BootstrapGradle(context, GradleVersionIdentifier.Current);
        }

        private static void BootstrapGradle(ICakeContext context, GradleVersionIdentifier version)
        {
            new GradleBootstrapper(context.Configuration, context.Environment, context.FileSystem, context.Log)
                .Bootstrap(version);
        }
    }
}
