// unset

namespace Cake.Gradle.Bootstrap
{
    /// <summary>
    /// Download information for gradle.
    /// <para>
    /// Compare to <see href="https://services.gradle.org/versions/"/>.
    /// </para>
    /// </summary>
    public class GradleDownloadInformation
    {
        /// <summary>
        /// Gets or sets the Version of gradle.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the download url of gradle.
        /// </summary>
        public string DownloadUrl { get; set; }
    }
}
