namespace Cake.Gradle
{
    public enum GradleLogLevel
    {
        /// <summary>
        /// no command line options
        /// Log Level: Lifecycle or higher
        /// </summary>
        Default,

        /// <summary>
        /// -q, --quiet
        /// </summary>
        Quiet,

        /// <summary>
        /// -i, --info          
        /// </summary>
        Info,

        /// <summary>
        /// -d, --debug
        /// </summary>
        Debug
    }
}