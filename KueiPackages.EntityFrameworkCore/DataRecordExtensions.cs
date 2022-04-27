using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace KueiPackages.EntityFrameworkCore
{
    public static class DataRecordExtensions
    {
        private static readonly ConcurrentDictionary<Type, object> _materializers = new();

        /// <summary>
        /// <remarks>如果後方直接接 FirstOrDefault() 就會產生 Bug</remarks>
        /// </summary>
        public static IEnumerable<T> Translate<T>(this DbDataReader reader) where T : new()
        {
            var materialize = GetMaterialize<T>();
            return reader.Translate(materialize);
        }

        public static T TranslateFirstOrDefault<T>(this DbDataReader reader) where T : new()
        {
            var materialize = GetMaterialize<T>();
            return reader.TranslateFirstOrDefault(materialize);
        }

        private static Func<IDataRecord, T> GetMaterialize<T>() where T : new()
        {
            var materialize  = (Func<IDataRecord, T>)Materializer.Materialize<T>;
            var getOrAddFunc = (Func<IDataRecord, T>)_materializers.GetOrAdd(typeof(T), materialize);
            return getOrAddFunc;
        }

        private static IEnumerable<T> Translate<T>(this DbDataReader    reader,
                                                   Func<IDataRecord, T> objectMaterializer)
        {
            while (reader.Read())
            {
                var obj = objectMaterializer.Invoke(reader);
                yield return obj;
            }

            var hasNextResult = reader.NextResult();
        }

        private static T TranslateFirstOrDefault<T>(this DbDataReader    reader,
                                                    Func<IDataRecord, T> objectMaterializer)
        {
            reader.Read();

            var obj = objectMaterializer.Invoke(reader);

            var hasNextResult = reader.NextResult();

            return obj;
        }
    }
}
