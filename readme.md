## KueiPackages

- Extensions
  - IEnumerable\<T>

    - ToDataTable()
    - ForEach()
    - Aggregate()
    - ToPaged()
    - GroupByToDictionary()
    - ToHashSet45()
    - ExceptNoDistinct()
    - Filter()
    - PartialContains()
    - Contains()

  - Decimal

    - ToFix()
    - ToFixAndFillTailZero()
    - Pow()
    - Sqrt()
    - StDev()

  - Dictionary\<TKey?, TValue?>()

    - GetValueOrNull()

  - ConcurrentDictionary\<TKey?, TValue?>()

    - GetValueOrDefault()

  - String
    - Join()
    - ToNullableDecimal()
    - ToDecimal()
    - Utf8Encode()
    - Utf8Decode()
    - ToNullableGuid()
    - ToNullableDateTime()
    - IsNullOrWhiteSpace()
    - ToNullableTimeSpan()
    - ToNullableLong()
    - ToNullableInt()
    - TrimStart()
    - TrimEnd()
  - DataTable

    - Except()
    - MergeByExcept()
    - MergeByDistinct()
    - MergeColumns()
    - LeftJoin()
    - LeftJoinAppendRight()

  - Type
    - IsDictionary()
    - IsDictionaryValueType()

  - DateTime
    - Add()

  - DurationDto
    - Except()
    - IsOverlap()

- Services
  - BaseValidateService
  - QueueService
    - QueueActionAsync()


- Helpers
  - String
    - ToDateTime

- Type
  - Array\<T>

## KueiPackages.AOP

- IServiceCollection.AddAOPScoped\<T, TImplementation>()
- AOP Attribute

## KueiPackages.Dapper

- IDbConnection

  - QueryMultipleResult().Result\<T>()
  - QueryMultipleResult().Result()

- DynamicParameters

  - Add()

- TypeHandler
  - VarcharToNullDecimalHandler

## KueiPackages.EntityFrameworkCore

- DbContext
  - QueryMultiple().Result\<T>()
  - QueryMultiple().Result()

## KueiPackages.System.Text.Json

- System.Text.Json
  - ToJson()
  - ParseJson\<T>()
- System.Text.Json.JsonConverter
  - NullableTimeSpanJsonConverter()
  - NullableDateTimeJsonConverter()
  - ArrayOfTJsonConverter

## KueiPackages.Microsoft.AspNetCore

Extensions

- String.GetContentType()
- Controller.RenderViewAsync\<T>()

Attribute

- PreventSqlInjectionAttribute

Services

- PropertyInfoService
- SqlInjectionValidateStringService

Validate

- BaseValidate

Exceptions

- ApiResponseException
- NotFoundException
- SqlInjectionValidateFailedException
- ValidateFormFailedException
