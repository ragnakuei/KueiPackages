using System;
using System.Collections.Generic;
using System.Linq;
using KueiPackages.Models;

namespace KueiPackages.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// source 排除 exceptPeriods
        /// <remarks>邊檢查邊回傳</remarks>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="exceptPeriods"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static IEnumerable<DurationDto> Except(this DurationDto         source,
                                                      IEnumerable<DurationDto> exceptPeriods)
        {
            // 先依照起始日期排序
            var exceptPeriodsByOrder = exceptPeriods?.OrderBy(p => p.Begin)
                                                     .ThenBy(p => p.End)
                                                     .ToArray()
                                    ?? new DurationDto[0];

            var begin = source.Begin;

            foreach (var exceptPeriod in exceptPeriodsByOrder)
            {
                if (begin < exceptPeriod.Begin)
                {
                    if (source.End >= exceptPeriod.Begin)
                    {
                        yield return new DurationDto(begin, exceptPeriod.Begin);
                        begin = exceptPeriod.End;
                    }
                }
                else if (begin == exceptPeriod.Begin)
                {
                    begin = exceptPeriod.End;
                }
                else // begin > exceptPeriod.Begin
                {
                    if (begin < exceptPeriod.End)
                    {
                        begin = exceptPeriod.End;
                    }
                }
            }

            if (begin < source.End)
            {
                yield return new DurationDto(begin, source.End);
            }
            else if (begin > source.End)
            {
                // throw new NotSupportedException("結束時間早於於排除起始時間");
            }
        }
    }
}
