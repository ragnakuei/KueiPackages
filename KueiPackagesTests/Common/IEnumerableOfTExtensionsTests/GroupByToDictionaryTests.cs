namespace KueiPackagesTests.Common.IEnumerableOfTExtensionsTests
{
    public class GroupByToDictionaryTests
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

        [Test]
        public void Case01()
        {
            var actual = GetTests().GroupByToDictionary(t => t.ParentId);

            var expected = new Dictionary<int?, List<Test>>
                           {
                               [1] = new()
                                     {
                                         new Test { Id = 4, Name = "A1", ParentId = 1 },
                                         new Test { Id = 5, Name = "A2", ParentId = 1 },
                                     },
                               [2] = new()
                                     {
                                         new Test { Id = 6, Name = "B1", ParentId = 2 },
                                         new Test { Id = 7, Name = "B2", ParentId = 2 },
                                         new Test { Id = 8, Name = "B3", ParentId = 2 },
                                     },
                           };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Case02()
        {
            var actual = GetTests().GroupByToDictionary(t => t.ParentId);

            var expected = new Dictionary<int?, List<Test>>
                           {
                               [1] = new()
                                     {
                                         new Test { Id = 4, Name = "A1", ParentId = 1 },
                                         new Test { Id = 5, Name = "A2", ParentId = 1 },
                                     },
                               [2] = new()
                                     {
                                         new Test { Id = 6, Name = "B1", ParentId = 2 },
                                         new Test { Id = 7, Name = "B2", ParentId = 2 },
                                         new Test { Id = 8, Name = "B3", ParentId = 2 },
                                     },
                           };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void LinqGroupByToDictionary_Case01()
        {
            var actual = GetTests().GroupByToDictionary(t => t.ParentId);

            var expected = new Dictionary<int?, List<Test>>
                           {
                               [1] = new()
                                     {
                                         new Test { Id = 4, Name = "A1", ParentId = 1 },
                                         new Test { Id = 5, Name = "A2", ParentId = 1 },
                                     },
                               [2] = new()
                                     {
                                         new Test { Id = 6, Name = "B1", ParentId = 2 },
                                         new Test { Id = 7, Name = "B2", ParentId = 2 },
                                         new Test { Id = 8, Name = "B3", ParentId = 2 },
                                     },
                           };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void LinqGroupByToDictionary_Case02()
        {
            var actual = GetTests().GroupByToDictionary(t => t.ParentId);

            var expected = new Dictionary<int?, List<Test>>
                           {
                               [1] = new()
                                     {
                                         new Test { Id = 4, Name = "A1", ParentId = 1 },
                                         new Test { Id = 5, Name = "A2", ParentId = 1 },
                                     },
                               [2] = new()
                                     {
                                         new Test { Id = 6, Name = "B1", ParentId = 2 },
                                         new Test { Id = 7, Name = "B2", ParentId = 2 },
                                         new Test { Id = 8, Name = "B3", ParentId = 2 },
                                     },
                           };

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
