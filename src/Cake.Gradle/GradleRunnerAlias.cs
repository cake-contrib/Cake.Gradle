using System;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Gradle
{
    [CakeAliasCategory("Npm")]
    public static class GradleRunnerAlias
    {

        [CakePropertyAlias]
        public static GradleRunner Gradle(this ICakeContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            return new GradleRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, context.Log.Verbosity);
        }
    }
}
