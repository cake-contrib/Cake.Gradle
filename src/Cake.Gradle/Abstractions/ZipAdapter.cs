// unset

using Cake.Common.IO;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;

namespace Cake.Gradle.Abstractions
{
    /// <inheritdoc cref="IZipAdapter"/>
    internal class ZipAdapter : IZipAdapter
    {
        private readonly Zipper _zipper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipAdapter"/> class.
        /// </summary>
        /// <param name="fileSystem">The <see cref="IFileSystem"/>.</param>
        /// <param name="environment">The <see cref="ICakeEnvironment"/>.</param>
        /// <param name="log">The <see cref="ICakeLog"/>.</param>
        internal ZipAdapter(
            IFileSystem fileSystem,
            ICakeEnvironment environment,
            ICakeLog log)
        {
            _zipper = new Zipper(fileSystem, environment, log);
        }

        /// <inheritdoc cref="IZipAdapter.Unzip"/>
        public void Unzip(FilePath zip, DirectoryPath target)
        {
            _zipper.Unzip(zip, target);
        }
    }
}
