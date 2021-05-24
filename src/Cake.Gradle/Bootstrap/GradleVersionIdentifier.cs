// unset

using System;
using System.Collections.Generic;
using System.Linq;

using LitJson;

namespace Cake.Gradle.Bootstrap
{
    /// <summary>
    /// Wraps the possibilities of https://services.gradle.org/versions/.
    /// </summary>
    public abstract class GradleVersionIdentifier
    {
        /// <summary>
        /// Gets the identifier for current version.
        /// </summary>
        public static GradleVersionIdentifier Current { get; } = new CurrentGradleVersion();

        /// <summary>
        /// Gets the identifier for nightly version.
        /// </summary>
        public static GradleVersionIdentifier Nightly { get; } = new NightlyGradleVersion();

        /// <summary>
        /// gets the Uri to download version Information from.
        /// </summary>
        internal abstract Uri Source { get; }

        /// <summary>
        /// Gets the identifier for a specific version.
        /// </summary>
        /// <param name="version">the version to find.</param>
        /// <returns>The VersionIdentifier.</returns>
        public static GradleVersionIdentifier ForVersion(string version)
        {
            return new FindVersionStringGradleVersion(version);
        }

        /// <summary>
        /// Parses the downloaded version information and returns the required <see cref="GradleDownloadInformation"/>.
        /// </summary>
        /// <param name="json">The downloaded JSON, from <see cref="Source"/>.</param>
        /// <returns>The parsed <see cref="GradleDownloadInformation"/>.</returns>
        internal abstract GradleDownloadInformation Parse(string json);

        private abstract class NamedVersionBasedGradleVersion : GradleVersionIdentifier
        {
            internal override GradleDownloadInformation Parse(string json)
            {
                var mapped = JsonMapper.ToObject(json);
                return new GradleDownloadInformation
                {
                    Version = (string)mapped["version"],
                    DownloadUrl = (string)mapped["downloadUrl"],
                };
            }
        }

        private class CurrentGradleVersion : NamedVersionBasedGradleVersion
        {
            internal override Uri Source { get; } = new Uri("https://services.gradle.org/versions/current");
        }

        private class NightlyGradleVersion : NamedVersionBasedGradleVersion
        {
            internal override Uri Source { get; } = new Uri("https://services.gradle.org/versions/nightly");
        }

        private abstract class QueryAllBasedGradleVersion : GradleVersionIdentifier
        {
            internal override Uri Source { get; } = new Uri("https://services.gradle.org/versions/all");

            internal override GradleDownloadInformation Parse(string json)
            {
                var mapped = JsonMapper.ToObject<JsonData[]>(json);
                var all = mapped.Select(x => new GradleDownloadInformation
                {
                    Version = (string)x["version"],
                    DownloadUrl = (string)x["downloadUrl"],
                });
                return Query(all);
            }

            protected abstract GradleDownloadInformation Query(IEnumerable<GradleDownloadInformation> all);
        }

        private class FindVersionStringGradleVersion : QueryAllBasedGradleVersion
        {
            private readonly string _version;

            public FindVersionStringGradleVersion(string version)
            {
                _version = version;
            }

            protected override GradleDownloadInformation Query(IEnumerable<GradleDownloadInformation> all)
            {
                return all.First(x => x.Version == _version);
            }
        }
    }
}
