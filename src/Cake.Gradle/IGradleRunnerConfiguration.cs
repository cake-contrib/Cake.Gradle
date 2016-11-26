using Cake.Core.IO;

namespace Cake.Gradle
{
    public interface IGradleRunnerConfiguration
    {
        /// <summary>
        /// Sets the gradle logging level
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        IGradleRunnerConfiguration WithLogLevel(GradleLogLevel logLevel);

        /// <summary>
        /// Sets the gradle task to be run
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        IGradleRunnerConfiguration WithTask(string task);

        /// <summary>
        /// Sets the gradle arguments to be added
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        IGradleRunnerConfiguration WithArguments(string arguments);

        /// <summary>
        /// Sets the working directory for gradle commands
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        IGradleRunnerConfiguration FromPath(DirectoryPath path);
    }
}