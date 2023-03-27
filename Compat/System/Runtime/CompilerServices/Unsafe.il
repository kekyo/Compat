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

    .method public hidebysig static !!T& AsRef<T>(!!T& v) cil managed
    {
        ldarg.0
        ret
    }

    .method public hidebysig static int32 SizeOf<T>() cil managed
    {
        sizeof !!T
        ret
    }

    .method public hidebysig static !!T As<class T>(object obj) cil managed
    {
        ldarg.0
        ret
    }

    .method public hidebysig static !!TTo& As<TFrom, TTo>(!!TFrom& from) cil managed
    {
        ldarg.0
        ret
    }

    .method public hidebysig static native int ByteOffset<T>(!!T& origin, !!T& target) cil managed
    {
        ldarg.1
        ldarg.0
        sub
        ret
    }

    .method public hidebysig static void Copy<T>(void* destination, !!T& source) cil managed
    {
        ldarg.0
        ldarg.1
        ldobj !!T
        stobj !!T
        ret
    }

    .method public hidebysig static void Copy<T>(!!T& destination, void* source) cil managed
    {
        ldarg.0
        ldarg.1
        ldobj !!T
        stobj !!T
        ret
    }

    .method public hidebysig static void CopyBlock(void* destination, void* source, uint32 byteCount) cil managed
    {
        ldarg.0
        ldarg.1
        ldarg.2
        cpblk
        ret
    }

    .method public hidebysig static void CopyBlock(uint8& destination, uint8& source, uint32 byteCount) cil managed
    {
        ldarg.0
        ldarg.1
        ldarg.2
        cpblk
        ret
    }

    .method public hidebysig static void CopyBlockUnaligned(void* destination, void* source, uint32 byteCount) cil managed
    {
        ldarg.0
        ldarg.1
        ldarg.2
        unaligned. 1
        cpblk
        ret
    }

    .method public hidebysig static void CopyBlockUnaligned(uint8& destination, uint8& source, uint32 byteCount) cil managed
    {
        ldarg.0
        ldarg.1
        ldarg.2
        unaligned. 1
        cpblk
        ret
    }

    .method public hidebysig static void InitBlock(void* startAddress, uint8 v, uint32 byteCount) cil managed
    {
        ldarg.0
        ldarg.1
        ldarg.2
        initblk
        ret
    }

    .method public hidebysig static void InitBlock(uint8& startAddress, uint8 v, uint32 byteCount) cil managed
    {
        ldarg.0
        ldarg.1
        ldarg.2
        initblk
        ret
    }

    .method public hidebysig static void InitBlockUnaligned(void* startAddress, uint8 v, uint32 byteCount) cil managed
    {
        ldarg.0
        ldarg.1
        ldarg.2
        unaligned. 1
        initblk
        ret
    }

    .method public hidebysig static void InitBlockUnaligned(uint8& startAddress, uint8 v, uint32 byteCount) cil managed
    {
        ldarg.0
        ldarg.1
        ldarg.2
        unaligned. 1
        initblk
        ret
    }
}
