#l nuget:?package=Cake.Recipe&version=2.1.0

Environment.SetVariableNames();

BuildParameters.SetParameters(
    context: Context,
    buildSystem: BuildSystem,
    sourceDirectoryPath: "./src",
    title: "Cake.Gradle",
    repositoryOwner: "cake-contrib",
    repositoryName: "cake.gradle",
    shouldRunCodecov: true,
    shouldRunDotNetCorePack: true,
    shouldUseDeterministicBuilds: true);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context);

Build.RunDotNetCore();

