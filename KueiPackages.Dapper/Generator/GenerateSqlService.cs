using System.Collections.Concurrent;
using System.Linq;
using KueiPackages.Dapper.Generator.Models;

namespace KueiPackages.Dapper.Generator
{
    public class GenerateSqlService
    {
        private readonly PropertyInfoService _propertyInfoService;

        private static ConcurrentDictionary<Type, ConcurrentDictionary<SqlScriptType, string>> _typeStore = new();

        public GenerateSqlService()
        {
            _propertyInfoService = new PropertyInfoService();
        }

        public string Generate<T>(SqlScriptType scriptType)
        {
            var type = typeof(T);

            var result = _typeStore.GetValueOrDefault(type)?.GetValueOrDefault(scriptType);

            if (result.IsNullOrWhiteSpace() == false)
            {
                return result;
            }

            _typeStore.TryAdd(type, GenerateSqlScript(type));

            return Generate<T>(scriptType);
        }


        private ConcurrentDictionary<SqlScriptType, string> GenerateSqlScript(Type type)
        {
            var propertyInfoDtos = _propertyInfoService.GetProperties(type);

            return new ConcurrentDictionary<SqlScriptType, string>
                   {
                       [SqlScriptType.Select]              = GenerateSelectScript(type, propertyInfoDtos),
                       [SqlScriptType.Insert]              = GenerateInsertScript(type, propertyInfoDtos),
                       [SqlScriptType.Update]              = GenerateUpdateScript(type, propertyInfoDtos),
                       [SqlScriptType.Delete]              = GenerateDeleteScript(type, propertyInfoDtos),
                       [SqlScriptType.MergeWithDeleteFlag] = GenerateMergeWithDeleteFlagScript(type, propertyInfoDtos),
                       [SqlScriptType.MergeWithDeleteKey]  = GenerateMergeWithDeleteKeyScript(type, propertyInfoDtos),
                   };
        }

        private static IEnumerable<PropertyInfoDto> GetProperties(Dictionary<string, PropertyInfoDto> propertyInfoDtos)
        {
            return propertyInfoDtos.Values.Where(p => p.IsCollection  == false
                                                   && p.IsClassObject == false
                                                   && p.IsNotMapped   == false);
        }

        private string GenerateSelectScript(Type type, Dictionary<string, PropertyInfoDto> propertyInfoDtos)
        {
            var columns = GetProperties(propertyInfoDtos)
                         .Select(p => p.PropertyName)
                         .Join(", ");

            var result = @$"SELECT {columns}
FROM [dbo].[{type.Name}];";

            return result;
        }

        private string GenerateInsertScript(Type type, Dictionary<string, PropertyInfoDto> propertyInfoDtos)
        {
            var columns = GetProperties(propertyInfoDtos)
                         .Select(p => p.PropertyName)
                         .Join(", ");

            var parameters = GetProperties(propertyInfoDtos)
                            .Select(p => "@" + p.PropertyName)
                            .Join(", ");

            var result = @$"INSERT INTO [dbo].[{ type.Name }] ({ columns })
VALUES ({ parameters });";

            return result;
        }

        private string GenerateUpdateScript(Type type, Dictionary<string, PropertyInfoDto> propertyInfoDtos)
        {
            var properties = GetProperties(propertyInfoDtos);

            var nonKeyColumns = properties.Where(p => p.HasColumnTypeSn == false)
                                          .Select(p => $"{p.PropertyName} = @{p.PropertyName}")
                                          .Join(", ");

            var keyColumns = properties.Where(p => p.HasColumnTypeSn)
                                       .Select(p => $"{p.PropertyName} = @{p.PropertyName}")
                                       .Join(" AND ");

            var result = @$"UPDATE [dbo].[{type.Name}]
SET {nonKeyColumns}
WHERE {keyColumns};";

            return result;
        }

        private string GenerateDeleteScript(Type type, Dictionary<string, PropertyInfoDto> propertyInfoDtos)
        {
            var keyColumns = GetProperties(propertyInfoDtos).Where(p => p.HasColumnTypeSn)
                                                            .Select(p => $"{p.PropertyName} = @{p.PropertyName}")
                                                            .Join(" AND ");

            var result = @$"DELETE [dbo].[{type.Name}]
WHERE {keyColumns};";

            return result;
        }

