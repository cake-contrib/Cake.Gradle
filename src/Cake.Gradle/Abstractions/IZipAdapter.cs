using Cake.Core.IO;

namespace Cake.Gradle.Abstractions
{
    /// <summary>
    /// Abstraction of unzipping for tests.
    /// </summary>
    public interface IZipAdapter
    {
        /// <summary>
        /// Unzip a file to a given directory.
        /// </summary>
        /// <param name="zip">The file to unzip.</param>
        /// <param name="target">The directory in which to unzip.</param>
        void Unzip(FilePath zip, DirectoryPath target);
    }
}
