using System.Security;

namespace KueiPackages.Services;

public interface IHashService
{
    string Hash(string salt, string text);

    SecureString HashSecureString(string salt, SecureString input);
}
