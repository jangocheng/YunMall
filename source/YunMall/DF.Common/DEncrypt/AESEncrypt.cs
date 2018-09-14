/// <summary>
/// 创建人：
/// 说明：AES加密算法即密码学中的高级加密标准（Advanced Encryption Standard，AES），又称Rijndael加密法，是美国联邦政府采用的一种区块加密标准。这个标准用来替代原先的DES，已经被多方分析且广为全世界所使用。经过五年的甄选流程，高级加密标准由美国国家标准与技术研究院（NIST）于2001年11月26日发布于FIPS PUB 197，并在2002年5月26日成为有效的标准。
/// 创建时间：2016-02-18
/// 修改人：：
/// </summary>
using  System;
using  System.Collections.Generic;
using  System.IO;
using  System.Linq;
using  System.Security.Cryptography;
using  System.Text;

namespace  DF.Common
{
    /// <summary>
    /// AES加密算法即密码学中的高级加密标准（Advanced Encryption Standard，AES），又称Rijndael加密法，是美国联邦政府采用的一种区块加密标准。这个标准用来替代原先的DES，已经被多方分析且广为全世界所使用。经过五年的甄选流程，高级加密标准由美国国家标准与技术研究院（NIST）于2001年11月26日发布于FIPS PUB 197，并在2002年5月26日成为有效的标准。
    /// </summary>
    public class AESEncrypt
    {
        #region AES加解密  
        /// <summary>  
        /// AES加密  
        /// </summary>  
        /// <param name="Data">被加密的明文</param>  
        /// <param name="Key">密钥</param>  
        /// <param name="Vector">向量</param>  
        /// <returns>密文</returns>  
        public static Byte[] AES_Encrypt(Byte[] Data, String Key, String Vector)
        {
            Byte[] bKey = new Byte[32];
            Array.Copy(Encoding.UTF8.GetBytes(Key.PadRight(bKey.Length)), bKey, bKey.Length);
            Byte[] bVector = new Byte[16];
            Array.Copy(Encoding.UTF8.GetBytes(Vector.PadRight(bVector.Length)), bVector, bVector.Length);
            Byte[] Cryptograph = null; // 加密后的密文  
            Rijndael Aes = Rijndael.Create();
            try
            {
                // 开辟一块内存流  
                using  (MemoryStream Memory = new MemoryStream())
                {
                    // 把内存流对象包装成加密流对象  
                    using  (CryptoStream Encryptor = new CryptoStream(Memory,Aes.CreateEncryptor(bKey, bVector),CryptoStreamMode.Write))
                    {
                        // 明文数据写入加密流  
                        Encryptor.Write(Data, 0, Data.Length);
                        Encryptor.FlushFinalBlock();

                        Cryptograph = Memory.ToArray();
                    }
                }
            }
            catch
            {
                Cryptograph = null;
            }
            return Cryptograph;
        }

        /// <summary>  
        /// AES解密  
        /// </summary>  
        /// <param name="Data">被解密的密文</param>  
        /// <param name="Key">密钥</param>  
        /// <param name="Vector">向量</param>  
        /// <returns>明文</returns>  
        public static Byte[] AES_Decrypt(Byte[] Data, String Key, String Vector)
        {
            Byte[] bKey = new Byte[32];
            Array.Copy(Encoding.UTF8.GetBytes(Key.PadRight(bKey.Length)), bKey, bKey.Length);
            Byte[] bVector = new Byte[16];
            Array.Copy(Encoding.UTF8.GetBytes(Vector.PadRight(bVector.Length)), bVector, bVector.Length);

            Byte[] original = null; // 解密后的明文  

            Rijndael Aes = Rijndael.Create();
            try
            {
                // 开辟一块内存流，存储密文  
                using  (MemoryStream Memory = new MemoryStream(Data))
                {
                    // 把内存流对象包装成加密流对象  
                    using  (CryptoStream Decryptor = new CryptoStream(Memory,Aes.CreateDecryptor(bKey, bVector),CryptoStreamMode.Read))
                    {
                        // 明文存储区  
                        using  (MemoryStream originalMemory = new MemoryStream())
                        {
                            Byte[] Buffer = new Byte[1024];
                            Int32 readBytes = 0;
                            while ((readBytes = Decryptor.Read(Buffer, 0, Buffer.Length)) > 0)
                            {
                                originalMemory.Write(Buffer, 0, readBytes);
                            }

                            original = originalMemory.ToArray();
                        }
                    }
                }
            }
            catch
            {
                original = null;
            }
            return original;
        }

