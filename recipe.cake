#l nuget:?package=Cake.Recipe&version=2.2.1

Environment.SetVariableNames();

BuildParameters.SetParameters(
    context: Context,
    buildSystem: BuildSystem,
    sourceDirectoryPath: "./src",
    title: "Cake.Gradle",
    repositoryOwner: "cake-contrib",
    shouldRunCodecov: true,
    shouldRunDotNetCorePack: true,
    shouldUseDeterministicBuilds: true);

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

