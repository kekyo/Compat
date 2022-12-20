////////////////////////////////////////////////////////////////////////////
//
// Compat - Totally makes compatibility layer for .NET platforms.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

#if NET35

namespace System;

public readonly struct ValueTuple
{
}

public readonly struct ValueTuple<T1>
{
    public readonly T1 Item1;

    public ValueTuple(T1 item1) =>
        this.Item1 = item1;
}

public readonly struct ValueTuple<T1, T2>
{
    public readonly T1 Item1;
    public readonly T2 Item2;

    public ValueTuple(T1 item1, T2 item2)
    {
        this.Item1 = item1;
        this.Item2 = item2;
    }
}

public readonly struct ValueTuple<T1, T2, T3>
{
    public readonly T1 Item1;
    public readonly T2 Item2;
    public readonly T3 Item3;

    public ValueTuple(T1 item1, T2 item2, T3 item3)
    {
        this.Item1 = item1;
        this.Item2 = item2;
        this.Item3 = item3;
    }
}

public readonly struct ValueTuple<T1, T2, T3, T4>
{
    public readonly T1 Item1;
    public readonly T2 Item2;
    public readonly T3 Item3;
    public readonly T4 Item4;

    public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4)
    {
        this.Item1 = item1;
        this.Item2 = item2;
        this.Item3 = item3;
        this.Item4 = item4;
    }
}

#endif
