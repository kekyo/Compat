////////////////////////////////////////////////////////////////////////////
//
// Compat - Totally makes compatibility layer for .NET platforms.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

#if !NET6_0_OR_GREATER

namespace System.Threading.Tasks;

public static class TaskExtension
{
    public static Task WaitAsync(
        this Task task,
        CancellationToken ct)
    {
        if (!task.IsCompleted)
        {
            var tcs = new TaskCompletionSource<bool>();
            var ctr = ct.Register(() => tcs.TrySetCanceled());

            task.ContinueWith(t =>
            {
                if (t.IsCompleted)
                {
                    tcs.TrySetResult(true);
                }
                else if (t.IsCanceled)
                {
                    tcs.TrySetCanceled();
                }
                else
                {
                    tcs.TrySetException(t.Exception!);
                }

                ctr.Dispose();
            });

            return tcs.Task;
        }
        else
        {
            return task;
        }
    }

    public static Task<TResult> WaitAsync<TResult>(
        this Task<TResult> task,
        CancellationToken ct)
    {
        if (!task.IsCompleted)
        {
            var tcs = new TaskCompletionSource<TResult>();
            var ctr = ct.Register(() => tcs.TrySetCanceled());

            task.ContinueWith(t =>
            {
                if (t.IsCompleted)
                {
                    tcs.TrySetResult(t.GetAwaiter().GetResult());
                }
                else if (t.IsCanceled)
                {
                    tcs.TrySetCanceled();
                }
                else
                {
                    tcs.TrySetException(t.Exception!);
                }

                ctr.Dispose();
            });

            return tcs.Task;
        }
        else
        {
            return task;
        }
    }
}

#endif
