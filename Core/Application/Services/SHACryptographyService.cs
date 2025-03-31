using Application.Interfaces.Services;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services;

public class SHACryptographyService : ISHACryptographyService
{
    public string GenerateHash(string text, int saltNumber)
    {
        try
        {
            byte[] salt = new byte[saltNumber];
            byte[] passwordBytes = Encoding.UTF8.GetBytes(text);
            byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

            Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
            Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

            byte[] hash = SHA256.HashData(saltedPassword);

            return Convert.ToBase64String(hash);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public bool VerifyHash(string requestPassword, string storedPassword, int salt)
    {
        try
        {
            string hashResult = GenerateHash(requestPassword, salt);

            return hashResult.SequenceEqual(storedPassword);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
