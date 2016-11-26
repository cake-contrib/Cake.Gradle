namespace Cake.Gradle
{
    public enum GradleLogLevel
    {
        LifecycleAndHigher,

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