////////////////////////////////////////////////////////////////////////////
//
// Compat - Totally makes compatibility layer for .NET platforms.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;

namespace System.IO;

public static class DirectoryEx
{
    public static IEnumerable<string> EnumerateFiles(
        string path, string pattern, SearchOption so) =>
#if NET35
        Directory.GetFiles(path, pattern, so);
#else
        Directory.EnumerateFiles(path, pattern, so);
#endif
}
