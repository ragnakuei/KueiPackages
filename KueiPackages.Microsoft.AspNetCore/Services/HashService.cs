namespace KueiPackages.Microsoft.AspNetCore.Services;

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
}
