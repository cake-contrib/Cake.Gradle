using System;
using System.Threading.Tasks;

using Cake.Core;
using Cake.Core.Configuration;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Gradle.Abstractions;

namespace Cake.Gradle.Bootstrap
{
    /// <summary>
    /// Bootstraps Gradle: Downloads a local version of
    /// gradle into the tools folder.
    /// </summary>
    public class GradleBootstrapper
    {
        private readonly ICakeConfiguration _config;
        private readonly ICakeEnvironment _environment;
        private readonly IFileSystem _fileSystem;
        private readonly ICakeLog _log;

        /// <summary>
        /// Initializes a new instance of the <see cref="GradleBootstrapper"/> class.
        /// </summary>
        /// <param name="config">see <see cref="ICakeConfiguration"/>.</param>
        /// <param name="environment">see <see cref="ICakeEnvironment"/>.</param>
        /// <param name="fileSystem">see <see cref="IFileSystem"/>.</param>
        /// <param name="log">see <see cref="ICakeLog"/>.</param>
        public GradleBootstrapper(
            ICakeConfiguration config,
            ICakeEnvironment environment,
            IFileSystem fileSystem,
            ICakeLog log)
        {
            _config = config;
            _environment = environment;
            _fileSystem = fileSystem;
            _log = log;
            WebAdapter = new WebAdapter(environment);
            ZipAdapter = new ZipAdapter(_fileSystem, _environment, _log);
        }

        /// <summary>
        /// Gets or sets the WebAdapter.
        /// used internally.
        /// </summary>
        internal IWebAdapter WebAdapter { get; set; }

        /// <summary>
        /// Gets or sets the ZipAdapter.
        /// used internally.
        /// </summary>
        internal IZipAdapter ZipAdapter { get; set; }

        /// <summary>
        /// Does the actual bootstrapping.
        /// </summary>
        /// <param name="version">The Version to download.</param>
        internal void Bootstrap(GradleVersionIdentifier version)
        {
            var versionInfo = GetGradleVersion(version)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
            _log.Verbose($"Current gradle version is {versionInfo.Version}");
            var toolsFolder = _fileSystem.GetDirectory(
                _config.GetToolPath(_environment.WorkingDirectory, _environment));

            if (!toolsFolder.Exists)
            {
                toolsFolder.Create();
            }

            var gradleInstallFolder = toolsFolder.Path.Combine($"gradle-{versionInfo.Version}");
            if (_fileSystem.GetDirectory(gradleInstallFolder).Exists)
            {
                _log.Debug("Gradle tool directory exists. Not downloading.");
                return;
            }

            // download gradle
            var gradleZip = DownloadGradle(versionInfo.DownloadUrl, toolsFolder.Path)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            UnzipGradle(gradleZip, toolsFolder.Path);
        }

        /// <summary>
        /// Used internally.
        /// </summary>
        /// <param name="identifier">The Identifier.</param>
        /// <returns>The Download-Information.</returns>
        internal async Task<GradleDownloadInformation> GetGradleVersion(GradleVersionIdentifier identifier)
        {
            var json = await WebAdapter.DownloadString(identifier.Source);
            return identifier.Parse(json);
        }

        /// <summary>
        /// Used internally.
        /// </summary>
        /// <param name="downloadedZip">The downloaded zip.</param>
        /// <param name="toolsFolder">The tools folder.</param>
        internal void UnzipGradle(FilePath downloadedZip, DirectoryPath toolsFolder)
        {
            var dir = _fileSystem.GetDirectory(toolsFolder);
            if (!dir.Exists)
            {
                dir.Create();
            }

            ZipAdapter.Unzip(downloadedZip, toolsFolder);
            _fileSystem.GetFile(downloadedZip).Delete();
        }

        private async Task<FilePath> DownloadGradle(string downloadUrl, DirectoryPath toolsFolder)
        {
            var fileName = new FilePath(
                downloadUrl.Substring(downloadUrl.LastIndexOf("/", StringComparison.Ordinal) + 1));
            var downloadTo = toolsFolder.CombineWithFilePath(fileName);

            // downloading gradle package
            await WebAdapter.DownloadFile(new Uri(downloadUrl), downloadTo, progress =>
            {
                _log.Verbose("Downloading gradle: {0}%", progress);
            });

            _log.Verbose("Download complete, saved to: {0}", downloadTo.FullPath);
            return downloadTo;
        }
    }
}
