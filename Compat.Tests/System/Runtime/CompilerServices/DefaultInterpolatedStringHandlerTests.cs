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

public sealed class DefaultInterpolatedStringHandlerTests
{
    [Test]
    public void ToStringFromStringInterpolated()
    {
        static DefaultInterpolatedStringHandler Run(int value1, int value2) =>
            $"AAA{value1}BBB{value2}CCC";

        var actual = Run(111, 222).ToString();

        Assert.AreEqual("AAA111BBB222CCC", actual);
    }

    [Test]
    public void FormatProvider()
    {
        var handler = new DefaultInterpolatedStringHandler(
            30, 2, new CultureInfo("de-DE"));

        handler.AppendLiteral("AAA");
        handler.AppendFormatted(111.111);
        handler.AppendLiteral("BBB");
        handler.AppendFormatted(222.222);
        handler.AppendLiteral("CCC");

        var actual = handler.ToString();

        Assert.AreEqual("AAA111,111BBB222,222CCC", actual.ToString());
    }

    [Test]
    public void ToStringAndClear()
    {
        var handler = new DefaultInterpolatedStringHandler(
            30, 2);

        handler.AppendLiteral("AAA");
        handler.AppendFormatted(111);
        handler.AppendLiteral("BBB");
        handler.AppendFormatted(222);
        handler.AppendLiteral("CCC");

        Assert.AreEqual("AAA111BBB222CCC", handler.ToString());
        Assert.AreEqual("AAA111BBB222CCC", handler.ToString());

        Assert.AreEqual("AAA111BBB222CCC", handler.ToStringAndClear());
        Assert.AreEqual("", handler.ToStringAndClear());
    }
}
