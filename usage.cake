 #addin "Cake.Gradle"

Task("Gradle-Example")
    .Does(() =>
    {
        Gradle.Run();
        Gradle.Run("hello");
        Gradle.WithArguments("--offline --build-file build.gradle").Run("hello");
        Gradle.WithLogLevel(GradleLogLevel.Info).WithArguments("--offline --build-file build.gradle").Run("hello");
    });

RunTarget("Gradle-Example");