using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Security.Cryptography;
using System.Text;

// todo: Två personer skickar meddelande. Dubbla nyckelpar. De krypterar med varandras publika nycklar.
namespace AdvancedCsharp.Test.Cryptography.RSA
{
    [TestClass]
    public class Demo1
    {
        [TestMethod]
        public void export_import()
        {
            const string messageToEncrypt = "Hello world";

            Console.WriteLine($"Message to encrypt: {messageToEncrypt}\n");

            var (encrypted, keysInfo) = EncryptAndExport(messageToEncrypt);
            
            Console.WriteLine($"Encrypted text: {BitConverter.ToString(encrypted)}\n");
            Console.WriteLine($"Key info: {BitConverter.ToString(keysInfo)}\n");

            var decrypted = DecryptUsingImport(encrypted, keysInfo);

            Console.WriteLine($"Decrypted text: {decrypted}\n");

            Assert.AreEqual(messageToEncrypt, decrypted);
        }

        private string DecryptUsingImport(byte[] encrypted, byte[] keysInfo)
        {
            var rsaProvider = new RSACryptoServiceProvider();
            rsaProvider.ImportCspBlob(keysInfo);
            var decryptedBytes = rsaProvider.Decrypt(encrypted, true);
            string decryptedText = Encoding.Default.GetString(decryptedBytes);
            return decryptedText;
        }

        // RSA = Rivest–Shamir–Adleman

        private (byte[], byte[]) EncryptAndExport(string originalText)
        {
            byte[] rawBytes = Encoding.Default.GetBytes(originalText);
            using (var rsaProvider = new RSACryptoServiceProvider())    // Här skapas automatiskt ett public/private-nyckelpar
            {
                var keysInfo = rsaProvider.ExportCspBlob(true);
                var encrypted = rsaProvider.Encrypt(rawBytes, true);
                return (encrypted, keysInfo);
            }
        }

        // Använd samma instans av RSACryptoServiceProvider

        [TestMethod]
        public void using_the_same_RSA_provider()
        {
            const string originalText = "Hello world";

            var rsaProvider = new RSACryptoServiceProvider();
            byte[] encrypted = Encrypt(originalText, rsaProvider);
            Console.WriteLine($"Encrypted message: {BitConverter.ToString(encrypted)}");

            string decrypted = Decrypt(encrypted, rsaProvider);

            Assert.AreEqual(originalText, decrypted);
        }

        private string Decrypt(byte[] encrypted, RSACryptoServiceProvider rsaProvider)
        {
            byte[] decrypted = rsaProvider.Decrypt(encrypted, true);
            return Encoding.Default.GetString(decrypted);
        }

        private byte[] Encrypt(string message, RSACryptoServiceProvider rsaProvider)
        {
            byte[] rawBytes = Encoding.Default.GetBytes(message);

            return rsaProvider.Encrypt(rawBytes, true);
        }

        // Akademiskt exempel

        [TestMethod]
        public void encrypt_and_decrypt_the_same_text()
        {
            EncryptAndDecryptSameText();
        }

        private void EncryptAndDecryptSameText()
        {
            byte[] rawBytes = Encoding.Default.GetBytes("Hello world");
            using (var rsaProvider = new RSACryptoServiceProvider())
            {
                Console.WriteLine($"Nyckelparet: {BitConverter.ToString(rsaProvider.ExportCspBlob(true))}");

                var useOaepPadding = true;

                var encryptedBytes =
                   rsaProvider.Encrypt(rawBytes, useOaepPadding);

                var decryptedBytes =
                   rsaProvider.Decrypt(encryptedBytes, useOaepPadding);

                string decryptedText = Encoding.Default.GetString(decryptedBytes);
                Assert.AreEqual("Hello world", decryptedText);
            }
            // decryptedText == hello world..

        }
    }
}
