# cake-gradle

Aliases to assist with running Gradle builds from Cake build scripts.

## Usage

```c#
 #r "artifacts/build/Cake.Gradle.dll"

// Run 'gradle --version'
Task("Gradle-Version")
    .Does(() =>
{
    Gradle.WithArguments("--version").Run();
});

// Run 'gradle hello' in a specific folder
// Note: if you have a gradle wrapper setup in the specified path, this one will be used
Task("Gradle-Hello")
    .Does(() =>
{
    Gradle.FromPath("./example").WithTask("hello").Run();
});


// Run 'gradle hello' in a specific folder with default log level
// Note: if no log level is set, it is derived from the Cake verbosity (which is set to 'verbose' in build.ps1)
Task("Gradle-Hello-WithDefaultLogLevel")
    .Does(() =>
{
    Gradle.FromPath("./example").WithTask("hello").WithLogLevel(GradleLogLevel.Default).Run(); 
});

// Run 'gradle --offline --build-file build.gradle hello' in a specific folder
Task("Gradle-Hello-WithArguments")
    .Does(() =>
{
    Gradle.FromPath("./example").WithTask("hello").WithArguments("--offline --build-file build.gradle").Run();
});

```

See also working usage example `usage.cake` (requires Java Runtime Environment).

## Compatibility

Developed and tested with Cake 0.15.2

## Motivation

Allow Cake users to orchestrate a complex build including a Gradle-based Java build.
Works similar to the [cake-gulp](https://github.com/Philo/cake-gulp) addin.

## Word of caution

Cake and Gradle are both task runners. I consider it bad practice to call one task runner out of another. 
It would be better to only have one tool per concern (i.e. task running) - but sometimes this is not feasible.
