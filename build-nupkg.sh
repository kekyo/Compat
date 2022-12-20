#!/bin/sh

# Compat - Totally makes compatibility layer for .NET platforms.
# Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
#
# Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0

echo ""
echo "==========================================================="
echo "Build ForestLog"
echo ""

# git clean -xfd

dotnet build -p:Configuration=Release -p:Platform="Any CPU"
dotnet pack -p:Configuration=Release -p:Platform=AnyCPU -o artifacts Compat/Compat.csproj
