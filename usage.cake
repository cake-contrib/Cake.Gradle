 #r "artifacts/build/Cake.Gradle.dll"

// execute from cake using: ./build.ps1 -Script usage.cake

Task("Gradle-Example")
    .Does(() =>
    {
        Gradle.FromPath("./example").WithTask("hello").Run();
        // Gradle.WithTask("hello").Run();
        // Gradle.WithTask("hello").WithArguments("--offline --build-file build.gradle").Run();
        // Gradle.WithTasks("hello").WithLogLevel(GradleLogLevel.Info).WithArguments("--offline --build-file build.gradle").Run();
    });

RunTarget("Gradle-Example");