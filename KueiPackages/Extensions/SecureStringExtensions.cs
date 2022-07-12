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

    public static bool ContentEqual(this SecureString? source, SecureString? compare)
    {
        var sourceIntPtr = Marshal.SecureStringToBSTR(source);
        var sourceLength = Marshal.ReadInt32(sourceIntPtr, -4);

        var compareIntPtr = Marshal.SecureStringToBSTR(compare);
        var compareLength = Marshal.ReadInt32(compareIntPtr, -4);

        if (sourceLength != compareLength)
        {
            return false;
        }

        var sourceBytesPin = GCHandle.Alloc(sourceIntPtr, GCHandleType.Pinned);
        var sourceBytes       = new byte[sourceLength];
        
        var compareBytesPin = GCHandle.Alloc(compareIntPtr, GCHandleType.Pinned);
        var compareBytes    = new byte[compareLength];

        try
        {
            Marshal.Copy(sourceIntPtr, sourceBytes, 0, sourceLength);
            Marshal.ZeroFreeBSTR(sourceIntPtr);
            
            Marshal.Copy(compareIntPtr, compareBytes, 0, compareLength);
            Marshal.ZeroFreeBSTR(compareIntPtr);
            
            return sourceBytes.SequenceEqual(compareBytes);
        }
        finally
        {
            for (var i = 0; i < sourceBytes.Length; i++)
            {
                sourceBytes[i] = 0;
            }
            
            for (var i = 0; i < compareBytes.Length; i++)
            {
                compareBytes[i] = 0;
            }

            sourceBytesPin.Free();
            compareBytesPin.Free();
        }
    }
}
