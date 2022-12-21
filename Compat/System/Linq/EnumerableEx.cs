////////////////////////////////////////////////////////////////////////////
//
// Compat - Totally makes compatibility layer for .NET platforms.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;

namespace System.Linq;

#if NETFRAMEWORK || NETSTANDARD1_3 || NETSTANDARD1_6 || NETSTANDARD2_0

public static class EnumerableEx
{
#if NET35 || NET40 || NET45 || NET452 || NET461 || NET462 || NETSTANDARD1_3
    public static IEnumerable<TValue> Append<TValue>(
        this IEnumerable<TValue> enumerable, TValue value)
    {
        foreach (var item in enumerable)
        {
            yield return item;
        }
        yield return value;
    }

    public static IEnumerable<TValue> Prepend<TValue>(
        this IEnumerable<TValue> enumerable, TValue value)
    {
        yield return value;
        foreach (var item in enumerable)
        {
            yield return item;
        }
    }
#endif

#if NETFRAMEWORK || NETSTANDARD1_3 || NETSTANDARD1_6 || NETSTANDARD2_0
    public static IEnumerable<TValue> TakeLast<TValue>(
        this IEnumerable<TValue> enumerable, int count)
    {
#if NET45 || NET452 || NET461 || NET462 || NETSTANDARD1_3
        if (enumerable is IReadOnlyCollection<TValue> rcollT)
        {
            IEnumerable<TValue> Iterator(IReadOnlyCollection<TValue> rcollT)
            {
                var c = Math.Min(rcollT.Count, count);
                var s = rcollT.Count - c;

                using var enumerator = rcollT.GetEnumerator();

                var index = 0;
                while (index < s && enumerator.MoveNext())
                {
                    index++;
                }

                index = 0;
                while (index < c && enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                    index++;
                }
            }
            return Iterator(rcollT);
        }
        else
#endif
        if (enumerable is ICollection<TValue> collT)
        {
            var c = Math.Min(collT.Count, count);
            var s = collT.Count - c;
            var arr = new TValue[c];
            collT.CopyTo(arr, s);
            return arr;
        }
        else if (enumerable is ICollection coll)
        {
            var c = Math.Min(coll.Count, count);
            var s = coll.Count - c;
            var arr = new TValue[c];
            coll.CopyTo(arr, s);
            return arr;
        }
        else
        {
            var q = new Queue<TValue>();

            foreach (var item in enumerable)
            {
                q.Enqueue(item);
                if (q.Count > count)
                {
                    q.Dequeue();
                }
            }

            return q;
        }
    }

    public static IEnumerable<TValue> SkipLast<TValue>(
        this IEnumerable<TValue> enumerable, int count)
    {
        var q = new Queue<TValue>();
        var results = new List<TValue>();

        foreach (var item in enumerable)
        {
            q.Enqueue(item);
            if (q.Count > count)
            {
                results.Add(q.Dequeue());
            }
        }

        return results;
    }
#endif

#if NET35 || NET40 || NET45 || NET452 || NET461 || NET462 || NETSTANDARD1_3 || NETSTANDARD1_6 || NETSTANDARD2_0
    public static HashSet<TValue> ToHashSet<TValue>(
        this IEnumerable<TValue> enumerable) =>
        new(enumerable);
#endif
}

#endif