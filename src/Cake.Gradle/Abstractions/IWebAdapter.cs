using System;
using System.Threading.Tasks;

using Cake.Core.IO;

namespace Cake.Gradle.Abstractions
{
    /// <summary>
    /// Abstraction of a WebAdapter for tests.
    /// </summary>
    public interface IWebAdapter
    {
        /// <summary>
        /// Downloads a resource and returns it as a string.
        /// </summary>
        /// <param name="uri">The resource to download.</param>
        /// <returns>Content of the resource.</returns>
        Task<string> DownloadString(Uri uri);

        /// <summary>
        /// Downloads a resource to a file.
        /// </summary>
        /// <param name="uri">The resource to download.</param>
        /// <param name="filePath">Path to store the file.</param>
        /// <param name="percentCompleteHandler">Handler to be called when download progress changed.</param>
        /// <returns>Nothing.</returns>
        Task DownloadFile(Uri uri, FilePath filePath, Action<int> percentCompleteHandler);
    }
}
