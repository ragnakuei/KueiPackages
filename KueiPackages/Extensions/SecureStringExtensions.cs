using System.Runtime.InteropServices;
using System.Security;

namespace KueiPackages.Extensions;

public static class SecureStringExtensions
{
    public static string? ToUnsecureString(this SecureString? secureString)
    {
        if (secureString == null)
        {
            return string.Empty;
        }

        var unmanagedString = IntPtr.Zero;
        try
        {
            unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
            return Marshal.PtrToStringUni(unmanagedString);
        }
        finally
        {
            Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
        }
    }
    
    public static SecureString? ToSecureString(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        var secureString = new SecureString();
        foreach (var c in value)
        {
            secureString.AppendChar(c);
        }

        secureString.MakeReadOnly();
        return secureString;
    }
}
