using Cake.Core;
using Cake.Frosting;
using Cake.Gradle;

public static class Program
{
    public static int Main(string[] args)
    {
        return new CakeHost()
            .UseContext<BuildContext>()
            .Run(args);
    }
}

public class BuildContext : FrostingContext
{
    public BuildContext(ICakeContext context)
        : base(context)
    {
    }
}

[TaskName("Gradle-Example")]
public sealed class GradleExampleTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        // Bootstrap a local gradle into tools folder
        context.BootstrapCurrentGradle();

        // Run 'gradle --version'
        // run this, in example folder too. So the "test" does not break when gradle is not installed globally.
        context
            .Gradle()
            .FromPath("../example")
            .WithArguments("--version")
            .Run();

        // Run 'gradle hello' in a specific folder
        // Note: if you have a gradle wrapper setup in the specified path, this one will be used
        context
            .Gradle()
            .FromPath("../example")
            .WithTask("hello")
            .Run();

        // Run 'gradle hello' in a specific folder with default log level
        // Note: if no log level is set, it is derived from the Cake verbosity (which is set to 'verbose' in build.ps1)
        context
            .Gradle()
            .FromPath("../example")
            .WithTask("hello")
            .WithLogLevel(GradleLogLevel.Default).Run();

        // Run 'gradle --offline --build-file build.gradle hello' in a specific folder
        context
            .Gradle()
            .FromPath("../example")
            .WithTask("hello")
            .WithArguments("--offline --build-file build.gradle")
            .Run();
    }
}

[TaskName("Default")]
[IsDependentOn(typeof(GradleExampleTask))]
// ReSharper disable once UnusedType.Global
public class DefaultTask : FrostingTask
{
}
