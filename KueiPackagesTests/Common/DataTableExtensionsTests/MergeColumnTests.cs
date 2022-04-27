namespace KueiPackagesTests.Common.DataTableExtensionsTests
{
    public class MergeColumnTests
    {
        [Test]
        public void RowTests()
        {
            var leftDt = new DataTable();
            leftDt.Columns.Add("Id",   typeof(int));
            leftDt.Columns.Add("Name", typeof(string));
            leftDt.Columns.Add("Age",  typeof(int));

            var rightDt = new DataTable();
            rightDt.Columns.Add("Id",      typeof(int));
            rightDt.Columns.Add("Age",     typeof(int));
            rightDt.Columns.Add("Address", typeof(string));

            var actual = leftDt.MergeColumns(rightDt)
                               .Columns.Cast<DataColumn>()
                               .Select(c => new
                                            {
                                                c.ColumnName,
                                                c.DataType,
                                            })
                               .ToArray();

            var expected = new[]
                           {
                               new { ColumnName = "Id", DataType      = typeof(int) },
                               new { ColumnName = "Name", DataType    = typeof(string) },
                               new { ColumnName = "Age", DataType     = typeof(int) },
                               new { ColumnName = "Address", DataType = typeof(string) },
                           };

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
