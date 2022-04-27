namespace KueiPackagesTests.Common.IEnumerableOfTExtensionsTests
{
    public class ForEachTests
    {
        private class Test
        {
            public int Count { get; set; }
        }

        [Test]
        public void Case01()
        {
            var actual = new List<Test>();

            Enumerable.Range(1, 2)
                      .ForEach(i => actual.Add(new Test { Count = i }));

            var expected = new List<Test>()
                           {
                               new() { Count = 1 },
                               new() { Count = 2 },
                           };

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
