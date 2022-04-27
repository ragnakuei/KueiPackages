using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace KueiPackages.EntityFrameworkCore
{
    internal class Materializer
    {
        public static T Materialize<T>(IDataRecord record) where T : new()
        {
            var t = new T();
            foreach (var prop in typeof(T).GetProperties())
            {
                // 1). If entity reference, bypass it.
                if (prop.PropertyType.Namespace == typeof(T).Namespace)
                {
                    continue;
                }

                // 2). If ICollection Or IEnumerable , bypass it.
                if (prop.PropertyType is Type propertyType
                 && propertyType.IsGenericType
                 && (
                        propertyType.GetGenericTypeDefinition() == typeof(ICollection<>)
                     || propertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                    ))
                {
                    continue;
                }

                // 4). If property is NotMapped, bypass it.
                if (Attribute.IsDefined(prop, typeof(NotMappedAttribute)))
                {
                    continue;
                }

                try
                {
                    var a = record.FieldCount;

                    var dbValue = record[prop.Name];

                    if (dbValue is DBNull)
                    {
                        continue;
                    }

                    if (prop.PropertyType.IsConstructedGenericType
                     && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        var baseType  = prop.PropertyType.GetGenericArguments()[0];
                        var baseValue = Convert.ChangeType(dbValue, baseType);
                        var value     = Activator.CreateInstance(prop.PropertyType, baseValue);
                        prop.SetValue(t, value);
                    }
                    else
                    {
                        var value = Convert.ChangeType(dbValue, prop.PropertyType);
                        prop.SetValue(t, value);
                    }
                }
                catch (Exception)
                {
                    return default;
                }
            }

            return t;
        }
    }
}
