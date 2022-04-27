namespace KueiPackagesTests.Common.DictionaryExtensionsTests
{
    public class Dictionary_TKeyNullableStruct_TValueNullableStruct_GetValueOrNullTests
    {
        private enum TestKey
        {
            A = 0,
            B = 1,
        }

        private enum TestValue
        {
            A = 0,
            B = 1,
        }

        private Dictionary<TestKey?, TestValue?> _map
            = new()
              {
                  [TestKey.A] = TestValue.A,
                  [TestKey.B] = TestValue.B,
              };

        [Test]
        public void Key為Null_回傳Null()
        {
            var actual = _map.GetValueOrNull(null);

            TestValue? expected = null;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void 回傳符合項目()
        {
            var actual = _map.GetValueOrNull(TestKey.A);

            var expected = TestValue.A;

            Assert.AreEqual(expected, actual);
        }
    }
}
