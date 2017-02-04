 // build artifacts first by executing ./build.ps1
 #r "artifacts/build/Cake.Gradle.dll"

// requires Java Runtime Environment 
// requires Gradle executable in the PATH (gradle wrapper not yet supported)

// execute from cake using: ./build.ps1 -Script usage.cake

Task("Gradle-Example")
    .Does(() =>
    {
        // Run 'gradle --version'
        Gradle.WithArguments("--version").Run();
        // Run 'gradle hello' in a specific folder
        // Note: if you have a gradle wrapper setup in the specified path, this one will be used
        Gradle.FromPath("./example").WithTask("hello").Run();
        // Run 'gradle hello' in a specific folder with default log level
        // Note: if no log level is set, it is derived from the Cake verbosity (which is set to 'verbose' in build.ps1)
        Gradle.FromPath("./example").WithTask("hello").WithLogLevel(GradleLogLevel.Default).Run(); 
        // Run 'gradle --offline --build-file build.gradle hello' in a specific folder
        Gradle.FromPath("./example").WithTask("hello").WithArguments("--offline --build-file build.gradle").Run();
    });

RunTarget("Gradle-Example");