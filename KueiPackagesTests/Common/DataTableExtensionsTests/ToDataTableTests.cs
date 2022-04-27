namespace KueiPackagesTests.Common.DataTableExtensionsTests
{
    public class ToDataTableTests
    {
        private class Test
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        [Test]
        public void RowTests()
        {
            var ts = new List<Test>
                     {
                         new() { Id = 1, Name = "A" },
                         new() { Id = 2, Name = "B" },
                     };

            var actual = new List<DataRow>();

            foreach (DataRow row in ts.ToDataTable().Rows)
            {
                actual.Add(row);
            }

            var expected = new[]
                           {
                               new { ItemArray = new object[] { 1, "A" } },
                               new { ItemArray = new object[] { 2, "B" } },
                           };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void ColumnTests()
        {
            var ts = new List<Test>
                     {
                         new() { Id = 1, Name = "A" },
                         new() { Id = 2, Name = "B" },
                     };

            var actual = new List<DataColumn>();

            foreach (DataColumn column in ts.ToDataTable().Columns)
            {
                actual.Add(column);
            }

            var expected = new[]
                           {
                               new { ColumnName = "Id" },
                               new { ColumnName = "Name" },
                           };

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
