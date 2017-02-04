namespace Cake.Gradle
{
    /// <summary>
    /// Represents the various log levels the Gradle executable can be called with.
    /// </summary>
    public enum GradleLogLevel
    {
        /// <summary>
        /// No command line options
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