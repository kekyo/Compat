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

public sealed class StringExTests
{
    [TestCase(false, "ABC")]
    [TestCase(true, "")]
    [TestCase(true, null)]
    public void IsNullOrWhiteSpace(bool expected, string? value)
    {
        var actual = StringEx.IsNullOrWhiteSpace(value);
        Assert.AreEqual(expected, actual);
    }
}
