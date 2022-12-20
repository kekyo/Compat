////////////////////////////////////////////////////////////////////////////
//
// Compat - Totally makes compatibility layer for .NET platforms.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

namespace System;

public static class StringEx
{
    public static bool IsNullOrWhiteSpace(string? text) =>
#if NET35
        string.IsNullOrEmpty(text) || (text!.Trim().Length == 0);
#else
        string.IsNullOrWhiteSpace(text);
#endif
}
