# cake-gradle

Aliases to assist with running Gradle builds from Cake build scripts

## Usage

```c#
// reference addin after building it by executing ./build.ps1
 #r "artifacts/build/Cake.Gradle.dll"

Task("Gradle-Default-Task")
    .Does(() =>
    {
        Gradle.Run();
    });

Task("Gradle-Specific-Task")
    .Does(() =>
    {
        Gradle.WithTask("hello").Run();
    });

Task("Gradle-Full-Blown")
    .Does(() =>
    {
        Gradle.FromPath("./example").WithTask("hello").WithLogLevel(GradleLogLevel.Info).WithArguments("--offline --build-file build.gradle").Run();
    });

```

See also working usage example `usage.cake` (requires Java Runtime Environment and Gradle).

## Status

Work in progress. Code reviews and pull requests welcome.

## Motivation

Allow Cake users to orchestrate a complex build including a Gradle-based Java build.
Works similar to the [cake-gulp](https://github.com/Philo/cake-gulp) addin.

## Word of caution

Cake and Gradle are both task runners. I consider it bad practice to call one taks runner out of another. 
It would be better to only have one tool per concern (i.e. task running) - but sometimes this is not feasible.