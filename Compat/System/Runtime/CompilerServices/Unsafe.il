////////////////////////////////////////////////////////////////////////////
//
// Compat - Totally makes compatibility layer for .NET platforms.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

.class public System.Runtime.CompilerServices.Unsafe
{
    .method public hidebysig static void* AsPointer<T>(!!T& v) cil managed
    {
        ldarg.0
        conv.u
        ret
    }

    .method public hidebysig static !!T& AsRef<T>(void* p) cil managed
    {
        ldarg.0
        ret
    }

    .method public hidebysig static int32 SizeOf<T>() cil managed
    {
        sizeof !!T
        ret
    }
}
