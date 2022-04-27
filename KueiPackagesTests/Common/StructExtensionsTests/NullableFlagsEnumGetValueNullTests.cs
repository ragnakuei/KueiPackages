namespace KueiPackagesTests.Common.StructExtensionsTests
{
    public class NullableFlagsEnumGetValueNullTests
    {
        [Flags]
        private enum Test
        {
            A = 1,
            B = 2,
            C = 4,
        }

        [Test]
        public void 回傳Null()
        {
            Test? t = null;

            var actual = t.GetValueOrNull();

            var expected = (Test?)null;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void 回傳符合項目()
        {
            Test? t = Test.A;

            var actual = t.GetValueOrNull();

            var expected = Test.A;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void 回傳不符合項目()
        {
            Test? t = (Test?)8;

            var actual = t.GetValueOrNull();

            var expected = (Test?)8;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void 回傳交集項目()
        {
            Test? t = (Test?)3;

            var actual = t.GetValueOrNull();

            var expected = Test.A | Test.B;

            Assert.AreEqual(expected, actual);
        }
    }
}
