// ========================================描述信息========================================
// 
// ========================================创建信息========================================
// 创建人：   锤子
// 创建时间： 2017-3-13     	
// ========================================变更信息========================================
// 日期          版本        修改人         描述
// 
// ========================================================================================

using  System;
using  System.Security.Cryptography;
using  System.Text;

namespace  DF.Common
{
    /// <summary>
    /// MD5加密操作使用类
    /// </summary>
    public static class MD5Encrypt
    {
        /// <summary>
        ///  MD5加密字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns> MD5签名字符串</returns>
        public static String MD5(this string s)
        {
            StringBuilder sb = new StringBuilder(32);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.Default.GetBytes(s));
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }

        /// <summary>
        ///  MD5加密文件
        /// </summary>
        /// <param name="message">字符串</param>
        /// <returns> MD5签名字符串</returns>
        public static String MD5(this byte[] dataFile)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(dataFile);
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }
    }
}
