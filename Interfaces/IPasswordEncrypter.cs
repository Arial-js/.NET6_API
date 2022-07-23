namespace Dotnet6_API.Interfaces
{
    public interface IPasswordEncrypter
    {
        string ComputeSha512Hash(string rawData, string salt);
        string GetSalt();
    }
}
