namespace KueiPackages.Extensions;

public static class LongExtensions
{
    public static string ToHumanReadUnit(this long l, int floatDigits = 0)
    {
        var units            = new[] { "", "萬", "億", "兆" };
        var currentUnitIndex = 0;

        var tmp = (decimal)l;
        while(tmp > 10000)
        {
            tmp = tmp /10000m;
            tmp = decimal.Round(tmp, floatDigits, MidpointRounding.AwayFromZero);
            currentUnitIndex ++;
        }

        return tmp.ToString("0.###########") +units[currentUnitIndex];
    }
}
