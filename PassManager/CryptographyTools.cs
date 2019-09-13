using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace PassManager
{
    public static class CryptographyTools
    {

        const int HASH_SIZE = 32;
        const int ITERATIONS = 1000;
        public static byte[] GenerateIV()
        {
            byte[] vec;
            using (Aes aes = Aes.Create())
            {
                vec = aes.IV;
            }
            return vec;
        }

        public static byte[] GenerateSalt(string s)
        {
            byte[] tmp = Encoding.Unicode.GetBytes(s);
            Array.Resize(ref tmp, 8);
            return tmp;
        }

        public static byte[] ConvertToKey(string pass)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(pass, GenerateSalt(pass), ITERATIONS);
            byte[] res = pbkdf2.GetBytes(HASH_SIZE);
            return res;
        }

        public static string GetStringFromByte(byte[] b)
        {
            return Encoding.Unicode.GetString(b);
        }

        public static byte[] GetBytesFromString(string s)
        {
            return Encoding.Unicode.GetBytes(s);
        }

        public static byte[] EncryptString(string s, byte[] k, byte[] IV)
        {
            byte[] enc;

            using (Aes aes = Aes.Create())
            {
                aes.Padding = PaddingMode.ISO10126;
                ICryptoTransform ICT = aes.CreateEncryptor(k, IV);

                using (MemoryStream MS = new MemoryStream())
                {
                    using (CryptoStream CS = new CryptoStream(MS, ICT, CryptoStreamMode.Write))
                    {
                        using (StreamWriter SW = new StreamWriter(CS))
                        {
                            SW.Write(s);
                        }
                    }
                    enc = MS.ToArray();
                }
            }

            return enc;
        }

        public static string DecryptString(byte[] enc, byte[] k, byte[] IV)
        {
            byte[] encstring = enc;
            string res = null;
            using (Aes aes = Aes.Create())
            {
                aes.Padding = PaddingMode.ISO10126;
                ICryptoTransform ICT = aes.CreateDecryptor(k, IV);
                using (MemoryStream MS = new MemoryStream(encstring))
                {
                    using (CryptoStream CS = new CryptoStream(MS, ICT, CryptoStreamMode.Read))
                    {
                        using (StreamReader SR = new StreamReader(CS))
                        {

                            res = SR.ReadToEnd();
                        }
                    }
                }
            }
            return res;
        }

    }
}
