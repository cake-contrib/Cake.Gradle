#l nuget:?package=Cake.Recipe&version=3.1.1

Environment.SetVariableNames();

BuildParameters.SetParameters(
    context: Context,
    buildSystem: BuildSystem,
    sourceDirectoryPath: "./src",
    title: "Cake.Gradle",
    repositoryOwner: "cake-contrib",
    shouldRunCodecov: false,
    shouldRunDotNetCorePack: true,
    shouldUseDeterministicBuilds: true,
    preferredBuildProviderType: BuildProviderType.GitHubActions,
    preferredBuildAgentOperatingSystem: PlatformFamily.Linux);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(
    context: Context,
    testCoverageFilter: $"+[{BuildParameters.Title}*]* -[*.Tests]* -[{BuildParameters.Title}]LitJson.* -[{BuildParameters.Title}]Cake.Gradle.Abstractions.*");

Build.RunDotNetCore();

