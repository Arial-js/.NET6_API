using EsercitazioneAPI.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace EsercitazioneAPI.Utility
{
    public class PasswordEncrypter : IPasswordEncrypter
    {
        private readonly IConfiguration _configuration;

        public PasswordEncrypter(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string ComputeSha512Hash(string rawData, string salt)
        {
            SHA512 sha512 = SHA512.Create();
            byte[] hashValue = sha512.ComputeHash(Encoding.UTF8.GetBytes(rawData + salt));
            return Convert.ToBase64String(hashValue);
        }
        
        public string GetSalt()
        {
            return _configuration["WebApp:Salt"];
        }
    }
}
