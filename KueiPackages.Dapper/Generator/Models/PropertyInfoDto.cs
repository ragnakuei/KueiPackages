using System.Linq;
using System.Reflection;

namespace KueiPackages.Dapper.Generator.Models
{
    public class PropertyInfoDto
    {
        public string PropertyName { get; set; }

        public PropertyInfo PropertyInfo { get; set; }

        public bool IsCollection => ElementType != null;

        /// <summary>
        /// 是否有加上 NotMapped
        /// </summary>
        public bool IsNotMapped { get; set; }

        /// <summary>
        /// 用於 IsCollection = true 時，取出 IEnumerable of T 的 typeof(T)
        /// </summary>
        public Type ElementType { get; set; }

        public bool IsClassObject { get; set; }


        /// <summary>
        /// 用於 IsObject = true 時，取出 Object 的 Type
        /// </summary>
        public Type ObjectType { get; set; }

        /// <summary>
        /// 所定義的 ColumnAttributes
        /// </summary>
        public ColumnAttribute[] ColumnAttributes { get; set; }

        public bool HasColumnTypeSn => ColumnAttributes.Any(c => c.ColumnType == ColumnType.Sn);

        public bool HasColumnTypeFn => ColumnAttributes.Any(c => c.ColumnType == ColumnType.Fn);

        public bool HasColumnTypeDeleteFlag => ColumnAttributes.Any(c => c.ColumnType == ColumnType.DeleteFlag);

        public string DisplayName { get; set; }
    }
}
