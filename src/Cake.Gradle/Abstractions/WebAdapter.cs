using System;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;

using Cake.Core;
using Cake.Core.IO;

namespace Cake.Gradle.Abstractions
{
    /// <inheritdoc cref="IWebAdapter"/>
    internal class WebAdapter : IWebAdapter
    {
        private readonly ICakeEnvironment _environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebAdapter"/> class.
        /// </summary>
        /// <param name="environment">The -<see cref="ICakeEnvironment"/>.</param>
        public WebAdapter(ICakeEnvironment environment)
        {
            _environment = environment;
        }

        /// <inheritdoc cref="IWebAdapter.DownloadString"/>
        public async Task<string> DownloadString(Uri uri)
        {
            using (var client = GetWebClient())
            {
                return await client.DownloadStringTaskAsync(uri);
            }
        }

        /// <inheritdoc cref="IWebAdapter.DownloadFile"/>
        public async Task DownloadFile(Uri uri, FilePath filePath, Action<int> percentCompleteHandler)
        {
            using (var client = GetWebClient())
            {
                var currentProgress = 0;
                client.DownloadProgressChanged += (_, e) =>
                {
                    if (e.ProgressPercentage != currentProgress && e.ProgressPercentage % 5 == 0)
                    {
                        currentProgress = e.ProgressPercentage;
                        percentCompleteHandler(e.ProgressPercentage);
                    }
                };

                await client
                    .DownloadFileTaskAsync(
                        uri,
                        filePath.MakeAbsolute(_environment).FullPath)
                    .ConfigureAwait(false);
            }
        }

        private WebClient GetWebClient()
        {
            var client = new WebClient();
            var assembly = GetType().Assembly;
            var product = assembly.GetName().Name;
            var version = assembly
                .GetCustomAttributes<AssemblyInformationalVersionAttribute>()
                .FirstOrDefault()?.InformationalVersion ?? "0.0.0";
            var productInfoHeader = new ProductInfoHeaderValue(product, version);
            client.Headers.Add("user-agent", productInfoHeader.ToString());

            return client;
        }
    }
}
