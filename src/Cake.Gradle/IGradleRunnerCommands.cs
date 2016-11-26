namespace Cake.Gradle
{
    /// <summary>
    /// Gradle Runner command interface
    /// </summary>
    public interface IGradleRunnerCommands
    {
        /// <summary>
        /// execute 'gradle' with options and tasks
        /// </summary>
        IGradleRunnerCommands Run();
    }
}