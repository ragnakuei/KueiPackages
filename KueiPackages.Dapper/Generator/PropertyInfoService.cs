using System.Collections.Concurrent;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using KueiPackages.Dapper.Generator.Models;

namespace KueiPackages.Dapper.Generator
{
    public class PropertyInfoService
    {
        private static ConcurrentDictionary<Type, Dictionary<string, PropertyInfoDto>> _typeStore = new();

        public Dictionary<string, PropertyInfoDto> GetProperties<T>()
        {
            return GetProperties(typeof(T));
        }

        public Dictionary<string, PropertyInfoDto> GetProperties(Type type)
        {
            var result = _typeStore.GetValueOrDefault(type);

            if (result != null)
            {
                return result;
            }

            result = type.GetProperties()
                         .Select(p =>
                                 {
                                     var dto = new PropertyInfoDto();
                                     dto.PropertyName = p.Name;
                                     dto.PropertyInfo = p;
                                     dto.ElementType = new[]
                                                       {
                                                           p.PropertyType.GetElementType(),
                                                           p.PropertyType.GetGenericArguments().FirstOrDefault(gt => gt.IsValueType == false)
                                                       }.FirstOrDefault(t => t != null);

                                     dto.DisplayName = p.GetCustomAttributes(typeof(DisplayNameAttribute), false).Cast<DisplayNameAttribute>().FirstOrDefault()?.DisplayName;
                                     
                                     dto.IsNotMapped = p.GetCustomAttributes(typeof(NotMappedAttribute), false).Any();

                                     dto.ColumnAttributes = p.GetCustomAttributes(typeof(ColumnAttribute), false).Cast<ColumnAttribute>().ToArray();

                                     dto.IsClassObject = dto.IsCollection                          == false
                                                      && dto.PropertyInfo.PropertyType.IsValueType == false
                                                      && dto.PropertyInfo.PropertyType             != typeof(string);

                                     if (dto.IsClassObject)
                                     {
                                         dto.ObjectType = dto.PropertyInfo.PropertyType;
                                     }

                                     return dto;
                                 })
                         .ToDictionary(k => k.PropertyName,
                                       v => v);

            _typeStore[type] = result;

            return result;
        }
    }
}
