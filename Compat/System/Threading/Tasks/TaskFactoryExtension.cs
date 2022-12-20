////////////////////////////////////////////////////////////////////////////
//
// Compat - Totally makes compatibility layer for .NET platforms.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

namespace System.Threading.Tasks;

public static class TaskFactoryExtension
{
    public static Task<T> FromResult<T>(this TaskFactory _, T value) =>
        TaskEx.FromResult(value);

    public static Task FromException(this TaskFactory _, Exception ex)
    {
#if NET35 || NET40 || NET45 || NET452
        var tcs = new TaskCompletionSource<bool>();
        tcs.SetException(ex);
        return tcs.Task;
#else
        return Task.FromException(ex);
#endif
    }

    public static Task CompletedTask(this TaskFactory _) =>
#if NET35 || NET40
        TaskEx.FromResult(true);
#elif NET45 || NET452
        Task.FromResult(true);
#else
        Task.CompletedTask;
#endif
}
