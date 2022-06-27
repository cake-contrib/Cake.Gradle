#l nuget:?package=Cake.Recipe&version=2.2.1

// Workaround for https://github.com/cake-contrib/Cake.Recipe/issues/854
#tool nuget:?package=NuGet.CommandLine&version=6.2.1

Environment.SetVariableNames();

BuildParameters.SetParameters(
    context: Context,
    buildSystem: BuildSystem,
    sourceDirectoryPath: "./src",
    title: "Cake.Gradle",
    repositoryOwner: "cake-contrib",
    shouldRunCodecov: true,
    shouldRunDotNetCorePack: true,
    shouldUseDeterministicBuilds: true,
    preferredBuildProviderType: BuildProviderType.GitHubActions,
    preferredBuildAgentOperatingSystem: PlatformFamily.Linux);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(
    context: Context,
    dupFinderExcludePattern: new string[] {
        MakeAbsolute(BuildParameters.SolutionDirectoryPath.CombineWithFilePath("LitJson/*.cs")).FullPath,
        MakeAbsolute(BuildParameters.SolutionDirectoryPath.CombineWithFilePath("**/*.AssemblyInfo.cs")).FullPath,
        MakeAbsolute(BuildParameters.SourceDirectoryPath.CombineWithFilePath("Cake.Gradle.Tests/**/*.cs")).FullPath
    },
    testCoverageFilter: $"+[{BuildParameters.Title}*]* -[*.Tests]* -[{BuildParameters.Title}]LitJson.* -[{BuildParameters.Title}]Cake.Gradle.Abstractions.*");

Build.RunDotNetCore();

