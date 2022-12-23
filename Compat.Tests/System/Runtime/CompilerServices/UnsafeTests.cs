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
    public unsafe void SizeOf()
    {
        var expected = sizeof(Guid);

        var actual = Unsafe.SizeOf<Guid>();

        Assert.AreEqual(expected, actual);
    }
}
