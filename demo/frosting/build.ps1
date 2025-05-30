$ErrorActionPreference = 'Stop'

Set-Location -LiteralPath $PSScriptRoot

$env:DOTNET_SKIP_FIRST_TIME_EXPERIENCE = '1'
$env:DOTNET_CLI_TELEMETRY_OPTOUT = '1'
$env:DOTNET_NOLOGO = '1'

# ensure a most-recent debug-build, so we can reference that.
dotnet build ../../src/Cake.Gradle/Cake.Gradle.csproj
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

dotnet run --project build/Build.csproj -- $args
exit $LASTEXITCODE;
