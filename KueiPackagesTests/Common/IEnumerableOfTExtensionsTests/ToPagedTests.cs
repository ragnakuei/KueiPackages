namespace KueiPackagesTests.Common.IEnumerableOfTExtensionsTests
{
    public class ToPagedTests
    {
        [Test]
        public void 十筆資料_3筆一組()
        {
            var actual = Enumerable.Range(1, 10)
                                   .ToPaged(3);

            var expected = new List<List<int>>
                           {
                               new List<int> { 1, 2, 3 },
                               new List<int> { 4, 5, 6 },
                               new List<int> { 7, 8, 9 },
                               new List<int> { 10 },
                           };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void 九筆資料_3筆一組()
        {
            var actual = Enumerable.Range(1, 9)
                                   .ToPaged(3);

            var expected = new List<List<int>>
                           {
                               new List<int> { 1, 2, 3 },
                               new List<int> { 4, 5, 6 },
                               new List<int> { 7, 8, 9 },
                           };

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
