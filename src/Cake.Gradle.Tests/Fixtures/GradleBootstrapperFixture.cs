using System;
using System.IO;
using System.Threading.Tasks;

using Cake.Core;
using Cake.Core.IO;
using Cake.Gradle.Abstractions;
using Cake.Gradle.Bootstrap;
using Cake.Testing;

using Moq;

namespace Cake.Gradle.Tests.Fixtures
{
    public class GradleBootstrapperFixture
    {
        private readonly GradleBootstrapper _bootstrapper;

        protected readonly FakeEnvironment Environment;
        protected readonly FakeConfiguration Configuration;
        protected readonly FakeFileSystem FileSystem;
        protected readonly FakeLog Log;

        private const string CurrentJson = @"{
            ""version"" : ""7.0.2"",
            ""buildTime"" : ""20210514120231+0000"",
            ""current"" : true,
            ""snapshot"" : false,
            ""nightly"" : false,
            ""releaseNightly"" : false,
            ""activeRc"" : false,
            ""rcFor"" : """",
            ""milestoneFor"" : """",
            ""broken"" : false,
            ""downloadUrl"" : ""https://services.gradle.org/distributions/gradle-7.0.2-bin.zip"",
            ""checksumUrl"" : ""https://services.gradle.org/distributions/gradle-7.0.2-bin.zip.sha256"",
            ""wrapperChecksumUrl"" : ""https://services.gradle.org/distributions/gradle-7.0.2-wrapper.jar.sha256""
        }";

        private const string NightlyJson = @"{
          ""version"" : ""7.1-20210519220049+0000"",
          ""buildTime"" : ""20210519220049+0000"",
          ""current"" : false,
          ""snapshot"" : true,
          ""nightly"" : true,
          ""releaseNightly"" : false,
          ""activeRc"" : false,
          ""rcFor"" : """",
          ""milestoneFor"" : """",
          ""broken"" : false,
          ""downloadUrl"" : ""https://services.gradle.org/distributions-snapshots/gradle-7.1-20210519220049+0000-bin.zip"",
          ""checksumUrl"" : ""https://services.gradle.org/distributions-snapshots/gradle-7.1-20210519220049+0000-bin.zip.sha256"",
          ""wrapperChecksumUrl"" : ""https://services.gradle.org/distributions-snapshots/gradle-7.1-20210519220049+0000-wrapper.jar.sha256""
        }";

        private const string RandomJson = @"{
          ""version"" : ""6.9-rc-2"",
          ""buildTime"" : ""20210505141217+0000"",
          ""current"" : false,
          ""snapshot"" : false,
          ""nightly"" : false,
          ""releaseNightly"" : false,
          ""activeRc"" : false,
          ""rcFor"" : ""6.9"",
          ""milestoneFor"" : """",
          ""broken"" : false,
          ""downloadUrl"" : ""https://services.gradle.org/distributions/gradle-6.9-rc-2-bin.zip"",
          ""checksumUrl"" : ""https://services.gradle.org/distributions/gradle-6.9-rc-2-bin.zip.sha256"",
          ""wrapperChecksumUrl"" : ""https://services.gradle.org/distributions/gradle-6.9-rc-2-wrapper.jar.sha256""
        }";


        public GradleBootstrapperFixture()
        {
            Configuration = new FakeConfiguration();
            Environment = new FakeEnvironment(PlatformFamily.Linux) {WorkingDirectory = "/tmp"};
            FileSystem = new FakeFileSystem(Environment);
            Log = new FakeLog();
            WebAdapterMock = new Mock<IWebAdapter>();
            WebAdapterMock
                .Setup(x =>
                    x.DownloadString(It.Is<Uri>(y =>
                        y.ToString().EndsWith("/Current", StringComparison.OrdinalIgnoreCase))))
                .Returns(Task<string>.Factory.StartNew(() => CurrentJson));
            WebAdapterMock
                .Setup(x =>
                    x.DownloadString(It.Is<Uri>(y =>
                        y.ToString().EndsWith("/Nightly", StringComparison.OrdinalIgnoreCase))))
                .Returns(Task<string>.Factory.StartNew(() => NightlyJson));
            WebAdapterMock
                .Setup(x =>
                    x.DownloadString(It.Is<Uri>(y =>
                        y.ToString().EndsWith("/all", StringComparison.OrdinalIgnoreCase))))
                .Returns(Task<string>.Factory.StartNew(() => $"[{CurrentJson}, {NightlyJson}, {RandomJson}]"));
            WebAdapterMock
                .Setup(x =>
                    x.DownloadFile(
                        It.IsAny<Uri>(),
                        It.IsAny<FilePath>(),
                        It.IsAny<Action<int>>()))
                .Callback<Uri, FilePath, Action<int>>((uri, filePath, progress) =>
                {
                    FileSystem.CreateFile(filePath, FileAttributes.Archive);
                    progress?.Invoke(100);
                });

            ZipAdapterMock = new Mock<IZipAdapter>();
            _bootstrapper = new GradleBootstrapper(
                Configuration,
                Environment,
                FileSystem,
                Log)
            {
                WebAdapter = WebAdapterMock.Object,
                ZipAdapter = ZipAdapterMock.Object
            };
        }

        public Mock<IZipAdapter> ZipAdapterMock { get; }

        public Mock<IWebAdapter> WebAdapterMock { get; }

        internal void GivenFileExists(FilePath file)
        {
            FileSystem.CreateFile(file);
        }

        internal void GivenDirectoryExistsInToolsPath(string name)
        {
            var toolsFolder = FileSystem.GetDirectory(
                Configuration.GetToolPath(Environment.WorkingDirectory, Environment));

            var target = toolsFolder.Path.Combine(name);

            if (!FileSystem.Exist(target))
            {
                FileSystem.CreateDirectory(target);
            }
        }

        internal async Task<GradleDownloadInformation> GetGradleCurrentVersion()
        {
            return await _bootstrapper.GetGradleVersion(GradleVersionIdentifier.Current);
        }

        internal async Task<GradleDownloadInformation> GetGradleNightlyVersion()
        {
            return await _bootstrapper.GetGradleVersion(GradleVersionIdentifier.Nightly);
        }

        internal async Task<GradleDownloadInformation> GetGradleVersion(string version)
        {
            return await _bootstrapper.GetGradleVersion(GradleVersionIdentifier.ForVersion(version));
        }

        internal void Bootstrap(GradleVersionIdentifier versionIdentifier)
        {
            _bootstrapper.Bootstrap(versionIdentifier);
        }

        internal void UnzipGradle(FilePath zipFile, DirectoryPath target)
        {
            _bootstrapper.UnzipGradle(zipFile, target);
        }

        public void GivenDirectoryDoesNotExist(DirectoryPath directoryPath)
        {
            if (FileSystem.Exist(directoryPath))
            {
                FileSystem.GetDirectory(directoryPath).Delete(true);
            }
        }
    }
}
