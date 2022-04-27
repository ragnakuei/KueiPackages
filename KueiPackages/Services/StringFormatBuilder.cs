using System.IO;
using KueiPackages.Extensions;

namespace KueiPackages.Services;

public class StringFormatBuilder
{
    private List<string> _parts = new List<string>();

    private string _extensionName = string.Empty;
    private string _delimiter     = string.Empty;

    internal void AppendFileName(string fileName)
    {
        var fileInfo = new FileInfo(fileName);
        _extensionName = fileInfo.Extension;

        _parts.Add(fileInfo.Name.Replace(_extensionName, String.Empty));
    }

    internal void SetDelimiter(string delimiter)
    {
        _delimiter = delimiter;
    }

    internal void AppendText(string appendText)
    {
        _parts.Add(appendText);
    }

    internal void AppendDateTime(DateTime? dt     = null,
                                 string    format = null)
    {
        var referenceDateTime = dt ?? DateTime.Now;

        var appendText = format.IsNullOrWhiteSpace()
                             ? referenceDateTime.ToString()
                             : referenceDateTime.ToString(format);

        _parts.Add(appendText);
    }

    public override string ToString()
    {
        return _parts.Join(_delimiter) + _extensionName;
    }
}

public static class StringFormatBuilderExtenstions
{
    public static StringFormatBuilder SetFileName(this StringFormatBuilder builder, string fileName)
    {
        builder.AppendFileName(fileName);
        return builder;
    }

    public static StringFormatBuilder SetDelimiter(this StringFormatBuilder builder, string delimiter)
    {
        builder.SetDelimiter(delimiter);
        return builder;
    }

    public static StringFormatBuilder AppendText(this StringFormatBuilder builder, string appendText)
    {
        builder.AppendText(appendText);
        return builder;
    }

    public static StringFormatBuilder AppendDateTime(this StringFormatBuilder builder,
                                                     DateTime?                date   = null,
                                                     string                   format = null)
    {
        builder.AppendDateTime(date, format);
        return builder;
    }
}
