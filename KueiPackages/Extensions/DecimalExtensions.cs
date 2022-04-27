using System;
using System.Collections.Generic;
using System.Linq;

namespace KueiPackages.Extensions
{
    public static class DecimalExtensions
    {
        /// <summary>
        /// 四捨五入
        /// </summary>
        public static decimal ToFix(this decimal input, int digits)
        {
            return decimal.Round(input, digits, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 四捨五入 + 補齊浮點數 0
        /// </summary>
        public static decimal ToFixAndFillTailZero(this decimal input, int digits)
        {
            var fixValue = input.ToFix(digits);

            var fixValueStr = fixValue.ToString();

            var fillDigits = fixValueStr.Contains(".")
                                 ? digits - fixValueStr.Split('.')[1].Length
                                 : digits;

            // 0 * 1.00m 還是 0，不符合預期
            // 額外處理
            if (fixValue == 0m)
            {
                var zeroResultStrings = new List<string>
                                        {
                                            "0",
                                            "."
                                        };
                var floatZeros = Enumerable.Repeat("0", digits);

                zeroResultStrings.AddRange(floatZeros);

                return zeroResultStrings.Join(string.Empty).ToDecimal();
            }

            if (fillDigits > 0)
            {
                var fillTailZeroMultipler = GenerateFillTailZeroMultipler(fillDigits);

                var fixAndFillTailZero = fixValue * fillTailZeroMultipler;

                return fixAndFillTailZero;
            }

            return fixValue;
        }

        /// <summary>
        /// 給定 2，就會產生 1.00m
        /// </summary>
        private static decimal GenerateFillTailZeroMultipler(int digits)
        {
            if (digits <= 0)
            {
                return 0m;
            }

            var digitList = new List<string> { "1", "." };
            digitList.AddRange(Enumerable.Repeat("0", digits));

            var dInString = digitList.Join(string.Empty);

            return decimal.Parse(dInString);
        }

        /// <summary>
        /// decimal 的 N 次方
        /// </summary>
        public static decimal Pow(this decimal input, int pow)
        {
            return Enumerable.Range(1, pow)
                             .Aggregate(1m,
                                        (seed, element) => seed * input);
        }

        /// <summary>
        /// decimal 開根號
        /// </summary>
        public static decimal Sqrt(this decimal x)
        {
            if (x < 0)
            {
                throw new OverflowException("Cannot calculate square root from a negative number");
            }

            // magic number ...
            decimal epsilon = 2;

            decimal current = (decimal)Math.Sqrt((double)x), previous;
            do
            {
                previous = current;
                if (previous == 0.0M) return 0;
                current = (previous + x / previous) / 2;
            } while (Math.Abs(previous - current) > epsilon);

            return current;
        }

        /// <summary>
        /// 對應至 Excel STDEV Function
        /// </summary>
        public static decimal? StDev(this decimal[] arrData)
        {
            decimal? xSum = 0m;


            int arrNum = arrData.Length;

            for (int i = 0; i < arrNum; i++)
            {
                xSum += arrData[i];
            }

            var      xAvg = xSum / arrNum;
            decimal? sSum = 0m;

            for (int j = 0; j < arrNum; j++)
            {
                sSum += ((arrData[j] - xAvg) * (arrData[j] - xAvg));
            }

            var tmpStDev = (sSum / (arrNum - 1))?.Sqrt();

            return tmpStDev;
        }
    }
}
