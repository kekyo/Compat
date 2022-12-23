////////////////////////////////////////////////////////////////////////////
//
// Compat - Totally makes compatibility layer for .NET platforms.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

using NUnit.Framework;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices;

public sealed class UnsafeTests
{
    [Test]
    public unsafe void AsPointerAsRef()
    {
        var expected = Guid.NewGuid();

        var p = Unsafe.AsPointer(ref expected);

        var actual = Unsafe.AsRef<Guid>(p);

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public unsafe void AsRef()
    {
        var expected = Guid.NewGuid();

        var actual = Unsafe.AsRef(in expected);

        Assert.AreEqual(expected, actual);
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct StructTarget
    {
        public int v0;    // 0
        public byte v1;   // 4
        public short v2;  // 5
        public int v3;    // 7
        public double v4; // 11
    }

    [Test]
    public unsafe void SizeOf()
    {
        var expected = sizeof(StructTarget);

        var actual = Unsafe.SizeOf<StructTarget>();

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public unsafe void As()
    {
        object expected = "ABC";

        var actual = Unsafe.As<string>(expected);

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public unsafe void AsNull()
    {
        object? expected = null;

        var actual = Unsafe.As<string>(expected);

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public unsafe void As2TV()
    {
        var expected =
            MethodImplOptions.NoInlining | MethodImplOptions.PreserveSig;

        short v = (short)expected;
        var actual = Unsafe.As<short, MethodImplOptions>(ref v);

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public unsafe void ByteOffset()
    {
        var v = new StructTarget();

        var actual = Unsafe.ByteOffset(ref v.v0, ref v.v3);

        Assert.AreEqual(new IntPtr(7), actual);
    }
}
