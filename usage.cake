 // build artifacts first by executing ./build.ps1
 #r "artifacts/build/Cake.Gradle.dll"

// requires Java Runtime Environment 
// requires Gradle executable in the PATH (gradle wrapper not yet supported)

// execute from cake using: ./build.ps1 -Script usage.cake

Task("Gradle-Example")
    .Does(() =>
    {
        // specify a project folder and a task
        Gradle.FromPath("./example").WithTask("hello").Run();
        // specify the Gradle log level
        // if no log level is set, it is derived from the Cake verbosity (which is set to 'verbose' in build.ps1)
        Gradle.FromPath("./example").WithTask("hello").WithLogLevel(GradleLogLevel.Default).Run(); 
        // specify arguments
        Gradle.FromPath("./example").WithTask("hello").WithArguments("--offline --build-file build.gradle").Run();
        // complete example
        Gradle
            .FromPath("./example")
            .WithTask("hello")
            .WithLogLevel(GradleLogLevel.Quiet)  
            .WithArguments("--offline --build-file build.gradle")
            .Run();
    });

RunTarget("Gradle-Example");