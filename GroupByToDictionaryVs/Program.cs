using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace GroupByToDictionaryVs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<TestRunner>();
        }
    }


    public class TestRunner
    {
        private class Test
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public int? ParentId { get; set; }
        }

        private IEnumerable<Test> GetTests()
            => new[]
               {
                   new Test { Id = 1, Name = "A", ParentId  = null },
                   new Test { Id = 2, Name = "B", ParentId  = null },
                   new Test { Id = 3, Name = "C", ParentId  = null },
                   new Test { Id = 4, Name = "A1", ParentId = 1 },
                   new Test { Id = 5, Name = "A2", ParentId = 1 },
                   new Test { Id = 6, Name = "B1", ParentId = 2 },
                   new Test { Id = 7, Name = "B2", ParentId = 2 },
                   new Test { Id = 8, Name = "B3", ParentId = 2 },
               };

        public TestRunner()
        {
        }

        [Benchmark]
        public void LinqGroupByToDictionary() => GetTests().LinqGroupByToDictionary(t => t.ParentId);

        [Benchmark]
        public void GroupByToDictionary01() => GetTests().GroupByToDictionary01(t => t.ParentId);

        [Benchmark]
        public void GroupByToDictionary02() => GetTests().GroupByToDictionary02(t => t.ParentId);
    }

    public static class IEnumerableOfTExtensions
    {
        /// <summary>
        /// GroupBy + ToDictionary
        /// </summary>
        public static Dictionary<TKey, List<TElement>> GroupByToDictionary01<TKey, TElement>(this IEnumerable<TElement> source, [NotNull]Func<TElement, TKey> keySelector)
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
        public static Dictionary<TKey, List<TElement>> GroupByToDictionary02<TKey, TElement>(this IEnumerable<TElement> source, [NotNull]Func<TElement, TKey> keySelector)
        {
            var result = new Dictionary<TKey, List<TElement>>();

            var hashKeys = new HashSet<TKey>();

            foreach (var element in source)
            {
                if (keySelector.Invoke(element) is not TKey elementKey)
                {
                    continue;
                }

                if (hashKeys.Contains(elementKey))
                {
                    result[elementKey].Add(element);
                }
                else
                {
                    hashKeys.Add(elementKey);
                    result.Add(elementKey, new List<TElement> { element });
                }
            }

            return result;
        }

        /// <summary>
        /// GroupBy + ToDictionary
        /// </summary>
        internal static Dictionary<TKey, List<TElement>> LinqGroupByToDictionary<TKey, TElement>(this IEnumerable<TElement> source, Func<TElement, TKey> keySelector)
        {
            return source.Where(s => keySelector.Invoke(s) != null)
                         .GroupBy(keySelector)
                         .ToDictionary(kv => kv.Key,
                                       kv => kv.ToList());
        }
    }
}
