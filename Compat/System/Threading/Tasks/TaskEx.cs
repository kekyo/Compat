////////////////////////////////////////////////////////////////////////////
//
// Compat - Totally makes compatibility layer for .NET platforms.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;

namespace System.Threading.Tasks;

#if !(NET35 || NET40)

public static class TaskEx
{
    public static Task<T> FromResult<T>(T value) =>
        Task.FromResult(value);

    public static Task FromException(Exception ex)
    {
#if NET45 || NET452
        var tcs = new TaskCompletionSource<bool>();
        tcs.SetException(ex);
        return tcs.Task;
#else
        return Task.FromException(ex);
#endif
    }

    public static Task CompletedTask
    {
#if NET45 || NET452
        get => Task.FromResult(true);
#else
        get => Task.CompletedTask;
#endif
    }

    public static Task Delay(TimeSpan delay) =>
        Task.Delay(delay);

    public static Task Delay(int millisecondsDelay) =>
        Task.Delay(millisecondsDelay);

    public static Task WhenAll(params Task[] tasks) =>
        Task.WhenAll(tasks);

    public static Task WhenAll(IEnumerable<Task> enumerable) =>
        Task.WhenAll(enumerable);

    public static Task<T[]> WhenAll<T>(params Task<T>[] tasks) =>
        Task.WhenAll(tasks);

    public static Task<T[]> WhenAll<T>(IEnumerable<Task<T>> enumerable) =>
        Task.WhenAll(enumerable);

    public static Task<Task> WhenAny(params Task[] tasks) =>
        Task.WhenAny(tasks);

    public static Task<Task> WhenAny(IEnumerable<Task> enumerable) =>
        Task.WhenAny(enumerable);

    public static Task<Task<T>> WhenAny<T>(params Task<T>[] tasks) =>
        Task.WhenAny(tasks);

    public static Task<Task<T>> WhenAny<T>(IEnumerable<Task<T>> enumerable) =>
        Task.WhenAny(enumerable);

    public static Task<T> Run<T>(Func<T> action) =>
        Task.Run(action);
}
#endif
