// unset

namespace Cake.Gradle
{
    /// <summary>
    /// Properties for gradle invocation.
    /// </summary>
    internal class GradleProperty
    {
        /// <summary>
        /// Types of gradle properties.
        /// </summary>
        internal enum Type
        {
            /// <summary>
            /// Project, i.e. will result in "-Dkey=value"
            /// </summary>
            System,

            /// <summary>
            /// Project, i.e. will result in "-Pkey=value"
            /// </summary>
            Project,
        }

        /// <summary>
        /// Gets or sets the <see cref="Type"/>.
        /// </summary>
        internal Type PropertyType { get; set; }

        /// <summary>
        /// Gets or sets the property key.
        /// </summary>
        internal string Key { get; set; }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        internal string Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="Value"/> is a secret.
        /// </summary>
        internal bool SecretValue { get; set; }
    }
}
