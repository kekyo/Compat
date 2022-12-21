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

public sealed class ValueTaskTests
{
    private sealed class TestException : Exception
    {
    }

    [Test]
    public async Task Void()
    {
        await new ValueTask();
    }

    [Test]
    public async Task CompletedTask()
    {
        await new ValueTask(Task.Factory.CompletedTask());
    }

    [Test]
    public async Task Exception()
    {
        var actual = new TestException();
        try
        {
            await new ValueTask(Task.Factory.FromException(actual));
            Assert.Fail();
        }
        catch (TestException ex)
        {
            Assert.AreSame(ex, actual);
        }
    }

    [Test]
    public async Task Value()
    {
        var actual = await new ValueTask<int>(111);

        Assert.AreEqual(111, actual);
    }

    [Test]
    public async Task ValuedTask()
    {
        var actual = await new ValueTask<int>(TaskEx.FromResult(111));

        Assert.AreEqual(111, actual);
    }

    [Test]
    public async Task ExceptionT()
    {
        var tcs = new TaskCompletionSource<int>();
        var actual = new TestException();
        try
        {
            tcs.SetException(actual);
            await new ValueTask<int>(tcs.Task);
            Assert.Fail();
        }
        catch (TestException ex)
        {
            Assert.AreSame(ex, actual);
        }
    }

    [Test]
    public async Task AsTask()
    {
        await new ValueTask().AsTask();
    }

    [Test]
    public async Task AsTaskT()
    {
        var actual = await new ValueTask<int>(111).AsTask();

        Assert.AreEqual(111, actual);
    }
}
