////////////////////////////////////////////////////////////////////////////
//
// Compat - Totally makes compatibility layer for .NET platforms.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

#if NET35 || NET40 || NET45 || NET452 || NETSTANDARD1_3 || NETSTANDARD1_6

using System.Runtime.CompilerServices;

namespace System.Threading.Tasks;

public readonly struct ValueTaskAwaiter : INotifyCompletion
{
    private readonly Task? task;

    public ValueTaskAwaiter(Task? task) =>
        this.task = task;

    public bool IsCompleted =>
        this.task?.IsCompleted ?? true;

    public void OnCompleted(Action continuation) =>
        continuation();

    void INotifyCompletion.OnCompleted(Action continuation) =>
        continuation();

    public void GetResult()
    {
        if (this.task is { })
        {
            this.task.GetAwaiter().GetResult();
        }
    }
}

public readonly struct ValueTaskAwaiter<TValue> : INotifyCompletion
{
    private readonly TValue value;
    private readonly Task<TValue>? task;

    public ValueTaskAwaiter(TValue value, Task<TValue>? task)
    {
        this.value = value;
        this.task = task;
    }

    public bool IsCompleted =>
        this.task?.IsCompleted ?? true;

    public void OnCompleted(Action continuation) =>
        continuation();

    void INotifyCompletion.OnCompleted(Action continuation) =>
        continuation();

    public TValue GetResult() =>
        this.task is { } t ? t.GetAwaiter().GetResult() : this.value;
}

#endif
