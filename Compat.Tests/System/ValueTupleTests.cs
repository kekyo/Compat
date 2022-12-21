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

public sealed class ValueTupleTests
{
    [Test]
    public void Item0()
    {
        new ValueTuple();
    }

    [Test]
    public void Item1()
    {
        var item = new ValueTuple<int>(111);
        Assert.AreEqual(111, item.Item1);
    }

    [Test]
    public void Item21()
    {
        var item = new ValueTuple<int, string>(111, "AAA");
        Assert.AreEqual(111, item.Item1);
        Assert.AreEqual("AAA", item.Item2);
    }

    [Test]
    public void Item22()
    {
        var item = (111, "AAA");
        Assert.AreEqual(111, item.Item1);
        Assert.AreEqual("AAA", item.Item2);
    }

    [Test]
    public void Item23()
    {
        var item = (111, "AAA");
        var (item1, item2) = item;

        Assert.AreEqual(111, item1);
        Assert.AreEqual("AAA", item2);
    }

    [Test]
    public void Item31()
    {
        var item = new ValueTuple<int, string, double>(111, "AAA", 123.456);
        Assert.AreEqual(111, item.Item1);
        Assert.AreEqual("AAA", item.Item2);
        Assert.AreEqual(123.456, item.Item3);
    }

    [Test]
    public void Item32()
    {
        var item = (111, "AAA", 123.456);
        Assert.AreEqual(111, item.Item1);
        Assert.AreEqual("AAA", item.Item2);
        Assert.AreEqual(123.456, item.Item3);
    }

    [Test]
    public void Item33()
    {
        var item = (111, "AAA", 123.456);
        var (item1, item2, item3) = item;

        Assert.AreEqual(111, item1);
        Assert.AreEqual("AAA", item2);
        Assert.AreEqual(123.456, item3);
    }

    [Test]
    public void Item41()
    {
        var item = new ValueTuple<int, string, double, byte>(111, "AAA", 123.456, 123);
        Assert.AreEqual(111, item.Item1);
        Assert.AreEqual("AAA", item.Item2);
        Assert.AreEqual(123.456, item.Item3);
    }

    [Test]
    public void Item42()
    {
        var item = (111, "AAA", 123.456, (byte)123);
        Assert.AreEqual(111, item.Item1);
        Assert.AreEqual("AAA", item.Item2);
        Assert.AreEqual(123.456, item.Item3);
        Assert.AreEqual((byte)123, item.Item4);
    }

    [Test]
    public void Item43()
    {
        var item = (111, "AAA", 123.456, (byte)123);
        var (item1, item2, item3, item4) = item;

        Assert.AreEqual(111, item1);
        Assert.AreEqual("AAA", item2);
        Assert.AreEqual(123.456, item3);
        Assert.AreEqual((byte)123, item4);
    }
}
