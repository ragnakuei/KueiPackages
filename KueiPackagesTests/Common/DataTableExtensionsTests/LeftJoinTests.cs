namespace KueiPackagesTests.Common.DataTableExtensionsTests
{
    public class LeftJoinTests
    {
        [Test]
        public void OneColumn()
        {
            var leftDt = new DataTable();
            leftDt.Columns.Add("Id",   typeof(int));
            leftDt.Columns.Add("Name", typeof(string));
            leftDt.Rows.Add(1, "John");
            leftDt.Rows.Add(2, "Mary");
            leftDt.Rows.Add(3, "Mike");

            var rightDt = new DataTable();
            rightDt.Columns.Add("Id",  typeof(int));
            rightDt.Columns.Add("Age", typeof(int));
            rightDt.Rows.Add(2, 21);
            rightDt.Rows.Add(2, 22);
            rightDt.Rows.Add(3, 23);
            rightDt.Rows.Add(4, 20);

            var actual = leftDt.LeftJoin(rightDt, new IdDataRowComparer())
                               .AsEnumerable()
                               .Select(r => new
                                            {
                                                Id   = r.Field<int>("Id"),
                                                Name = r.Field<string>("Name"),
                                                Age  = r.Field<int?>("Age")
                                            })
                               .ToArray();

            var expected = new[]
                           {
                               new { Id = 1, Name = "John", Age = (int?)null },
                               new { Id = 2, Name = "Mary", Age = (int?)21 },
                               new { Id = 2, Name = "Mary", Age = (int?)22 },
                               new { Id = 3, Name = "Mike", Age = (int?)23 },
                           };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void TwoColumn()
        {
            var leftDt = new DataTable();
            leftDt.Columns.Add("Id",      typeof(int));
            leftDt.Columns.Add("Name",    typeof(string));
            leftDt.Columns.Add("Address", typeof(string));
            leftDt.Rows.Add(1, "John", "A1");
            leftDt.Rows.Add(2, "Mary", "A2");
            leftDt.Rows.Add(3, "Mike", "A3");

            var rightDt = new DataTable();
            rightDt.Columns.Add("Id",   typeof(int));
            rightDt.Columns.Add("Name", typeof(string));
            rightDt.Columns.Add("Age",  typeof(int));
            rightDt.Rows.Add(2, "Mary", 21);
            rightDt.Rows.Add(2, "Mary", 22);
            rightDt.Rows.Add(3, "Mike", 23);
            rightDt.Rows.Add(4, "John", 20);

            var actual = leftDt.LeftJoin(rightDt, new IdNameDataRowComparer())
                               .AsEnumerable()
                               .Select(r => new
                                            {
                                                Id      = r.Field<int>("Id"),
                                                Name    = r.Field<string>("Name"),
                                                Age     = r.Field<int?>("Age"),
                                                Address = r.Field<string>("Address")
                                            })
                               .ToArray();

            var expected = new[]
                           {
                               new { Id = 1, Name = "John", Age = (int?)null, Address = "A1", },
                               new { Id = 2, Name = "Mary", Age = (int?)21, Address   = "A2", },
                               new { Id = 2, Name = "Mary", Age = (int?)22, Address   = "A2", },
                               new { Id = 3, Name = "Mike", Age = (int?)23, Address   = "A3", },
                           };

            actual.Should().BeEquivalentTo(expected);
        }

        public class IdDataRowComparer : IEqualityComparer<DataRow>
        {
            public bool Equals(DataRow x, DataRow y)
            {
                var xId = x.Field<int?>("Id");
                var yId = y.Field<int?>("Id");

                return xId == yId;
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
