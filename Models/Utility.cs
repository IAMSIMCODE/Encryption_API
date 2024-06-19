using System.Security.Cryptography;

namespace Encryption.Api.Models
{
    public static class Utility
    {
        //GENERATE A BASE 64 AES KEY 

        public static string GenerateKey()
        {
            using Aes aes = Aes.Create();
            aes.KeySize = 256;
            aes.GenerateKey();

            string keyBase64 = Convert.ToBase64String(aes.Key);
            return keyBase64;
        }

        public static string Encrypt(string plainText, string key, out string IVKey)
        {
            using Aes aes = Aes.Create();

            aes.Padding = PaddingMode.Zeros;
            aes.Key = Convert.FromBase64String(key);

            aes.GenerateIV();
            IVKey = Convert.ToBase64String(aes.IV);

            ICryptoTransform encryptor = aes.CreateEncryptor();
            byte[] encryptedData;

            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                using var sw = new StreamWriter(cs);
                sw.Write(plainText);
            }

            encryptedData = ms.ToArray();
            return Convert.ToBase64String(encryptedData);
        }

        public static string Decrypt(string cipherText, string key, string IVKey)
        {
            using Aes aes = Aes.Create();
            aes.Padding = PaddingMode.Zeros;

            aes.Key = Convert.FromBase64String(key);
            aes.IV = Convert.FromBase64String(IVKey);

            IVKey = Convert.ToBase64String(aes.IV);

            ICryptoTransform decryptor = aes.CreateDecryptor();

            string plainText = "";
            byte[] cipher = Convert.FromBase64String(cipherText);

            using var ms = new MemoryStream(cipher);
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            {
                using var sr = new StreamReader(cs);
                plainText = sr.ReadToEnd();
            }
            
            return plainText;
        }
    }
}
