@echo off

rem Compat - Totally makes compatibility layer for .NET platforms.
rem Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
rem
rem Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0

echo.
echo "==========================================================="
echo "Build Compat"
echo.

rem git clean -xfd

dotnet build -p:Configuration=Release -p:Platform="Any CPU"
dotnet pack -p:Configuration=Release -p:Platform=AnyCPU -o artifacts Compat\Compat.csproj