        #endregion

        #region  AES加解密  
        /// <summary>  
        /// 获取密钥  
        /// </summary>  
        private static string Key
        {
            get { return @"DFYG2016"; }
        }

        /// <summary>  
        /// 获取向量  
        /// </summary>  
        private static string IV
        {
            get { return @"L+\~f4,Ir)b$=pkf"; }
        }

        /// <summary>  
        /// AES加密  
        /// </summary>  
        /// <param name="plainStr">明文字符串</param>  
        /// <returns>密文</returns>  
        public static string AES_Encrypt(string plainStr)
        {
            byte[] bKey = Encoding.UTF8.GetBytes(Key);
            byte[] bIV = Encoding.UTF8.GetBytes(IV);
            byte[] byteArray = Encoding.UTF8.GetBytes(plainStr);

            string encrypt = null;
            Rijndael aes = Rijndael.Create();
            using  (MemoryStream mStream = new MemoryStream())
            {
                using  (CryptoStream cStream = new CryptoStream(mStream, aes.CreateEncryptor(bKey, bIV), CryptoStreamMode.Write))
                {
                    cStream.Write(byteArray, 0, byteArray.Length);
                    cStream.FlushFinalBlock();
                    encrypt = Convert.ToBase64String(mStream.ToArray());
                }
            }
            aes.Clear();
            return encrypt.Replace('+', '-').Replace('/', '_').Replace("=", "."); 
        }

        /// <summary>  
        /// AES加密  
        /// </summary>  
        /// <param name="plainStr">明文字符串</param>  
        /// <param name="returnNull">加密失败时是否返回 null，false 返回 String.Empty</param>  
        /// <returns>密文</returns>  
        public static string AES_Encrypt(string plainStr, bool returnNull)
        {
            string encrypt = AES_Encrypt(plainStr);
            return returnNull ? encrypt : (encrypt == null ? String.Empty : encrypt);
        }

        /// <summary>  
        /// AES解密  
        /// </summary>  
        /// <param name="encryptStr">密文字符串</param>  
        /// <returns>明文</returns>  
        public static string AES_Decrypt(string encryptStr)
        {
            byte[] bKey = Encoding.UTF8.GetBytes(Key);
            byte[] bIV = Encoding.UTF8.GetBytes(IV);
            byte[] byteArray = Convert.FromBase64String(encryptStr.Replace('-', '+').Replace('_', '/').Replace(".", "="));

            string decrypt = null;
            Rijndael aes = Rijndael.Create();
            using  (MemoryStream mStream = new MemoryStream())
            {
                using  (CryptoStream cStream = new CryptoStream(mStream, aes.CreateDecryptor(bKey, bIV), CryptoStreamMode.Write))
                {
                    cStream.Write(byteArray, 0, byteArray.Length);
                    cStream.FlushFinalBlock();
                    decrypt = Encoding.UTF8.GetString(mStream.ToArray());
                }
            }
            aes.Clear();
            return decrypt;
        }

        /// <summary>  
        /// AES解密  
        /// </summary>  
        /// <param name="encryptStr">密文字符串</param>  
        /// <param name="returnNull">解密失败时是否返回 null，false 返回 String.Empty</param>  
        /// <returns>明文</returns>  
        public static string AES_Decrypt(string encryptStr, bool returnNull)
        {
            string decrypt = AES_Decrypt(encryptStr);
            return returnNull ? decrypt : (decrypt == null ? String.Empty : decrypt);
        }


        #endregion

        #region 256位AES加解密

        /// <summary>
        /// 256位AES加密
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt)
        {
            // 256-AES key    
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes("12345678901234567890123456789012");
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// 256位AES解密
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <returns></returns>
        public static string Decrypt(string toDecrypt)
        {
            // 256-AES key    
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes("12345678901234567890123456789012");
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        #endregion
    }
}
