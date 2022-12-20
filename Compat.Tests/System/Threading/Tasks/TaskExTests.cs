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
using System.Diagnostics;
using System.Linq;

namespace System.Threading.Tasks;

public sealed class TaskExTests
{
    [Test]
    public async Task FromResult()
    {
        var actual = await TaskEx.FromResult(100);
        Assert.AreEqual(100, actual);
    }

#if !(NET35 || NET40)
    [Test]
    public async Task CompletedTask()
    {
        await TaskEx.CompletedTask;
    }
#endif

    [Test]
    public async Task DelayTimeSpan()
    {
        var sw = Stopwatch.StartNew();
        await TaskEx.Delay(TimeSpan.FromMilliseconds(1000));
        sw.Stop();

        var elapsed = sw.ElapsedMilliseconds;
        Assert.GreaterOrEqual(elapsed, 1000 - 250);
    }

    [Test]
    public async Task DelayMilliseconds()
    {
        var sw = Stopwatch.StartNew();
        await TaskEx.Delay(1000);
        sw.Stop();

        var elapsed = sw.ElapsedMilliseconds;
        Assert.GreaterOrEqual(elapsed, 1000 - 250);
    }

    [Test]
    public async Task WhenAll1()
    {
        static async Task Delay(int value)
        {
            await TaskEx.Delay(1000);
        }

        var sw = Stopwatch.StartNew();
        await TaskEx.WhenAll(
            Delay(0),
            Delay(1),
            Delay(2),
            Delay(3),
            Delay(4));
        sw.Stop();

        var elapsed = sw.ElapsedMilliseconds;
        Assert.GreaterOrEqual(elapsed, 1000 - 250);
    }

    [Test]
    public async Task WhenAll2()
    {
        static async Task Delay(int value)
        {
            await TaskEx.Delay(1000);
        }

        var sw = Stopwatch.StartNew();
        await TaskEx.WhenAll(
            Enumerable.Range(0, 10).Select(Delay));
        sw.Stop();

        var elapsed = sw.ElapsedMilliseconds;
        Assert.GreaterOrEqual(elapsed, 1000 - 250);
    }

    [Test]
    public async Task WhenAll3()
    {
        static async Task<int> Delay(int value)
        {
            await TaskEx.Delay(100);
            return value;
        }

        var actual = await TaskEx.WhenAll(
            Delay(0),
            Delay(1),
            Delay(2),
            Delay(3),
            Delay(4));

        var expected = Enumerable.Range(0, 5).ToArray();

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public async Task WhenAll4()
    {
        static async Task<int> Delay(int value)
        {
            await TaskEx.Delay(100);
            return value;
        }

        var actual = await TaskEx.WhenAll(
            Enumerable.Range(0, 10).Select(Delay));

        var expected = Enumerable.Range(0, 10).ToArray();

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public async Task WhenAny1()
    {
        static async Task Delay(int value)
        {
            await TaskEx.Delay((4 - value) * 200 + 1000);
        }

        var sw = Stopwatch.StartNew();
        var delaies = new[]
        {
            Delay(0),
            Delay(1),
            Delay(2),
            Delay(3),
            Delay(4),
        };
        var taken = await TaskEx.WhenAny(delaies);
        sw.Stop();

        Assert.AreSame(delaies[4], taken);

        var differ = sw.ElapsedMilliseconds - 1000;
        Assert.LessOrEqual(differ, 250);
    }

    [Test]
    public async Task WhenAny2()
    {
        static async Task Delay(int value)
        {
            await TaskEx.Delay((4 - value) * 200 + 1000);
        }

        var sw = Stopwatch.StartNew();
        var delaies = new[]
        {
            Delay(0),
            Delay(1),
            Delay(2),
            Delay(3),
            Delay(4),
        };
        var taken = await TaskEx.WhenAny((IEnumerable<Task>)delaies);
        sw.Stop();

        Assert.AreSame(delaies[4], taken);

        var differ = sw.ElapsedMilliseconds - 1000;
        Assert.LessOrEqual(differ, 250);
    }

    [Test]
    public async Task WhenAny3()
    {
        static async Task<int> Delay(int value)
        {
            await TaskEx.Delay((4 - value) * 200 + 1000);
            return value;
        }

        var sw = Stopwatch.StartNew();
        var delaies = new[]
        {
            Delay(0),
            Delay(1),
            Delay(2),
            Delay(3),
            Delay(4),
        };
        var taken = await TaskEx.WhenAny(delaies);
        sw.Stop();

        Assert.AreSame(delaies[4], taken);
        Assert.AreEqual(4, taken.GetAwaiter().GetResult());

        var differ = sw.ElapsedMilliseconds - 1000;
        Assert.LessOrEqual(differ, 250);
    }

    [Test]
    public async Task WhenAny4()
    {
        static async Task<int> Delay(int value)
        {
            await TaskEx.Delay((4 - value) * 200 + 1000);
            return value;
        }

        var sw = Stopwatch.StartNew();
        var delaies = new[]
        {
            Delay(0),
            Delay(1),
            Delay(2),
            Delay(3),
            Delay(4),
        };
        var taken = await TaskEx.WhenAny((IEnumerable<Task<int>>)delaies);
        sw.Stop();

        Assert.AreSame(delaies[4], taken);
        Assert.AreEqual(4, taken.GetAwaiter().GetResult());

        var differ = sw.ElapsedMilliseconds - 1000;
        Assert.LessOrEqual(differ, 250);
    }

    [Test]
    public async Task Run()
    {
        static int Delay(int value)
        {
            Thread.Sleep(100);
            return value;
        }

        var actual = await TaskEx.WhenAll(
            Enumerable.Range(0, 10).Select(index => TaskEx.Run(() => Delay(index))));

        var expected = Enumerable.Range(0, 10).ToArray();

        Assert.AreEqual(expected, actual);
    }
}
