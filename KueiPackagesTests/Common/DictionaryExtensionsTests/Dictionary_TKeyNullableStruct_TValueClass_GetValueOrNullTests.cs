namespace KueiPackagesTests.Common.DictionaryExtensionsTests
{
    public class Dictionary_TKeyNullableStruct_TValueClass_GetValueOrNullTests
    {
        private enum TestKey
        {
            A = 0,
            B = 1,
        }

        private class Test
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        private Dictionary<TestKey?, Test> _map
            = new()
              {
                  [TestKey.A] = new Test { Id = 1, Name = "A" },
                  [TestKey.B] = new Test { Id = 2, Name = "B" },
              };

        [Test]
        public void Key為Null_回傳Null()
        {
            var actual = _map.GetValueOrNull(null);

            Test expected = null;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void 回傳符合項目()
        {
            var actual = _map.GetValueOrNull(TestKey.A);

            var expected = new Test { Id = 1, Name = "A" };

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
