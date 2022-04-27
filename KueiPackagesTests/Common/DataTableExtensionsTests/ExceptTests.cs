namespace KueiPackagesTests.Common.DataTableExtensionsTests
{
    public class ExceptTests
    {
        [Test]
        public void TwoColumn()
        {
            var leftDt = new DataTable();
            leftDt.Columns.Add("Id",   typeof(int));
            leftDt.Columns.Add("Name", typeof(string));
            leftDt.Columns.Add("Age",  typeof(int));
            leftDt.Rows.Add(1, "John", 18);
            leftDt.Rows.Add(2, "Mary", 20);
            leftDt.Rows.Add(3, "Mike", null);

            var rightDt = new DataTable();
            rightDt.Columns.Add("Id",   typeof(int));
            rightDt.Columns.Add("Name", typeof(string));
            rightDt.Columns.Add("Age",  typeof(int));
            rightDt.Rows.Add(2, "Mary", 21);
            rightDt.Rows.Add(2, "Mary", 22);
            rightDt.Rows.Add(3, "Mike", 23);
            rightDt.Rows.Add(4, "John", 20);

            var actual = leftDt.Except(rightDt, new IdNameDataRowComparer())
                               .AsEnumerable()
                               .Select(r => new
                                            {
                                                Id   = r.Field<int>("Id"),
                                                Name = r.Field<string>("Name"),
                                                Age  = r.Field<int?>("Age"),
                                            })
                               .ToArray();

            var expected = new[]
                           {
                               new { Id = 1, Name = "John", Age = 18 },
                           };

            actual.Should().BeEquivalentTo(expected);
        }

        public class IdNameDataRowComparer : IEqualityComparer<DataRow>
        {
            public bool Equals(DataRow x, DataRow y)
            {
                var xId   = x.Field<int?>("Id");
                var yId   = y.Field<int?>("Id");
                var xName = x.Field<string>("Name");
                var yName = y.Field<string>("Name");

                return xId   == yId
                    && xName == yName;
            }

            public int GetHashCode(DataRow obj)
            {
                unchecked
                {
                    var hashCode = (obj.RowError != null ? obj.RowError.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (int)obj.RowState;
                    hashCode = (hashCode * 397) ^ obj.Table.GetHashCode();
                    hashCode = (hashCode * 397) ^ obj.ItemArray.GetHashCode();
                    hashCode = (hashCode * 397) ^ obj.HasErrors.GetHashCode();
                    return hashCode;
                }
            }
        }
    }
}
