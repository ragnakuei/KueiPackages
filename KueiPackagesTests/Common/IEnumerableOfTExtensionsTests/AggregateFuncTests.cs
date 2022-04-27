namespace KueiPackagesTests.Common.IEnumerableOfTExtensionsTests
{
    public class AggregateFuncTests
    {
        private class Test
        {
            public int Count { get; set; }

            public int Index { get; set; }
        }

        [Test]
        public void ArgsCount1_Case01()
        {
            var actual = Enumerable.Range(1, 3)
                                   .Aggregate((seed, item, index) => seed + item + index);

            // 1 + 2 + 1 => 4
            // 4 + 3 + 2 => 9

            var expected = 9;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ArgsCount2_Case01()
        {
            var actual = Enumerable.Range(1, 3)
                                   .Aggregate(seed: 10,
                                              func: (seed, item, index) => seed + item + index);

            // 10 + 1 + 0 => 11
            // 11 + 2 + 1 => 14
            // 14 + 3 + 2 => 19

            var expected = 19;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ArgsCount3_Case01()
        {
            var actual = Enumerable.Range(2, 3)
                                   .Aggregate(seed: 10,
                                              func: (seed, item, index) => seed + item + index,
                                              resultSelector: (seed) => seed + 10);

            // 10 + 2 + 0 => 12
            // 12 + 3 + 1 => 16
            // 16 + 4 + 2 => 22
            // 22 + 10 => 32

            var expected = 32;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void isBreak_Case01()
        {
            var actual = Enumerable.Range(1, 10)
                                   .Aggregate(100,
                                              (accumulate, item) =>
                                              {
                                                  var iteratorResult = accumulate + item;
                                                  var isBreak        = item % 5 == 0;
                                                  return (iteratorResult, isBreak);
                                              },
                                              accumulate => accumulate - 100);

            var expected = 15;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void isBreak_index_Case01()
        {
            var actual = Enumerable.Range(1, 10)
                                   .Aggregate(100,
                                              (accumulate, item, index) =>
                                              {
                                                  var iteratorResult = accumulate + item + index;
                                                  var isBreak        = item % 5 == 0;
                                                  return (iteratorResult, isBreak);
                                              },
                                              accumulate => accumulate - 100);

            var expected = 25;

            Assert.AreEqual(expected, actual);
        }
    }
}
