using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text.Json;

namespace KueiPackages.System.Text.Json;

public class StringSecureStringJsonConverter : JsonConverter<SecureString>
{
    public override SecureString Read(ref Utf8JsonReader    reader,
                                      Type                  typeToConvert,
                                      JsonSerializerOptions options)
    {
        var inputString = reader.GetString();
        var result      = new SecureString();

        if (string.IsNullOrWhiteSpace(inputString) == false)
        {
            foreach (var inputChar in inputString)
            {
                result.AppendChar(inputChar);
            }
        }

        return result;
    }

    public override void Write(Utf8JsonWriter        writer,
                               SecureString          secureString,
                               JsonSerializerOptions options)
    {


        IntPtr ptr = Marshal.SecureStringToBSTR(secureString);
        writer.WriteStringValue(Marshal.PtrToStringBSTR(ptr));
        Marshal.ZeroFreeBSTR(ptr);
    }
}