        private string GenerateMergeWithDeleteFlagScript(Type type, Dictionary<string, PropertyInfoDto> propertyInfoDtos)
        {
            var properties = GetProperties(propertyInfoDtos);

            var keyColumns = properties.Where(p => p.HasColumnTypeSn).ToArray();

            var deleteFlagColumns = properties.Where(p => p.HasColumnTypeDeleteFlag).ToArray();

            var mergeConditionDeleteFlag = deleteFlagColumns.Select(p => $"[t].[{p.PropertyName}] = @Active{p.PropertyName}")
                                                            .Join(@" AND ");

            var mergeCondition = keyColumns.Select(p => $"[t].[{p.PropertyName}] = [s].[{p.PropertyName}]")
                                           .Join(@" AND ")
                               + "\r\n    AND "
                               + mergeConditionDeleteFlag;

            var updateColumns = properties.Where(p => p.HasColumnTypeSn == false)
                                          .Select(p => $"[t].[{p.PropertyName}] = [s].[{p.PropertyName}]")
                                          .Join(",\r\n        ");

            var insertColumns = properties.Select(p => $"[{p.PropertyName}]")
                                          .Join(",\r\n            ");

            var insertColumnValues = properties.Select(p => $"[s].[{p.PropertyName}]")
                                               .Join(",\r\n            ");

            var deleteAdditionCondition = properties.Where(p => p.HasColumnTypeFn)
                                                        .Select(p => $"[t].[{p.PropertyName}] = @Delete{p.PropertyName}")
                                                        .Join(" AND ");

            var updateToDelete = deleteFlagColumns
                                .Select(p => $"[t].[{p.PropertyName}] = @{p.PropertyName}")
                                .Join(", ");

            var result = $@"MERGE [dbo].[{type.Name}] [t]
USING @{type.Name} [s]
ON {mergeCondition}
WHEN MATCHED
    THEN
    UPDATE
    SET {updateColumns}
WHEN NOT MATCHED BY TARGET
    THEN
    INSERT ({insertColumns})
    VALUES ({insertColumnValues})
WHEN NOT MATCHED BY SOURCE 
    AND {deleteAdditionCondition}
    THEN
    UPDATE
    SET {updateToDelete};";
            return result;
        }
        private string GenerateMergeWithDeleteKeyScript(Type type, Dictionary<string, PropertyInfoDto> propertyInfoDtos)
        {
            var properties = GetProperties(propertyInfoDtos);

            var keyColumns = properties.Where(p => p.HasColumnTypeSn).ToArray();

            var deleteFlagColumns = properties.Where(p => p.HasColumnTypeDeleteFlag).ToArray();

            var mergeCondition = keyColumns.Select(p => $"[t].[{p.PropertyName}] = [s].[{p.PropertyName}]")
                                           .Join(@" AND ");

            var updateColumns = properties.Where(p => p.HasColumnTypeSn == false)
                                          .Select(p => $"[t].[{p.PropertyName}] = [s].[{p.PropertyName}]")
                                          .Join(",\r\n        ");

            var insertColumns = properties.Select(p => $"[{p.PropertyName}]")
                                          .Join(",\r\n            ");

            var insertColumnValues = properties.Select(p => $"[s].[{p.PropertyName}]")
                                               .Join(",\r\n            ");

            var deleteAdditionCondition = properties.Where(p => p.HasColumnTypeFn)
                                                        .Select(p => $"[t].[{p.PropertyName}] = @Delete{p.PropertyName}")
                                                        .Join(" AND ");

            var result = $@"MERGE [dbo].[{type.Name}] [t]
USING @{type.Name} [s]
ON {mergeCondition}
WHEN MATCHED
    THEN
    UPDATE
    SET {updateColumns}
WHEN NOT MATCHED BY TARGET
    THEN
    INSERT ({insertColumns})
    VALUES ({insertColumnValues})
WHEN NOT MATCHED BY SOURCE 
    AND {deleteAdditionCondition}
    THEN
    DELETE;";
            return result;
        }
    }
}
