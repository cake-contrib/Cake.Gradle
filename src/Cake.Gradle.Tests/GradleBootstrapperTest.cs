using System;
using System.Threading.Tasks;

using Cake.Core.IO;
using Cake.Gradle.Bootstrap;
using Cake.Gradle.Tests.Fixtures;

using Moq;

using VerifyXunit;

using Xunit;

namespace Cake.Gradle.Tests
{
    public class GradleBootstrapperTest
    {
        private readonly GradleBootstrapperFixture _fixture;

        public GradleBootstrapperTest()
        {
            _fixture = new GradleBootstrapperFixture();
        }

        [Fact]
        public async Task GetCurrentVersion_returns_the_current_version_info()
        {
            var version = await _fixture.GetGradleCurrentVersion();
            await Verifier.Verify(version);
        }

        [Fact]
        public async Task GetNightlyVersion_returns_the_nightly_version_info()
        {
            var version = await _fixture.GetGradleNightlyVersion();
            await Verifier.Verify(version);
        }

        [Fact]
        public async Task GetStringVersion_returns_the_correct_version_info()
        {
            var version = await _fixture.GetGradleVersion("6.9-rc-2");
            await Verifier.Verify(version);
        }

        [Fact]
        public async Task GetCurrentVersion_queries_the_endpoint_named_current()
        {
            await _fixture.GetGradleCurrentVersion();

            _fixture.WebAdapterMock.Verify(
                x => x.DownloadString(It.Is<Uri>(
                    y => y.ToString().Equals(
                        "https://services.gradle.org/versions/current",
                        StringComparison.OrdinalIgnoreCase))),
                Times.Once);
        }

        [Fact]
        public async Task GetNightlyVersion_queries_the_endpoint_named_nightly()
        {
            await _fixture.GetGradleNightlyVersion();

            _fixture.WebAdapterMock.Verify(
                x => x.DownloadString(It.Is<Uri>(
                    y => y.ToString().Equals(
                        "https://services.gradle.org/versions/nightly",
                        StringComparison.OrdinalIgnoreCase))),
                Times.Once);
        }

        [Fact]
        public async Task GetStringVersion_queries_the_endpoint_named_all()
        {
            await _fixture.GetGradleVersion("6.9-rc-2");

            _fixture.WebAdapterMock.Verify(
                x => x.DownloadString(It.Is<Uri>(
                    y => y.ToString().Equals(
                        "https://services.gradle.org/versions/all",
                        StringComparison.OrdinalIgnoreCase))),
                Times.Once);
        }

        [Fact]
        public void Bootstrap_CurrentVersion_downloads_the_current_version()
        {
            _fixture.Bootstrap(GradleVersionIdentifier.Current);

            _fixture.WebAdapterMock.Verify(
                x => x.DownloadString(It.Is<Uri>(
                    y => y.ToString().Equals(
                        "https://services.gradle.org/versions/current",
                        StringComparison.OrdinalIgnoreCase))),
                Times.Once);
            _fixture.ZipAdapterMock.Verify(x =>
                x.Unzip(
                    It.Is<FilePath>(f =>
                        f.GetFilename().ToString() == "gradle-7.0.2-bin.zip"),
                    It.IsAny<DirectoryPath>()));
        }

        [Fact]
        public void Bootstrap_NightlyVersion_downloads_the_nightly_version()
        {
            _fixture.Bootstrap(GradleVersionIdentifier.Nightly);

            _fixture.WebAdapterMock.Verify(
                x => x.DownloadString(It.Is<Uri>(
                    y => y.ToString().Equals(
                        "https://services.gradle.org/versions/nightly",
                        StringComparison.OrdinalIgnoreCase))),
                Times.Once);
            _fixture.ZipAdapterMock.Verify(x =>
                x.Unzip(
                    It.Is<FilePath>(f =>
                        f.GetFilename().ToString() == "gradle-7.1-20210519220049+0000-bin.zip"),
                    It.IsAny<DirectoryPath>()));
        }

        [Fact]
        public void Bootstrap_StringVersion_downloads_all_versionInfos()
        {
            _fixture.Bootstrap(GradleVersionIdentifier.ForVersion("6.9-rc-2"));

            _fixture.WebAdapterMock.Verify(
                x => x.DownloadString(It.Is<Uri>(
                    y => y.ToString().Equals(
                        "https://services.gradle.org/versions/all",
                        StringComparison.OrdinalIgnoreCase))),
                Times.Once);
            _fixture.ZipAdapterMock.Verify(x =>
                x.Unzip(
                    It.Is<FilePath>(f =>
                        f.GetFilename().ToString() == "gradle-6.9-rc-2-bin.zip"),
                    It.IsAny<DirectoryPath>()));
        }

        [Fact]
        public void Unzip_WithNonExistingTarget_Does_Not_Throw()
        {
            var zipFile = new FilePath("test.zip");
            var target = new DirectoryPath("Does-not-exist");
            _fixture.GivenFileExists(zipFile);
            _fixture.GivenDirectoryDoesNotExist(target);

            _fixture.UnzipGradle(zipFile, target);
        }

        [Fact]
        public void Bootstrap_Will_NotDownload_If_Target_Folder_Exists()
        {
            _fixture.GivenDirectoryExistsInToolsPath("gradle-7.0.2");
            _fixture.Bootstrap(GradleVersionIdentifier.Current);

            _fixture.WebAdapterMock.Verify(
                x => x.DownloadFile(
                    It.IsAny<Uri>(),
                    It.IsAny<FilePath>(),
                    It.IsAny<Action<int>>()),
                Times.Never);
        }
    }
}
