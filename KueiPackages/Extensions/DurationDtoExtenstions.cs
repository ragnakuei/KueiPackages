using KueiPackages.Models;

namespace KueiPackages.Extensions
{
    public static class DurationDtoExtenstions
    {
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
