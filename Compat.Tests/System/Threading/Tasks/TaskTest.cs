////////////////////////////////////////////////////////////////////////////
//
// Compat - Totally makes compatibility layer for .NET platforms.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

using NUnit.Framework;

namespace System.Threading.Tasks;

// Async method lacks 'await' operators and will run synchronously
#pragma warning disable CS1998

public sealed class TaskTest
{
    private sealed class TestException : Exception
    {
    }

    [Test]
    public async Task WaitAsyncCompletedImmediately()
    {
        static async Task Immediate()
        {
        }

        var cts = new CancellationTokenSource();

        await Immediate().WaitAsync(cts.Token);
    }

    [Test]
    public async Task WaitAsyncCanceledImmediately()
    {
        static async Task Immediate()
        {
        }

        var cts = new CancellationTokenSource();
        cts.Cancel();

        await Immediate().WaitAsync(cts.Token);
    }

    [Test]
    public async Task WaitAsyncFaultedImmediately()
    {
        static async Task Immediate(Exception ex)
        {
            throw ex;
        }

        var cts = new CancellationTokenSource();
        var ex = new TestException();

        try
        {
            await Immediate(ex).WaitAsync(cts.Token);
            Assert.Fail();
        }
        catch (TestException ex2)
        {
            Assert.AreSame(ex, ex2);
        }
    }

    [Test]
    public async Task WaitAsyncCompletedDelayed()
    {
        static async Task Delay()
        {
            await TaskEx.Delay(100);
        }

        var cts = new CancellationTokenSource();

        await Delay().WaitAsync(cts.Token);
    }

    [Test]
    public async Task WaitAsyncCanceledDelayed()
    {
        static async Task Delay()
        {
            await TaskEx.Delay(100);
        }

        var cts = new CancellationTokenSource();
        cts.Cancel();

        try
        {
            await Delay().WaitAsync(cts.Token);
            Assert.Fail();
        }
        catch (TaskCanceledException)
        {
        }
    }

    [Test]
    public async Task WaitAsyncFaultedDelayed()
    {
        static async Task Delay(Exception ex)
        {
            await TaskEx.Delay(100);
            throw ex;
        }

        var cts = new CancellationTokenSource();
        var ex = new TestException();

        try
        {
            await Delay(ex).WaitAsync(cts.Token);
            Assert.Fail();
        }
        catch (TestException ex2)
        {
            Assert.AreSame(ex, ex2);
        }
    }

    ///////////////////////////////////////////////////////////////////

    [Test]
    public async Task WaitAsyncTCompletedImmediately()
    {
        static async Task<int> Immediate()
        {
            return 111;
        }

        var cts = new CancellationTokenSource();

        var actual = await Immediate().WaitAsync(cts.Token);

        Assert.AreEqual(111, actual);
    }

    [Test]
    public async Task WaitAsyncTCanceledImmediately()
    {
        static async Task<int> Immediate()
        {
            return 111;
        }

        var cts = new CancellationTokenSource();
        cts.Cancel();

        var actual = await Immediate().WaitAsync(cts.Token);

        Assert.AreEqual(111, actual);
    }

    [Test]
    public async Task WaitAsyncTFaultedImmediately()
    {
        static async Task<int> Immediate(Exception ex)
        {
            throw ex;
        }

        var cts = new CancellationTokenSource();
        var ex = new TestException();

        try
        {
            var _ = await Immediate(ex).WaitAsync(cts.Token);
            Assert.Fail();
        }
        catch (TestException ex2)
        {
            Assert.AreSame(ex, ex2);
        }
    }

    [Test]
    public async Task WaitAsyncTCompletedDelayed()
    {
        static async Task<int> Delay()
        {
            await TaskEx.Delay(100);
            return 111;
        }

        var cts = new CancellationTokenSource();

        var actual = await Delay().WaitAsync(cts.Token);

        Assert.AreEqual(111, actual);
    }

    [Test]
    public async Task WaitAsyncTCanceledDelayed()
    {
        static async Task<int> Delay()
        {
            await TaskEx.Delay(100);
            return 111;
        }

        var cts = new CancellationTokenSource();
        cts.Cancel();

        try
        {
            var _ = await Delay().WaitAsync(cts.Token);
            Assert.Fail();
        }
        catch (TaskCanceledException)
        {
        }
    }

    [Test]
    public async Task WaitAsyncTFaultedDelayed()
    {
        static async Task<int> Delay(Exception ex)
        {
            await TaskEx.Delay(100);
            throw ex;
        }

        var cts = new CancellationTokenSource();
        var ex = new TestException();

        try
        {
            var _ = await Delay(ex).WaitAsync(cts.Token);
            Assert.Fail();
        }
        catch (TestException ex2)
        {
            Assert.AreSame(ex, ex2);
        }
    }
}
