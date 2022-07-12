using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;

namespace KueiPackages.Services;

public class HashService : IHashService
{
    public string Hash(string salt, string text)
    {
        if (string.IsNullOrWhiteSpace(salt)
         || string.IsNullOrWhiteSpace(text))
        {
            return string.Empty;
        }

        var encoding  = new UTF8Encoding();
        var saltBytes = encoding.GetBytes(salt);
        var textBytes = encoding.GetBytes(text);
        using (var hmacSHA256 = new HMACSHA256(saltBytes))
        {
            var hashMessage = hmacSHA256.ComputeHash(textBytes);
            return BitConverter.ToString(hashMessage).Replace("-", "").ToLower();
        }
    }

    public SecureString HashSecureString(string salt, SecureString input)
    {
        var bstr   = Marshal.SecureStringToBSTR(input);
        var length = Marshal.ReadInt32(bstr, -4);
        var bytes  = new byte[length];

        var bytesPin = GCHandle.Alloc(bytes, GCHandleType.Pinned);
        try
        {
            Marshal.Copy(bstr, bytes, 0, length);
            Marshal.ZeroFreeBSTR(bstr);

            var hashBytes = HashBytes(salt, bytes);

            var result = new SecureString();
            foreach (var b in hashBytes)
            {
                result.AppendChar((char)b);
            }

            return result;
        }
        finally
        {
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = 0;
            }

            bytesPin.Free();
        }
    }

    private byte[] HashBytes(string salt, byte[] source)
    {
        var encoding  = new UTF8Encoding();
        var saltBytes = encoding.GetBytes(salt);
        using (var hmacSHA256 = new HMACSHA256(saltBytes))
        {
            var hashMessage = hmacSHA256.ComputeHash(source);
            return hashMessage;
        }
    }
}
