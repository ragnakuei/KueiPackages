namespace KueiPackages.Microsoft.AspNetCore;

public static class FileExtensionContentTypeExtension
{
    public static string GetContentType(this string fileName, string defaultContentType = "application/octet-stream")
    {
        var provider = new FileExtensionContentTypeProvider();

        if (provider.TryGetContentType(fileName, out var contentType))
        {
            return contentType;
        }

        return defaultContentType;
    }
}
