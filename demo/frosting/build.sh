#!/usr/bin/env bash
set -euo pipefail

cd "$(dirname "${BASH_SOURCE[0]}")"

export DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1
export DOTNET_CLI_TELEMETRY_OPTOUT=1
export DOTNET_NOLOGO=1

# ensure a most-recent debug-build, so we can reference that.
dotnet build ../../src/Cake.Gradle/Cake.Gradle.csproj

dotnet run --project ./build/Build.csproj -- "$@"
