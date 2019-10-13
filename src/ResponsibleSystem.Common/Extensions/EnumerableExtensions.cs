using System;
using System.Collections.Generic;
using System.Linq;

namespace ResponsibleSystem.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Distinct<T, TKey>(this IEnumerable<T> list, Func<T, TKey> lookup) where TKey : struct
        {
            return list.Distinct(new StructEqualityComparer<T, TKey>(lookup));
        }

        class StructEqualityComparer<T, TKey> : IEqualityComparer<T> where TKey : struct
        {
            Func<T, TKey> lookup;

            public StructEqualityComparer(Func<T, TKey> lookup)
            {
                this.lookup = lookup;
            }

            public bool Equals(T x, T y)
            {
                return lookup(x).Equals(lookup(y));
            }

            public int GetHashCode(T obj)
            {
                return lookup(obj).GetHashCode();
            }
        }

        /// <summary>
        /// Returns distinct elements from a sequence by the provided key selector function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TKey">The type of the elements of the key selector function.</typeparam>
        /// <param name="source">The sequence to remove duplicate elements from.</param>
        /// <param name="keySelector">The function that returns the key that the sequence will be filtered by.</param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.DistinctBy(keySelector, null);
        }

        /// <summary>
        /// Returns distinct elements from a sequence by the provided key selector function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TKey">The type of the elements of the key selector function.</typeparam>
        /// <param name="source">The sequence to remove duplicate elements from.</param>
        /// <param name="keySelector">The function that returns the key that the sequence will be filtered by.</param>
        /// <param name="comparer">An equality comparer to compare values, or <c>null</c> to use a default comparer.</param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            if (comparer == null)
            {
                comparer = EqualityComparer<TKey>.Default;
            }

            HashSet<TKey> seenKeys = new HashSet<TKey>(comparer);

            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
