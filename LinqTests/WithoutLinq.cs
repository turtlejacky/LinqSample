using System;
using System.Collections.Generic;
using System.Linq;
using LinqTests;

namespace JoeyIsFat
{
    internal static class WithoutLinq
    {
        public static IEnumerable<TSource> JoeyWhere<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<string> ToHttps(IEnumerable<string> urls)
        {
            foreach (var url in urls)
            {
                yield return url.Replace(@"http://", @"https://");
            }
        }

        public static IEnumerable<TResult> JoeySelect<TSource, TResult>(this IEnumerable<TSource> source,
            Func<TSource, TResult> selector)
        {
            foreach (var item in source)
            {
                yield return selector(item);
            }
        }

        public static IEnumerable<TSource> JoeyTake<TSource>(IEnumerable<TSource> employees, int count)
        {
            var enumerator = employees.GetEnumerator();

            var index = 0;

            while (enumerator.MoveNext())
            {
                if (index >= count)
                {
                    yield break;
                }

                yield return enumerator.Current;
                index++;
            }
        }

        public static IEnumerable<TSource> JoeySkip<TSource>(IEnumerable<TSource> employees, int count)
        {
            var enumerator = employees.GetEnumerator();

            var index = 0;

            while (enumerator.MoveNext())
            {
                if (index >= count)
                {
                    yield return enumerator.Current;
                }

                index++;
            }
        }

        public static IEnumerable<int> GetSum<TSource>(IEnumerable<TSource> source, int pageSize,
            Func<TSource, int> sumFunc)
        {
            var rowIndex = 0;

            while (rowIndex < source.Count())
            {
                yield return source.Skip(rowIndex).Take(pageSize).Sum(sumFunc);
                rowIndex += pageSize;
            }
        }

        public static IEnumerable<TSource> TakeWhile<TSource>(IEnumerable<TSource> source, int count,
            Func<TSource, bool> predicate)
        {
            var enumerator = source.GetEnumerator();
            var index = 0;

            while (enumerator.MoveNext() && index < count)
            {
                if (predicate(enumerator.Current))
                {
                    yield return enumerator.Current;
                    index++;
                }
            }
        }

        public static IEnumerable<TSource> SkipWhile<TSource>(IEnumerable<TSource> employees, int count,
            Func<TSource, bool> predicate)
        {
            var enumerator = employees.GetEnumerator();

            var skipCounter = 0;

            while (enumerator.MoveNext())
            {
                if (skipCounter >= count || !predicate(enumerator.Current))
                {
                    yield return enumerator.Current;
                }
                else
                {
                    skipCounter++;
                }
                //if (predicate(enumerator.Current))
                //{
                //}

                //else
                //{
                //    yield return enumerator.Current;
                //}
            }
        }
    }
}