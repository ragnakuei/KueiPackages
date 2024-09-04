namespace KueiPackagesTests;

public static class ActExtension
{
    public static void ActThrowApiResponseExceptionWithMessage(this Action act, string alertMessage)
    {
        var exception = Assert.Throws<ApiResponseException>(act.Invoke);

        Assert.AreEqual(alertMessage, exception.AlertMessage);
    }

    public static void ActThrowApiResponseExceptionWithMessage<T>(this Action act, string alertMessage, List<string> data = null)
    {
        var exception = Assert.Throws<ApiResponseException<T>>(act.Invoke);

        Assert.AreEqual(alertMessage, exception.AlertMessage);

        if (data != null)
        {
            Assert.AreEqual(data, exception.Data);
        }
    }
}