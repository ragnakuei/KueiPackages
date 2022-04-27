namespace KueiPackagesTests.Common.IEnumerableOfTExtensionsTests
{
    public class FilterTests
    {
        [Test]
        public void UseEqualityComparerTests()
        {
            var actual = Enumerable.Range(1, 10)
                                   .Filter(Enumerable.Range(1, 3))
                                   .ToArray();

            var expected = new[] { 1, 2, 3 };

            actual.Should().BeEquivalentTo(expected);
        }

        private class TestDto
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        [Test]
        public void UsePredicate()
        {
            var actual = new[]
                         {
                             new TestDto { Id = 1, Name = "A" },
                             new TestDto { Id = 2, Name = "B" },
                             new TestDto { Id = 3, Name = "C" },
                             new TestDto { Id = 4, Name = "D" },
                             new TestDto { Id = 5, Name = "E" },
                         }.Filter(new[] { 1, 2, 3 }, (x, y) => x.Id == y)
                          .ToArray();

            var expected = new[]
                           {
                               new TestDto { Id = 1, Name = "A" },
                               new TestDto { Id = 2, Name = "B" },
                               new TestDto { Id = 3, Name = "C" },
                           };

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
