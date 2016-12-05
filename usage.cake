 #r "artifacts/build/Cake.Gradle.dll"

// execute from cake using: ./build.ps1 -Script usage.cake

Task("Gradle-Example")
    .Does(() =>
    {
        Gradle.FromPath("./example").WithTask("hello").Run();
        Gradle.FromPath("./example").WithTask("hello").WithArguments("--offline --build-file build.gradle").Run();
        Gradle.FromPath("./example").WithTask("hello").WithLogLevel(GradleLogLevel.Info).WithArguments("--offline --build-file build.gradle").Run();
    });

RunTarget("Gradle-Example");