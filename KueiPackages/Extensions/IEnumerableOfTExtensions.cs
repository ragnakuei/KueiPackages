namespace KueiPackages.Extensions
{
    public static class IEnumerableOfTExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null)
            {
                return;
            }

            foreach (T obj in source)
            {
                action.Invoke(obj);
            }
        }

        public static void ForEach<TElement>(this IEnumerable<TElement> source, Action<TElement, int> action)
        {
            int index = 0;

            foreach (var item in source)
            {
                action.Invoke(item, index);
                index++;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <param name="func">第三個引數是 Index，從 1 開始執行</param>
        /// <typeparam name="TElement"></typeparam>
        /// <returns></returns>
        public static TElement Aggregate<TElement>(this IEnumerable<TElement>              source,
                                                   Func<TElement, TElement, int, TElement> func)
        {
            var index = 1;

            var result = source.Aggregate((accumulate, item) =>
                                          {
                                              accumulate = func.Invoke(accumulate, item, index);
                                              index++;
                                              return accumulate;
                                          });

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <param name="seed"></param>
        /// <param name="func">第三個引數是 Index，從 0 開始執行</param>
        /// <typeparam name="TElement"></typeparam>
        /// <typeparam name="TAccumulate"></typeparam>
        /// <returns></returns>
        public static TAccumulate Aggregate<TElement, TAccumulate>(this IEnumerable<TElement>                    source,
                                                                   TAccumulate                                   seed,
                                                                   Func<TAccumulate, TElement, int, TAccumulate> func)
        {
            var index = 0;

            var result = source.Aggregate(seed: seed,
                                          func: (seed, item) =>
                                                {
                                                    seed = func.Invoke(seed, item, index);
                                                    index++;
                                                    return seed;
                                                });

            return result;
        }

        public static TResult Aggregate<TSource, TAccumulate, TResult>(this IEnumerable<TSource>                    source,
                                                                       TAccumulate                                  seed,
                                                                       Func<TAccumulate, TSource, int, TAccumulate> func,
                                                                       Func<TAccumulate, TResult>                   resultSelector)
        {
            var index = 0;

            var result = source.Aggregate(seed: seed,
                                          func: (accumulate, item) =>
                                                {
                                                    accumulate = func.Invoke(accumulate, item, index);
                                                    index++;
                                                    return accumulate;
                                                },
                                          resultSelector: accumulate => resultSelector.Invoke(accumulate));

            return result;
        }

        public static TElement Aggregate<TElement>(this IEnumerable<TElement>                                    source,
                                                   Func<TElement, TElement, (TElement accumulate, bool isBreak)> func)
        {
            var accumulate = source.FirstOrDefault();

            foreach (var item in source.Skip(1))
            {
                var iteratorResult = func.Invoke(accumulate, item);

                accumulate = iteratorResult.accumulate;

                if (iteratorResult.isBreak)
                {
                    break;
                }
            }

            return accumulate;
        }

        public static TResult Aggregate<TElement, TResult>(this IEnumerable<TElement>                                    source,
                                                           Func<TElement, TElement, (TElement accumulate, bool isBreak)> func,
                                                           Func<TElement, TResult>                                       resultSelector)
        {
            var accumulate = source.FirstOrDefault();

            foreach (var item in source.Skip(1))
            {
                var iteratorResult = func.Invoke(accumulate, item);

                accumulate = iteratorResult.accumulate;

                if (iteratorResult.isBreak)
                {
                    break;
                }
            }

            var result = resultSelector.Invoke(accumulate);

            return result;
        }

        public static TResult Aggregate<TElement, TAccumulate, TResult>(this IEnumerable<TElement>                                               source,
                                                                        TAccumulate                                                              seed,
                                                                        Func<TAccumulate, TElement, int, (TAccumulate accumulate, bool isBreak)> func,
                                                                        Func<TAccumulate, TResult>                                               resultSelector)
        {
            var index      = 0;
            var accumulate = seed;

            foreach (var item in source)
            {
                var iteratorResult = func.Invoke(accumulate, item, index);

                accumulate = iteratorResult.accumulate;
                index++;

                if (iteratorResult.isBreak)
                {
                    break;
                }
            }

            var result = resultSelector.Invoke(accumulate);

            return result;
        }

        public static TResult Aggregate<TElement, TAccumulate, TResult>(this IEnumerable<TElement>                                          source,
                                                                        TAccumulate                                                         seed,
                                                                        Func<TAccumulate, TElement, (TAccumulate accumulate, bool isBreak)> func,
                                                                        Func<TAccumulate, TResult>                                          resultSelector)
        {
            var accumulate = seed;

            foreach (var item in source)
            {
                var iteratorResult = func.Invoke(accumulate, item);

                accumulate = iteratorResult.accumulate;

                if (iteratorResult.isBreak)
                {
                    break;
                }
            }

            var result = resultSelector.Invoke(accumulate);

            return result;
        }

        /// <summary>
        /// 將集合資料分頁
        /// </summary>
        public static IEnumerable<IEnumerable<T>> ToPaged<T>(this IEnumerable<T> source, int size = 99)
        {
            if (source == null)
            {
                yield return Enumerable.Empty<T>();
                yield break;
            }

            var index      = 0;
            var chunkItems = new T[size];
            foreach (var item in source)
            {
                if (index == size)
                {
                    yield return chunkItems;

                    chunkItems = new T[size];
                    index      = 0;
                }

                chunkItems[index++] = item;
            }

            // Tak 用來去掉因為 new T[size] 額外產生的不必要 element
            yield return chunkItems.Take(index);
        }

        /// <summary>
        /// GroupBy + ToDictionary
        /// </summary>
        public static Dictionary<TKey, List<TElement>> GroupByToDictionary<TKey, TElement>(this IEnumerable<TElement> source, Func<TElement, TKey> keySelector)
        {
            var result = new Dictionary<TKey, List<TElement>>();

            foreach (var element in source)
            {
                if (keySelector.Invoke(element) is not TKey elementKey)
                {
                    continue;
                }

                if (result.TryGetValue(elementKey, out var values))
                {
                    values.Add(element);
                }
                else
                {
                    result.Add(elementKey, new List<TElement> { element });
                }
            }

            return result;
        }

        /// <summary>
        /// GroupBy + ToDictionary
        /// </summary>
        public static Dictionary<TKey1, Dictionary<TKey2, List<TElement>>> GroupByToDictionary<TKey1, TKey2, TElement>(this IEnumerable<TElement> source,
                                                                                                                       Func<TElement, TKey1>      keySelector1,
                                                                                                                       Func<TElement, TKey2>      keySelector2)
        {
            var result = new Dictionary<TKey1, Dictionary<TKey2, List<TElement>>>();

            var groupByKey1 = source.GroupByToDictionary(keySelector1);

            foreach (var key1Values in groupByKey1)
            {
                var groupByKey2 = key1Values.Value.GroupByToDictionary(keySelector2);

                foreach (var key2Values in groupByKey2)
                {
                    if (result.TryGetValue(key1Values.Key, out var step3Result))
                    {
                        step3Result.Add(key2Values.Key, key2Values.Value);
                    }
                    else
                    {
                        result.Add(key1Values.Key, new Dictionary<TKey2, List<TElement>> { { key2Values.Key, key2Values.Value } });
                    }
                }
            }

            return result;
        }

        public static HashSet<TElement> ToHashSet45<TElement>(this IEnumerable<TElement> source)
        {
            return new HashSet<TElement>(source);
        }

        public static IEnumerable<T> ExceptNoDistinct<T>(this IEnumerable<T>  source,
                                                         IEnumerable<T>       target,
                                                         IEqualityComparer<T> equalityComparer = null)
        {
            var targetDistinct = new HashSet<T>(target);

            foreach (var sItem in source)
            {
                if (targetDistinct.Contains(sItem, equalityComparer) == false)
                {
                    yield return sItem;
                }
            }
        }

        public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, IEnumerable<T> filter, IEqualityComparer<T> equalityComparer = null)
        {
            equalityComparer ??= EqualityComparer<T>.Default;

            foreach (var item in source)
            {
                foreach (var filterItem in filter)
                {
                    if (equalityComparer.Equals(item, filterItem))
                    {
                        yield return item;
                    }
                }
            }
        }

        public static IEnumerable<T> Filter<T, TFilter>(this IEnumerable<T> source, IEnumerable<TFilter> filter, Func<T, TFilter, bool> predicate)
        {
            foreach (var item in source)
            {
                foreach (var filterItem in filter)
                {
                    if (predicate(item, filterItem))
                    {
                        yield return item;
                    }
                }
            }
        }

        public static bool Contains<T>(this IEnumerable<T>  source,
                                              IEnumerable<T>       filter,
                                              IEqualityComparer<T> equalityComparer = null)
        {
            equalityComparer ??= EqualityComparer<T>.Default;

            foreach (var item in source)
            {
                foreach (var filterItem in filter)
                {
                    if (equalityComparer.Equals(item, filterItem))
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        public static bool Contains<T, TFilter>(this IEnumerable<T>    source,
                                                       IEnumerable<TFilter>   filter,
                                                       Func<T, TFilter, bool> predicate)
        {
            foreach (var item in source)
            {
                foreach (var filterItem in filter)
                {
                    if (predicate(item, filterItem))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
