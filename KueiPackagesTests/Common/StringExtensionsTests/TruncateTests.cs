namespace KueiPackagesTests.Common.StringExtensionsTests;

public class TruncateTests
{
    [Test]
    public void 剛好()
    {
        var actual = "此外，當天氣潮濕、常下雨，更須留意用電安全。".Truncate(22, '。');

        var expected = "此外，當天氣潮濕、常下雨，更須留意用電安全。";

        Assert.AreEqual(expected, actual);
    }
        
    [Test]
    public void 往左找()
    {
        var actual = "此外，當天氣潮濕、常下雨，更須留意用電安全。台灣電力公司則曾於臉書專頁提醒，灰塵、毛髮遇水氣容易「積污導電」，導致短路引發火災。民眾應適時檢查插頭、插座上是否累積過多灰塵，關掉電源後，用棉花棒或乾布擦乾淨。若家電不慎受潮，則建議先送檢維修，切勿直接插電使用。".Truncate(25, '。');

        var expected = "此外，當天氣潮濕、常下雨，更須留意用電安全。";

        Assert.AreEqual(expected, actual);
    }
        
    [Test]
    public void 往右找()
    {
        var actual = "此外，當天氣潮濕、常下雨，更須留意用電安全。台灣電力公司則曾於臉書專頁提醒，灰塵、毛髮遇水氣容易「積污導電」，導致短路引發火災。民眾應適時檢查插頭、插座上是否累積過多灰塵，關掉電源後，用棉花棒或乾布擦乾淨。若家電不慎受潮，則建議先送檢維修，切勿直接插電使用。".Truncate(55, '。');

        var expected = "此外，當天氣潮濕、常下雨，更須留意用電安全。台灣電力公司則曾於臉書專頁提醒，灰塵、毛髮遇水氣容易「積污導電」，導致短路引發火災。";

        Assert.AreEqual(expected, actual);
    }  
    
    [Test]
    public void 原始句子太短()
    {
        var actual = "此外，當天氣潮濕、常下雨，更須留意用電安全。".Truncate(50, '。');

        var expected = "此外，當天氣潮濕、常下雨，更須留意用電安全。";

        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public void 找不到對應的字元_顯示全部()
    {
        var actual = "此外，當天氣潮濕、常下雨，更須留意用電安全".Truncate(10, '。');

        var expected = "此外，當天氣潮濕、常下雨，更須留意用電安全";

        Assert.AreEqual(expected, actual);
    }
}