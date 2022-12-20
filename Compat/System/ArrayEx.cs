////////////////////////////////////////////////////////////////////////////
//
// Compat - Totally makes compatibility layer for .NET platforms.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

namespace System;

public static class ArrayEx
{
#if NET35 || NET40 || NET45 || NET452
    private static class EmptyArray<T>
    {
        public static readonly T[] Empty = new T[0];
    }
#endif

    public static T[] Empty<T>() =>
#if NET35 || NET40 || NET45 || NET452
        EmptyArray<T>.Empty;
#else
        Array.Empty<T>();
#endif
}
