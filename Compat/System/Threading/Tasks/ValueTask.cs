////////////////////////////////////////////////////////////////////////////
//
// Compat - Totally makes compatibility layer for .NET platforms.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

#if NET35 || NET40 || NET45 || NET452 || NETSTANDARD1_3 || NETSTANDARD1_6

namespace System.Threading.Tasks;

public readonly struct ValueTask
{
    private readonly Task? task;

    public ValueTask(Task task) =>
        this.task = task;

    public bool IsCompleted =>
        this.task?.IsCompleted ?? true;

    public bool IsCanceled =>
        this.task?.IsCanceled ?? false;

    public bool IsFaulted =>
        this.task?.IsFaulted ?? false;

    public Task AsTask() =>
        this.task ?? Task.Factory.CompletedTask();

    public ValueTaskAwaiter GetAwaiter() =>
        new(this.task);
}

public readonly struct ValueTask<TValue>
{
    private readonly TValue value;
    private readonly Task<TValue>? task;

    public ValueTask(TValue value) =>
        this.value = value;

    public ValueTask(Task<TValue> task)
    {
        this.value = default!;
        this.task = task;
    }

    public bool IsCompleted =>
        this.task?.IsCompleted ?? true;

    public bool IsCanceled =>
        this.task?.IsCanceled ?? false;

    public bool IsFaulted =>
        this.task?.IsFaulted ?? false;

    public Task<TValue> AsTask() =>
        this.task ?? Task.Factory.FromResult(this.value);

    public ValueTaskAwaiter<TValue> GetAwaiter() =>
        new(this.value, this.task);
}

#endif
