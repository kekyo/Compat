////////////////////////////////////////////////////////////////////////////
//
// Compat - Totally makes compatibility layer for .NET platforms.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

using NUnit.Framework;
using Lepracaun;

namespace System.Threading.Tasks;

// Async method lacks 'await' operators and will run synchronously
#pragma warning disable CS1998

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

    ///////////////////////////////////////////////////////////////////

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

    ///////////////////////////////////////////////////////////////////

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

    ///////////////////////////////////////////////////////////////////

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

    ///////////////////////////////////////////////////////////////////

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

    ///////////////////////////////////////////////////////////////////

    [Test]
    public void IsCompleted1()
    {
        var task = new ValueTask();

        Assert.IsTrue(task.IsCompleted);
    }

    [Test]
    public void IsCompleted2()
    {
        var tcs = new TaskCompletionSource<int>();
        var task = new ValueTask(tcs.Task);

        Assert.IsFalse(task.IsCompleted);
    }

    [Test]
    public void IsCompletedT1()
    {
        var task = new ValueTask<int>(111);

        Assert.IsTrue(task.IsCompleted);
    }

    [Test]
    public void IsCompletedT2()
    {
        var tcs = new TaskCompletionSource<int>();
        var task = new ValueTask<int>(tcs.Task);

        Assert.IsFalse(task.IsCompleted);
    }

    ///////////////////////////////////////////////////////////////////

    [Test]
    public void IsCanceled1()
    {
        var task = new ValueTask();

        Assert.IsFalse(task.IsCanceled);
    }

    [Test]
    public void IsCanceled2()
    {
        var tcs = new TaskCompletionSource<int>();
        tcs.SetCanceled();
        var task = new ValueTask(tcs.Task);

        Assert.IsTrue(task.IsCanceled);
    }

    [Test]
    public void IsCanceledT1()
    {
        var task = new ValueTask<int>(111);

        Assert.IsFalse(task.IsCanceled);
    }

    [Test]
    public void IsCanceledT2()
    {
        var tcs = new TaskCompletionSource<int>();
        tcs.SetCanceled();
        var task = new ValueTask<int>(tcs.Task);

        Assert.IsTrue(task.IsCanceled);
    }

    ///////////////////////////////////////////////////////////////////

    [Test]
    public void IsFaulted1()
    {
        var task = new ValueTask();

        Assert.IsFalse(task.IsFaulted);
    }

    [Test]
    public void IsFaulted2()
    {
        var tcs = new TaskCompletionSource<int>();
        tcs.SetException(new TestException());
        var task = new ValueTask(tcs.Task);

        Assert.IsTrue(task.IsFaulted);
    }

    [Test]
    public void IsFaultedT1()
    {
        var task = new ValueTask<int>(111);

        Assert.IsFalse(task.IsFaulted);
    }

    [Test]
    public void IsFaultedT2()
    {
        var tcs = new TaskCompletionSource<int>();
        tcs.SetException(new TestException());
        var task = new ValueTask<int>(tcs.Task);

        Assert.IsTrue(task.IsFaulted);
    }

    ///////////////////////////////////////////////////////////////////

    [Test]
    public async Task AwaitableStateMachineInImmediateReturn()
    {
        static async ValueTask Immediate()
        {
        }

        await Immediate();
    }

    [Test]
    public async Task AwaitableStateMachineInDelayedReturn()
    {
        static async ValueTask Delay()
        {
            await TaskEx.Delay(100);
        }

        await Delay();
    }

    [Test]
    public async Task AwaitableStateMachineInImmediateThrow()
    {
        static async ValueTask Immediate(Exception ex)
        {
            throw ex;
        }

        var ex = new TestException();
        try
        {
            await Immediate(ex);
            Assert.Fail();
        }
        catch (TestException ex2)
        {
            Assert.AreSame(ex, ex2);
        }
    }

    [Test]
    public async Task AwaitableStateMachineInDelayedThrow()
    {
        static async ValueTask Delay(Exception ex)
        {
            await TaskEx.Delay(100);
            throw ex;
        }

        var ex = new TestException();
        try
        {
            await Delay(ex);
            Assert.Fail();
        }
        catch (TestException ex2)
        {
            Assert.AreSame(ex, ex2);
        }
    }

    [Test]
    public async Task AwaitableStateMachineInSynchContextImmediate()
    {
        var expected = Thread.CurrentThread.ManagedThreadId;

        async ValueTask Run()
        {
            var actual = Thread.CurrentThread.ManagedThreadId;
            Assert.AreEqual(expected, actual);
        }

        var app = new Application();
        app.Run(Run().AsTask());
    }

    [Test]
    public async Task AwaitableStateMachineInSynchContextDelay()
    {
        var expected = Thread.CurrentThread.ManagedThreadId;

        async ValueTask Run()
        {
            await TaskEx.Delay(100);
            var actual = Thread.CurrentThread.ManagedThreadId;
            Assert.AreEqual(expected, actual);
        }

        var app = new Application();
        app.Run(Run().AsTask());
    }

    ///////////////////////////////////////////////////////////////////

    [Test]
    public async Task AwaitableStateMachineTInImmediateReturn()
    {
        static async ValueTask<int> Immediate()
        {
            return 111;
        }

        var actual = await Immediate();

        Assert.AreEqual(111, actual);
    }

    [Test]
    public async Task AwaitableStateMachineTInDelayedReturn()
    {
        static async ValueTask<int> Delay()
        {
            await TaskEx.Delay(100);
            return 111;
        }

        var actual = await Delay();

        Assert.AreEqual(111, actual);
    }

    [Test]
    public async Task AwaitableStateMachineTInImmediateThrow()
    {
        static async ValueTask<int> Immediate(Exception ex)
        {
            throw ex;
        }

        var ex = new TestException();
        try
        {
            var _ = await Immediate(ex);
            Assert.Fail();
        }
        catch (TestException ex2)
        {
            Assert.AreSame(ex, ex2);
        }
    }

    [Test]
    public async Task AwaitableStateMachineTInDelayedThrow()
    {
        static async ValueTask<int> Delay(Exception ex)
        {
            await TaskEx.Delay(100);
            throw ex;
        }

        var ex = new TestException();
        try
        {
            var _ = await Delay(ex);
            Assert.Fail();
        }
        catch (TestException ex2)
        {
            Assert.AreSame(ex, ex2);
        }
    }

    [Test]
    public async Task AwaitableStateMachineTInSynchContextImmediate()
    {
        var expected = Thread.CurrentThread.ManagedThreadId;

        async ValueTask<int> Run()
        {
            var actual = Thread.CurrentThread.ManagedThreadId;
            Assert.AreEqual(expected, actual);

            return 111;
        }

        var app = new Application();
        app.Run(Run().AsTask());
    }

    [Test]
    public async Task AwaitableStateMachineTInSynchContextDelay()
    {
        var expected = Thread.CurrentThread.ManagedThreadId;

        async ValueTask<int> Run()
        {
            await TaskEx.Delay(100);
            var actual = Thread.CurrentThread.ManagedThreadId;
            Assert.AreEqual(expected, actual);

            return 111;
        }

        var app = new Application();
        app.Run(Run().AsTask());
    }
}
