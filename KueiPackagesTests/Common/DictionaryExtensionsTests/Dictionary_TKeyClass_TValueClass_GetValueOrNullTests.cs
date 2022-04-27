namespace KueiPackagesTests.Common.DictionaryExtensionsTests
{
    public class Dictionary_TKeyClass_TValueClass_GetValueOrNullTests
    {
        private class TestKey
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        private class TestValue
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        private static TestKey _testKeyA = new() { Id = 1, Name = "A" };
        private static TestKey _testKeyB = new() { Id = 2, Name = "B" };

        private Dictionary<TestKey, TestValue> _map
            = new()
              {
                  [_testKeyA] = new TestValue { Id = 1, Name = "A" },
                  [_testKeyB] = new TestValue { Id = 2, Name = "B" },
              };

        [Test]
        public void Key為Null_回傳Null()
        {
            var       actual   = _map.GetValueOrNull(null);
            TestValue expected = null;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void 回傳符合項目()
        {
            var actual   = _map.GetValueOrNull(_testKeyA);
            var expected = new TestValue { Id = 1, Name = "A" };
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
