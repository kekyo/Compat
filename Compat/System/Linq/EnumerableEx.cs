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
            static IEnumerable<TValue> Iterator(IReadOnlyCollection<TValue> rcollT, int count)
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
            return Iterator(rcollT, count);
        }
        else if (enumerable is IReadOnlyList<TValue> rlistT)
        {
            static IEnumerable<TValue> Iterator(IReadOnlyList<TValue> rlistT, int count)
            {
                var c = Math.Min(rlistT.Count, count);
                var s = rlistT.Count - c;
                for (var index = 0; index < c; index++)
                {
                    yield return rlistT[index + s];
                }
            }
            return Iterator(rlistT, count);
        }
        else
#endif
        if (enumerable is IList<TValue> listT)
        {
            static IEnumerable<TValue> Iterator(IList<TValue> listT, int count)
            {
                var c = Math.Min(listT.Count, count);
                var s = listT.Count - c;
                for (var index = 0; index < c; index++)
                {
                    yield return listT[index + s];
                }
            }
            return Iterator(listT, count);
        }
        else if (enumerable is IList list)
        {
            static IEnumerable<TValue> Iterator(IList list, int count)
            {
                var c = Math.Min(list.Count, count);
                var s = list.Count - c;
                for (var index = 0; index < c; index++)
                {
                    yield return (TValue)list[index + s];
                }
            }
            return Iterator(list, count);
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

#if NET35
    public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(
        this IEnumerable<TFirst> enumerable1,
        IEnumerable<TSecond> enumerable2,
        Func<TFirst, TSecond, TResult> selector)
    {
        using var enumerator1 = enumerable1.GetEnumerator();
        using var enumerator2 = enumerable2.GetEnumerator();

        while (enumerator1.MoveNext() && enumerator2.MoveNext())
        {
            yield return selector(enumerator1.Current, enumerator2.Current);
        }
    }
#endif

#if NETFRAMEWORK || NETSTANDARD || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2
    public static IEnumerable<(TFirst, TSecond)> Zip<TFirst, TSecond>(
        this IEnumerable<TFirst> enumerable1,
        IEnumerable<TSecond> enumerable2)
    {
        using var enumerator1 = enumerable1.GetEnumerator();
        using var enumerator2 = enumerable2.GetEnumerator();

        while (enumerator1.MoveNext() && enumerator2.MoveNext())
        {
            yield return (enumerator1.Current, enumerator2.Current);
        }
    }
#endif

#if NET35 || NET40 || NET45 || NET452 || NET461 || NET462 || NETSTANDARD1_3 || NETSTANDARD1_6 || NETSTANDARD2_0
    public static HashSet<TValue> ToHashSet<TValue>(
        this IEnumerable<TValue> enumerable) =>
        new(enumerable);
#endif
}
