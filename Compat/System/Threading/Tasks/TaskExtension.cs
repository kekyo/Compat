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
    private static bool OnCompleted<T>(
        Task task, TaskCompletionSource<T> tcs)
    {
        if (task.IsCanceled)
        {
            tcs.TrySetCanceled();
            return false;
        }
        else if (task.IsFaulted)
        {
            // Unwrap
            if (task.Exception is { } aex && aex.InnerExceptions.Count == 1)
            {
                tcs.TrySetException(aex.InnerExceptions[0]);
            }
            else
            {
                tcs.TrySetException(task.Exception!);
            }
            return false;
        }
        else
        {
            return true;
        }
    }

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
                if (OnCompleted(t, tcs))
                {
                    tcs.TrySetResult(true);
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
                if (OnCompleted(t, tcs))
                {
                    tcs.TrySetResult(t.Result);
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
