using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

// https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.rfc2898derivebytes?view=net-5.0

namespace AdvancedCsharp.Test.Cryptography.RFC
{
    [TestClass]
    public class Demo1
    {
        [TestMethod]
        public void symmetric_encryption()
        {
            byte[] salt = GenerateSalt();

            const string messageToEncrypt = "Text att kryptera";
            const string password = "hemligt";

            Console.WriteLine($"Message to encrypt: {messageToEncrypt}\n");

            Console.WriteLine($"Salt:      {BitConverter.ToString(salt)}\n"); // olika varje gång

            var (encryptedData, iv) = Encrypt(
                data: messageToEncrypt,
                password: password,
                salt: salt
            );

            Console.WriteLine($"Encrypted: {BitConverter.ToString(encryptedData)}\n");
            Console.WriteLine($"IV:        {BitConverter.ToString(iv)}\n");

            string decryptedText = Decrypt(encryptedData, password, salt, iv);

            Console.WriteLine($"Decrypted text: {decryptedText}\n");

            Assert.AreEqual(messageToEncrypt, decryptedText);
        }

        private byte[] GenerateSalt()
        {
            // Saltet är ett slumpmässigt nummer
            byte[] salt = new byte[8];
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(salt);
            }
            return salt;
        }

        /*
           Rfc2898DeriveBytes är en implementation av PBKDF2
            Den hashar lösenordet många gånger tillsammans med salt'et. 
            Fördel: 
                - längden på lösenordet spelar ingen roll
                - attackern måste utföra 1000 hashningar för varje lösenordgissning
                - det färdiga nyckeln är jämt distribuerad (och blir svårare att gissa)
        */
        public static (byte[], byte[]) Encrypt(string data, string password, byte[] salt)
        {
            int numberOfIterations = 1000;
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt, numberOfIterations);
            Aes encAlg = Aes.Create();
            encAlg.Key = key.GetBytes(16);
            
            MemoryStream encryptionStream = new MemoryStream();
            CryptoStream encrypt = new CryptoStream(encryptionStream, encAlg.CreateEncryptor(), CryptoStreamMode.Write);
            byte[] utfD1 = new UTF8Encoding(false).GetBytes(data);
            encrypt.Write(utfD1, 0, utfD1.Length);
            encrypt.FlushFinalBlock();
            encrypt.Close();
            byte[] encryptedData = encryptionStream.ToArray();
            key.Reset();

            Assert.AreEqual(1000, key.IterationCount);

            return (encryptedData, encAlg.IV);
        }

        public static string Decrypt(byte[] encryptedData, string password, byte[] salt, byte[] iv)
        {
            // Dekryptera (använder samma lösenord och salt)
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt);
            Aes decAlg = Aes.Create();
            decAlg.Key = key.GetBytes(16);
            decAlg.IV = iv;
            MemoryStream decryptionStreamBacking = new MemoryStream();
            CryptoStream decrypt = new CryptoStream(decryptionStreamBacking, decAlg.CreateDecryptor(), CryptoStreamMode.Write);
            decrypt.Write(encryptedData, 0, encryptedData.Length);
            decrypt.Flush();
            decrypt.Close();
            key.Reset();
            string decrypted = new UTF8Encoding(false).GetString(decryptionStreamBacking.ToArray());

            Assert.AreEqual(1000, key.IterationCount);

            return decrypted;
        }
    }
}
