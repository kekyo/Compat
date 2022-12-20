////////////////////////////////////////////////////////////////////////////
//
// Compat - Totally makes compatibility layer for .NET platforms.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

using NUnit.Framework;

namespace System;

public sealed class ArrayExTests
{
    [Test]
    public void EmptyInt32()
    {
        Assert.AreEqual(0, ArrayEx.Empty<int>().Length);
    }

    [Test]
    public void EmptyString()
    {
        Assert.AreEqual(0, ArrayEx.Empty<string>().Length);
    }
}
