////////////////////////////////////////////////////////////////////////////
//
// Compat - Totally makes compatibility layer for .NET platforms.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

#if NET35 || NET40

namespace System.Runtime.CompilerServices;

public static class Unsafe
{
    [MethodImpl(MethodImplOptions.ForwardRef)]
    public static extern unsafe void* AsPointer<T>(ref T v);

    [MethodImpl(MethodImplOptions.ForwardRef)]
    public static extern unsafe ref T AsRef<T>(void* p);

    [MethodImpl(MethodImplOptions.ForwardRef)]
    public static extern ref T AsRef<T>(in T from);

    [MethodImpl(MethodImplOptions.ForwardRef)]
    public static extern unsafe int SizeOf<T>();

    [MethodImpl(MethodImplOptions.ForwardRef)]
    public static extern T As<T>(object? o) where T : class;

    [MethodImpl(MethodImplOptions.ForwardRef)]
    public static extern ref TTo As<TFrom, TTo>(ref TFrom from);

    [MethodImpl(MethodImplOptions.ForwardRef)]
    public static extern IntPtr ByteOffset<T>(ref T origin, ref T target);
}

#endif
