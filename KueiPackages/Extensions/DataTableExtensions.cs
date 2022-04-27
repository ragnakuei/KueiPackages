using System.Data;
using System.Reflection;

namespace KueiPackages.Extensions
{
    public static class DataTableExtensions
    {
        /// <summary>
        /// 轉成 DataTable， T 的欄位順序必須要與 user defined table type 的欄位順序一致 !
        /// </summary>
        public static DataTable ToDataTable<T>(this IEnumerable<T> objs)
        {
            var dt = new DataTable();

            var propertyInfos = typeof(T).GetProperties()
                                         .Where(p => CustomAttributeExtensions.GetCustomAttribute<IgnoreDataTableAttribute>(p) == null);

            foreach (var p in propertyInfos)
            {
                var dc = new DataColumn(p.Name, Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType);
                dc.AllowDBNull = true;
                dt.Columns.Add(dc);
            }

            foreach (T entity in objs)
            {
                DataRow dr = dt.NewRow();

                foreach (PropertyInfo p in propertyInfos)
                {
                    var value = p.GetValue(entity);
                    dr[p.Name] = value ?? DBNull.Value;
                }

                dt.Rows.Add(dr);
            }

            return dt;
        }

        public static DataTable Except(this DataTable leftDt, DataTable rightDt, IEqualityComparer<DataRow> comparer = null)
        {
            var result = leftDt.Clone();

            foreach (var leftRow in leftDt.AsEnumerable())
            {
                var jointRow = rightDt.AsEnumerable()
                                      .FirstOrDefault(rightRow => comparer.Equals(leftRow, rightRow));

                if (jointRow == null)
                {
                    result.ImportRow(leftRow);
                }
            }

            return result;
        }

        /// <summary>
        /// 左邊扣掉右邊，再加上，右邊扣掉左邊
        /// </summary>
        public static DataTable MergeByExcept(this DataTable leftDt, DataTable rightDt, IEqualityComparer<DataRow> comparer = null)
        {
            var leftExceptRight = leftDt.Except(rightDt, comparer);
            var rightExceptLeft = rightDt.Except(leftDt, comparer);

            leftExceptRight.Merge(rightExceptLeft);

            return leftExceptRight;
        }

        /// <summary>
        /// 用 Merge + Distinct 的方式
        /// </summary>
        public static DataTable MergeByDistinct(this DataTable leftDt, DataTable rightDt, IEqualityComparer<DataRow> comparer = null)
        {
            var leftDtCloned = leftDt.DefaultView.ToTable();

            leftDtCloned.Merge(rightDt);

            var result = leftDtCloned.AsEnumerable()
                                     .Distinct(comparer)
                                     .CopyToDataTable();

            return result;
        }

        public static DataTable MergeColumns(this DataTable leftDt, DataTable rightDt, IEqualityComparer<DataColumn> comparer = null)
        {
            comparer = comparer ?? new DataColumnComparer();

            var columns = leftDt.Columns.Cast<DataColumn>()
                                .Concat(rightDt.Columns.Cast<DataColumn>())
                                .Distinct(comparer)
                                .ToArray();

            var result = new DataTable();

            foreach (var column in columns)
            {
                result.Columns.Add(column.ColumnName, column.DataType);
            }

            return result;
        }

        public static DataTable LeftJoin(this DataTable leftDt, DataTable rightDt, IEqualityComparer<DataRow> comparer = null)
        {
            var result = leftDt.MergeColumns(rightDt);

            var columns = result.Columns.Cast<DataColumn>()
                                .Select(column => column.ColumnName)
                                .ToArray();
            var leftColumns = new HashSet<string>(leftDt.Columns.Cast<DataColumn>()
                                                        .Select(column => column.ColumnName));

            var rightColumns = new HashSet<string>(rightDt.Columns.Cast<DataColumn>()
                                                          .Select(column => column.ColumnName));


            foreach (var leftRow in leftDt.AsEnumerable())
            {
                var jointRows = rightDt.AsEnumerable()
                                       .Where(row => comparer.Equals(leftRow, row))
                                       .ToArray();

                if (jointRows.Any())
                {
                    foreach (var rightRow in jointRows)
                    {
                        var resultRow = result.NewRow();

                        foreach (var column in columns)
                        {
                            object leftValue = null;

                            if (leftColumns.Contains(column))
                            {
                                leftValue = leftRow.Field<object>(column);
                            }

                            if (leftValue == null
                             && rightColumns.Contains(column))
                            {
                                leftValue = rightRow.Field<object>(column);
                            }

                            if (leftValue != null)
                            {
                                resultRow.SetField(column, leftValue);
                            }
                        }

                        result.Rows.Add(resultRow);
                    }
                }
                else
                {
                    result.ImportRow(leftRow);
                }
            }

            return result;
        }

        public static DataTable LeftJoinAppendRight(this DataTable leftDt, DataTable rightDt, IEqualityComparer<DataRow> comparer = null)
        {
            var result = leftDt.LeftJoin(rightDt, comparer);

            // Append Right
            var appendDt = rightDt.Except(leftDt, comparer);

            foreach (var appendRow in appendDt.AsEnumerable())
            {
                result.ImportRow(appendRow);
            }

            return result;
        }

        public class DataColumnComparer : IEqualityComparer<DataColumn>
        {
            public bool Equals(DataColumn x, DataColumn y)
            {
                return x.ColumnName == y.ColumnName
                    && Equals(x.DataType, y.DataType);
            }

            public int GetHashCode(DataColumn obj)
            {
                unchecked
                {
                    var hashCode = obj.ColumnName.GetHashCode();
                    hashCode = (hashCode * 397) ^ (obj.Caption  != null ? obj.Caption.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (obj.DataType != null ? obj.DataType.GetHashCode() : 0);
                    return hashCode;
                }
            }
        }
    }
}
