namespace KueiPackages.Services;

public interface IHashService
{
    string Hash(string salt, string text);
}
