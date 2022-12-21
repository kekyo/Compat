////////////////////////////////////////////////////////////////////////////
//
// Compat - Totally makes compatibility layer for .NET platforms.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

using NUnit.Framework;
using System.Collections.Generic;

namespace System.Linq;

public sealed class EnumerableExTest
{
    [Test]
    public void Append1()
    {
        var items = new[] { 123, 456, 789, };
        var actual = items.Append(111).ToArray();

        Assert.AreEqual(new[] { 123, 456, 789, 111, }, actual);
    }

    [Test]
    public void Append2()
    {
        var items = new int[0];
        var actual = items.Append(111).ToArray();

        Assert.AreEqual(new[] { 111, }, actual);
    }

    ///////////////////////////////////////////////////////////////////

    [Test]
    public void Prepend1()
    {
        var items = new[] { 123, 456, 789, };
        var actual = items.Prepend(111).ToArray();

        Assert.AreEqual(new[] { 111, 123, 456, 789, }, actual);
    }

    [Test]
    public void Prepend2()
    {
        var items = new int[0];
        var actual = items.Prepend(111).ToArray();

        Assert.AreEqual(new[] { 111, }, actual);
    }

    ///////////////////////////////////////////////////////////////////

    [Test]
    public void TakeLast1()
    {
        var items = new[] { 123, 456, 789, };
        var actual = items.TakeLast(2).ToArray();

        Assert.AreEqual(new[] { 456, 789, }, actual);
    }

    [Test]
    public void TakeLast2()
    {
        var items = new[] { 123, 456, 789, };
        var actual = items.TakeLast(4).ToArray();

        Assert.AreEqual(new[] { 123, 456, 789, }, actual);
    }

    [Test]
    public void TakeLast3()
    {
        var items = new int[0];
        var actual = items.TakeLast(3).ToArray();

        Assert.AreEqual(0, actual.Length);
    }

    ///////////////////////////////////////////////////////////////////

    [Test]
    public void SkipLast1()
    {
        var items = new[] { 123, 456, 789, };
        var actual = items.SkipLast(2).ToArray();

        Assert.AreEqual(new[] { 123, }, actual);
    }

    [Test]
    public void SkipLast2()
    {
        var items = new[] { 123, 456, 789, };
        var actual = items.SkipLast(4).ToArray();

        Assert.AreEqual(0, actual.Length);
    }

    [Test]
    public void SkipLast3()
    {
        var items = new int[0];
        var actual = items.SkipLast(3).ToArray();

        Assert.AreEqual(0, actual.Length);
    }

    ///////////////////////////////////////////////////////////////////

    [Test]
    public void Zip()
    {
        var left = new[] { 123, 456, 789, };
        var right = new[] { "AAA", "BBB", "CCC", };
        var actual = left.Zip(right, (l, r) => (l, r));

        Assert.AreEqual(
            new[] { (123, "AAA"), (456, "BBB"), (789, "CCC"), },
            actual);
    }

    [Test]
    public void ZipSimple()
    {
        var left = new[] { 123, 456, 789, };
        var right = new[] { "AAA", "BBB", "CCC", };
        var actual = left.Zip(right);

        Assert.AreEqual(
            new[] { (123, "AAA"), (456, "BBB"), (789, "CCC"), },
            actual);
    }

    ///////////////////////////////////////////////////////////////////

    [Test]
    public void ToHashSet()
    {
        var items = new[] { 123, 456, 789, };
        var actual = items.ToHashSet();

        foreach (var item in items)
        {
            Assert.IsTrue(actual.Contains(item));
        }
    }
}
