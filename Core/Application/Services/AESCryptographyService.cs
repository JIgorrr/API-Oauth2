using Application.Interfaces.Services;
using System.Security.Cryptography;

namespace Application.Services;

public class AESCryptographyService : IAESCryptographyService
{
    public string Decrypt(string password, string key, string vector)
    {
        try
        {
            byte[] passwordByte = Convert.FromBase64String(password);
            byte[] keyByte = Convert.FromBase64String(key);
            byte[] vectorByte = Convert.FromBase64String(vector);

            string textResult;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = keyByte;
                aesAlg.IV = vectorByte;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using MemoryStream msDecrypt = new(passwordByte);
                using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
                using StreamReader srDecrypt = new(csDecrypt);
                textResult = srDecrypt.ReadToEnd();
            }

            return textResult;
        }
        catch(Exception)
        {
            throw new Exception("Não foi possível descriptografar o dado");
        }
    }
}
