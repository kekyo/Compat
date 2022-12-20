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

public sealed class FormattableStringFactoryTests
{
    [Test]
    public void ToString1()
    {
        var actual = FormattableStringFactory.Create(
            "AAA{0}BBB{1}CCC",
            111,
            222).
            ToString();
        var expected = "AAA111BBB222CCC";

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void ToString2()
    {
        var actual = FormattableStringFactory.Create(
            "AAA{0:X}BBB{1:x}CCC",
            111,
            222).
            ToString();
        var expected = "AAA6FBBBdeCCC";

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void ToString3()
    {
        var ci = new CultureInfo("de-DE");

        var actual = FormattableStringFactory.Create(
            "AAA{0:G}BBB{1:G}CCC",
            111.111,
            222.222).
            ToString(ci);
        var expected = "AAA111,111BBB222,222CCC";

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void ArgumentCount()
    {
        var actual = FormattableStringFactory.Create(
            "AAA{0}BBB{1}CCC",
            111,
            222).
            ArgumentCount;

        Assert.AreEqual(2, actual);
    }

    [Test]
    public void Format()
    {
        var actual = FormattableStringFactory.Create(
            "AAA{0}BBB{1}CCC",
            111,
            222).
            Format;

        Assert.AreEqual("AAA{0}BBB{1}CCC", actual);
    }

    [Test]
    public void GetArgument()
    {
        var fs = FormattableStringFactory.Create(
            "AAA{0}BBB{1}CCC",
            111,
            222);

        Assert.AreEqual(111, fs.GetArgument(0));
        Assert.AreEqual(222, fs.GetArgument(1));
    }

    [Test]
    public void GetArguments()
    {
        var fs = FormattableStringFactory.Create(
            "AAA{0}BBB{1}CCC",
            111,
            222);

        Assert.AreEqual(new[] { 111, 222 }, fs.GetArguments());
    }

    [Test]
    public void InterpolatedString1()
    {
        static FormattableString Run(int a0, int a1) =>
            $"AAA{a0}BBB{a1}CCC";

        var actual = Run(111, 222).ToString();

        Assert.AreEqual("AAA111BBB222CCC", actual);
    }

    [Test]
    public void InterpolatedString2()
    {
        static IFormattable Run(int a0, int a1) =>
            $"AAA{a0}BBB{a1}CCC";

        var actual = Run(111, 222).ToString();

        Assert.AreEqual("AAA111BBB222CCC", actual);
    }

    [Test]
    public void InterpolatedString3()
    {
        static string Run(int a0, int a1) =>
            $"AAA{a0}BBB{a1}CCC";

        var actual = Run(111, 222);

        Assert.AreEqual("AAA111BBB222CCC", actual);
    }
}
