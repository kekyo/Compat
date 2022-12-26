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

    [Test]
    public unsafe void CopyToPointer()
    {
        var expected = Guid.NewGuid();
        var actual = default(Guid);
        var p = Unsafe.AsPointer(ref actual);

        Unsafe.Copy(p, ref expected);

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public unsafe void CopyFromPointer()
    {
        var expected = Guid.NewGuid();
        var actual = default(Guid);
        var p = Unsafe.AsPointer(ref expected);

        Unsafe.Copy(ref actual, p);

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public unsafe void CopyPointerToPointer()
    {
        var expected = Guid.NewGuid();
        var actual = default(Guid);
        var ps = Unsafe.AsPointer(ref expected);
        var pd = Unsafe.AsPointer(ref actual);
        var s = Unsafe.SizeOf<Guid>();

        Unsafe.CopyBlock(pd, ps, (uint)s);

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public unsafe void CopyRefToRef()
    {
        var expected = Guid.NewGuid();
        var actual = default(Guid);
        ref var rs = ref Unsafe.AsRef<byte>(Unsafe.AsPointer(ref expected));
        ref var rd = ref Unsafe.AsRef<byte>(Unsafe.AsPointer(ref actual));
        var s = Unsafe.SizeOf<Guid>();

        Unsafe.CopyBlock(ref rd, ref rs, (uint)s);

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public unsafe void CopyPointerToPointerUnaligned()
    {
        var expected = Guid.NewGuid();
        var s = Unsafe.SizeOf<Guid>();

        var pd0 = Marshal.AllocHGlobal(s + 1);
        try
        {
            var pd = new IntPtr(pd0.ToInt64() + 1).ToPointer();

            var ps = Unsafe.AsPointer(ref expected);

            Unsafe.CopyBlockUnaligned(pd, ps, (uint)s);

            var actual = default(Guid);
            var pa = Unsafe.AsPointer(ref actual);

            Unsafe.CopyBlockUnaligned(pa, pd, (uint)s);

            Assert.AreEqual(expected, actual);
        }
        finally
        {
            Marshal.FreeHGlobal(pd0);
        }
    }

    [Test]
    public unsafe void CopyRefToRefUnaligned()
    {
        var expected = Guid.NewGuid();
        var s = Unsafe.SizeOf<Guid>();

        var pd0 = Marshal.AllocHGlobal(s + 1);
        try
        {
            var pd = new IntPtr(pd0.ToInt64() + 1).ToPointer();

            ref var rs = ref Unsafe.AsRef<byte>(Unsafe.AsPointer(ref expected));
            ref var rd = ref Unsafe.AsRef<byte>(pd);

            Unsafe.CopyBlockUnaligned(ref rd, ref rs, (uint)s);

            var actual = default(Guid);
            var pa = Unsafe.AsPointer(ref actual);

            Unsafe.CopyBlockUnaligned(pa, pd, (uint)s);

            Assert.AreEqual(expected, actual);
        }
        finally
        {
            Marshal.FreeHGlobal(pd0);
        }
    }
}
