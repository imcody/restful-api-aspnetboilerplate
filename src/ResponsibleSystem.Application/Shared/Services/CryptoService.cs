using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using ResponsibleSystem.Configuration;

namespace ResponsibleSystem.Shared.Services
{
    // https://gist.github.com/RichardHan/0848a25d9466a21f1f38
    public class CryptoService : ICryptoService
    {
        private readonly string _key, _iv;

        public CryptoService(IOptions<CryptographySettings> cryptoSettings)
        {
            string PadRight(string input)
            {
                input = input.Replace("=", "");
                var padLength = (4 - (input.Length % 4)) % 4;
                return input.PadRight(input.Length + padLength, '=');
            }

            _key = PadRight(cryptoSettings.Value.EncryptionKey);
            _iv = PadRight(cryptoSettings.Value.IV);
        }

        public string Decrypt(string encryptedText)
        {
            string decrypted = null;
            byte[] cipher = Convert.FromBase64String(encryptedText);

            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.Key = Convert.FromBase64String(_key);
                // iv length for aes cbc is 16 bytes
                aes.IV = Convert.FromBase64String(_iv).Take(16).ToArray();
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform dec = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(cipher))
                {
                    using (CryptoStream cs = new CryptoStream(ms, dec, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            decrypted = sr.ReadToEnd();
                        }
                    }
                }
            }

            return decrypted;
        }

        public string Encrypt(string plainText)
        {
            byte[] encrypted;

            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.Key = Convert.FromBase64String(_key);
                aes.IV = Convert.FromBase64String(_iv).Take(16).ToArray();
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform enc = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, enc, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }

                        encrypted = ms.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encrypted);
        }
    }
}