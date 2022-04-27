namespace KueiPackagesTests.Common.IEnumerableOfTExtensionsTests
{
    public class ExceptNoDistinctTests
    {
        private class Test
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public int? ParentId { get; set; }
        }

        private sealed class IdEqualityComparer : IEqualityComparer<Test>
        {
            public bool Equals(Test x, Test y)
            {
                if (ReferenceEquals(x, y))
                {
                    return true;
                }

                if (ReferenceEquals(x, null))
                {
                    return false;
                }

                if (ReferenceEquals(y, null))
                {
                    return false;
                }

                if (x.GetType() != y.GetType())
                {
                    return false;
                }

                return x.Id == y.Id;
            }

            public int GetHashCode(Test obj)
            {
                return obj.Id;
            }
        }

        [Test]
        public void Case01()
        {
            var actual = new[] { 1, 1, 2, 3 }.ExceptNoDistinct(new[] { 2, 3 }).ToArray();

            var expected = new[] { 1, 1 };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Case02()
        {
            var actual = new[]
                         {
                             new Test { Id = 1, Name = "A", ParentId = null },
                             new Test { Id = 2, Name = "B", ParentId = null },
                             new Test { Id = 3, Name = "C", ParentId = null },
                         }.ExceptNoDistinct(new[] { new Test { Id = 1, Name = "A", ParentId = null } },
                                            new IdEqualityComparer());

            var expected = new[]
                           {
                               new Test { Id = 2, Name = "B", ParentId = null },
                               new Test { Id = 3, Name = "C", ParentId = null },
                           };

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
