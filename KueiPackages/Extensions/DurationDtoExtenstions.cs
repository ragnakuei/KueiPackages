using KueiPackages.Models;

namespace KueiPackages.Extensions
{
    public static class DurationDtoExtenstions
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

        public static bool IsOverlap(this DurationDto a, DurationDto b)
        {
            // 列舉所有可能版本
            // return (b.Begin <= a.Begin && a.Begin < b.End)
            //     || (b.Begin < a.End    && a.End   <= b.End)
            //     || (a.Begin <= b.Begin && b.Begin < a.End)
            //     || (a.Begin < b.End    && b.End   <= a.End);

            // 列舉 - 不會重疊的情境
            if (b.End <= a.Begin || a.End <= b.Begin)
            {
                return false;
            }

            return true;

            // 最簡化版本
            // return (a.Begin < b.End && b.Begin < a.End);
        }
    }
}
