using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Encryption
{
    public class AES
    {
        public static String Encrypt256(string mark, byte[] sharedKey)
        {
            // AesCryptoServiceProvider
            byte[] Key = sharedKey;
            Array.Resize(ref Key, 32);
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.Key = Key;
            aes.IV = Encoding.UTF8.GetBytes(@"!QAZ2WSX#EDC4RFV");
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            // Convert string to byte array
            byte[] src = System.Text.ASCIIEncoding.ASCII.GetBytes(mark);

            // encryption
            using (ICryptoTransform encrypt = aes.CreateEncryptor())
            {
                byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);
                string result = Convert.ToBase64String(dest);
                return result;
            }
        }

        public static string Decrypt256(string markEncrypted, byte[] sharedKey)
        {
            byte[] Key = sharedKey;
            Array.Resize(ref Key, 32);
            // AesCryptoServiceProvider
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.IV = Encoding.UTF8.GetBytes(@"!QAZ2WSX#EDC4RFV");
            aes.Key = Key;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            // Convert Base64 strings to byte array
            byte[] src = Convert.FromBase64String(markEncrypted);

            // decryption
            using (ICryptoTransform decrypt = aes.CreateDecryptor())
            {
                byte[] dest = decrypt.TransformFinalBlock(src, 0, src.Length);
                return Encoding.UTF8.GetString(dest);
            }
        }
    }
}
