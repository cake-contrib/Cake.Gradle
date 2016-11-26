using System;

namespace Cake.Gradle
{
    public interface IGradleRunnerCommands
    {
        /// <summary>
        /// execute 'gradle' with options and tasks
        /// </summary>
        IGradleRunnerCommands Run();
    }
}