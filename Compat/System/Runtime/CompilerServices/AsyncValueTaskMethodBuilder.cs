////////////////////////////////////////////////////////////////////////////
//
// Compat - Totally makes compatibility layer for .NET platforms.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;

namespace System.Runtime.CompilerServices;

#if NET35 || NET40 || NETSTANDARD1_3 || NETSTANDARD1_6

public struct AsyncValueTaskMethodBuilder
{
    private static readonly TaskCompletionSource<bool> completedGuard = new();

    private TaskCompletionSource<bool>? tcs;

    public static AsyncValueTaskMethodBuilder Create() =>
        default;

    public ValueTask Task
    {
        get
        {
            if (this.tcs == completedGuard)
            {
                return new();
            }
            else
            {
                this.tcs ??= new();
                return new(this.tcs.Task);
            }
        }
    }

    public void Start<TStateMachine>(ref TStateMachine stateMachine)
        where TStateMachine : IAsyncStateMachine
    {
        var sc = SynchronizationContext.Current;
        try
        {
            stateMachine.MoveNext();
        }
        catch
        {
            SynchronizationContext.SetSynchronizationContext(sc);
            throw;
        }

        SynchronizationContext.SetSynchronizationContext(sc);
    }

    public void SetStateMachine(IAsyncStateMachine stateMachine)
    {
    }

    public void SetResult()
    {
        if (this.tcs != null)
        {
            this.tcs.SetResult(true);
        }
        else
        {
            this.tcs = completedGuard;
        }
    }

    public void SetException(Exception ex)
    {
        this.tcs ??= new();

        Debug.Assert(this.tcs != completedGuard);

        if (ex is OperationCanceledException)
        {
            this.tcs.SetCanceled();
        }
        else
        {
            this.tcs.SetException(ex);
        }
    }

    internal static void RethrowAsynchronously(Exception ex)
    {
#if NET35 || NET40
        ThreadPool.QueueUserWorkItem(p =>
        {
            var ex = (Exception)p!;
            throw new TargetInvocationException(ex);
        }, ex);
#elif NETSTANDARD1_3 || NETSTANDARD1_6
        var edi = System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(ex);
        TaskEx.Run(() => edi.Throw());
#else
        var edi = System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(ex);
        ThreadPool.QueueUserWorkItem(p =>
        {
            var edi = (System.Runtime.ExceptionServices.ExceptionDispatchInfo)p!;
            edi.Throw();
        }, edi);
#endif
    }

    public void AwaitOnCompleted<TAwaiter, TStateMachine>(
        ref TAwaiter awaiter, ref TStateMachine stateMachine)
        where TAwaiter : ICriticalNotifyCompletion
        where TStateMachine : IAsyncStateMachine
    {
        this.tcs ??= new();

        // Will make boxed copy myself (inside of state machine).
        IAsyncStateMachine boxed = stateMachine;

        //Debug.Assert(boxed.builder.tcs != null);

        try
        {
            awaiter.OnCompleted(boxed.MoveNext);
        }
        catch (Exception ex)
        {
            RethrowAsynchronously(ex);
        }
    }

    public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
        ref TAwaiter awaiter, ref TStateMachine stateMachine)
        where TAwaiter : ICriticalNotifyCompletion
        where TStateMachine : IAsyncStateMachine
    {
        this.tcs ??= new();

        // Will make boxed copy myself (inside of state machine).
        IAsyncStateMachine boxed = stateMachine;

        //Debug.Assert(boxed.builder.tcs != null);

        try
        {
            awaiter.UnsafeOnCompleted(boxed.MoveNext);
        }
        catch (Exception ex)
        {
            RethrowAsynchronously(ex);
        }
    }
}

public struct AsyncValueTaskMethodBuilder<TResult>
{
    private static readonly TaskCompletionSource<TResult> completedGuard = new();

    private TaskCompletionSource<TResult>? tcs;
    private TResult value;

    public static AsyncValueTaskMethodBuilder<TResult> Create() =>
        default;

    public ValueTask<TResult> Task
    {
        get
        {
            if (this.tcs == completedGuard)
            {
                return new(this.value);
            }
            else
            {
                this.tcs ??= new();
                return new(this.tcs.Task);
            }
        }
    }

    public void Start<TStateMachine>(ref TStateMachine stateMachine)
        where TStateMachine : IAsyncStateMachine
    {
        var sc = SynchronizationContext.Current;
        try
        {
            stateMachine.MoveNext();
        }
        catch
        {
            SynchronizationContext.SetSynchronizationContext(sc);
            throw;
        }

        SynchronizationContext.SetSynchronizationContext(sc);
    }

    public void SetStateMachine(IAsyncStateMachine stateMachine)
    {
    }

    public void SetResult(TResult value)
    {
        if (this.tcs != null)
        {
            this.tcs.SetResult(value);
        }
        else
        {
            this.value = value;
            this.tcs = completedGuard;
        }
    }

    public void SetException(Exception ex)
    {
        this.tcs ??= new();

        Debug.Assert(this.tcs != completedGuard);

        if (ex is OperationCanceledException)
        {
            this.tcs.SetCanceled();
        }
        else
        {
            this.tcs.SetException(ex);
        }
    }

    public void AwaitOnCompleted<TAwaiter, TStateMachine>(
        ref TAwaiter awaiter, ref TStateMachine stateMachine)
        where TAwaiter : ICriticalNotifyCompletion
        where TStateMachine : IAsyncStateMachine
    {
        this.tcs ??= new();

        // Will make boxed copy myself (inside of state machine).
        IAsyncStateMachine boxed = stateMachine;

        //Debug.Assert(boxed.builder.tcs != null);

        try
        {
            awaiter.OnCompleted(boxed.MoveNext);
        }
        catch (Exception ex)
        {
            AsyncValueTaskMethodBuilder.RethrowAsynchronously(ex);
        }
    }

    public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
        ref TAwaiter awaiter, ref TStateMachine stateMachine)
        where TAwaiter : ICriticalNotifyCompletion
        where TStateMachine : IAsyncStateMachine
    {
        this.tcs ??= new();

        // Will make boxed copy myself (inside of state machine).
        IAsyncStateMachine boxed = stateMachine;

        //Debug.Assert(boxed.builder.tcs != null);

        try
        {
            awaiter.UnsafeOnCompleted(boxed.MoveNext);
        }
        catch (Exception ex)
        {
            AsyncValueTaskMethodBuilder.RethrowAsynchronously(ex);
        }
    }
}

#endif
