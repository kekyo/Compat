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

public sealed class TaskFactoryExtebsionTests
{
    private sealed class TestException : Exception
    {
    }

    [Test]
    public async Task FromResult()
    {
        var actual = await Task.Factory.FromResult(100);
        Assert.AreEqual(100, actual);
    }

    [Test]
    public async Task FromException()
    {
        var actual = new TestException();
        try
        {
            await Task.Factory.FromException(actual);
            Assert.Fail();
        }
        catch (TestException ex)
        {
            Assert.AreSame(ex, actual);
        }
    }

    [Test]
    public async Task CompletedTask()
    {
        await Task.Factory.CompletedTask();
    }
}
