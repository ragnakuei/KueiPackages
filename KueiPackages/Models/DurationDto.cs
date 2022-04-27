namespace KueiPackages.Models
{
    public struct DurationDto
    {
        public DurationDto(DateTime begin, DateTime end)
        {
            Begin = begin;
            End   = end;
        }

        /// <summary>
        /// 起始時間
        /// </summary>
        public DateTime Begin { get; set; }

        /// <summary>
        /// 結束時間
        /// </summary>
        public DateTime End { get; set; }
    }
}
