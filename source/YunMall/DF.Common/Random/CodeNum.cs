using  System;
using  System.Collections.Generic;
using  System.Linq;
using  System.Text;

namespace  DF.Common
{
    /// <summary>
    /// 用于获取流水号等一系列的ID操作类
    /// </summary>
    public static class CodeNum
    {
        private static object objLock;
        private static System.Collections.Generic.SortedList<string, int> iOrderNumList;
        static CodeNum()
        {
            CodeNum.objLock = new object();
            CodeNum.iOrderNumList = new System.Collections.Generic.SortedList<string, int>();
        }
        private static string getOrderNumSeq(string key, int Width)
        {
            string result;
            lock (CodeNum.objLock)
            {
                if (!CodeNum.iOrderNumList.ContainsKey(key))
                {
                    CodeNum.iOrderNumList.Add(key, 0);
                }
                if (CodeNum.iOrderNumList[key] > 2147483637)
                {
                    CodeNum.iOrderNumList[key] = 0;
                }
                //System.Collections.Generic.SortedList<string, int> sortedList;
                //(sortedList = CodeNum.iOrderNumList)[key] = sortedList[key] + 1;
                CodeNum.iOrderNumList[key] = CodeNum.iOrderNumList[key] + 1;
                result = CodeNum.iOrderNumList[key].ToString().PadLeft(Width, '0');
            }
            return result;
        }

        /// <summary>
        /// 获取绑定的卡号的ID
        /// </summary>
        /// <returns></returns>
        public static string GetBindCardID()
        {
            return string.Format("{0}{1}", System.DateTime.Now.ToString("yyyyMMddHHmmss"), CodeNum.getOrderNumSeq("GetBindCardID", 4));
        }
        /// <summary>
        /// 获取消息流水号
        /// </summary>
        /// <returns></returns>
        public static string GetNumber()
        {
            return string.Format("{0}{1}", System.DateTime.Now.ToString("yyyyMMddHHmmss"), CodeNum.getOrderNumSeq("GetNumber", 6));
        }
        /// <summary>
        /// 验证码序列号
        /// </summary>
        /// <returns></returns>
        public static string GetVeriNum()
        {
            return string.Format("{0}{1}", System.DateTime.Now.ToString("HHmmss"), CodeNum.getOrderNumSeq("GetVeriNum", 2));
        }

        /// <summary>
        /// 订单序列号
        /// </summary>
        /// <returns></returns>
        public static string GetOrderNum()
        {
            return string.Format("{0}{1}", System.DateTime.Now.ToString("HHmmss"), CodeNum.getOrderNumSeq("GetVeriNum", 5));
        }
        /// <summary>
        /// 订单序列号
        /// </summary>
        /// <returns></returns>
        public static string GetOrderNum1()
        {
            return string.Format("{0}{1}", System.DateTime.Now.ToString("HHmmss"), CodeNum.getOrderNumSeq("GetVeriNum", 3));
        }
        /// <summary>
        /// 获取验证码（数字加字母）
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static string GetNum(int length)
        {
            var text2 = "";
            var random = new Random((int)DateTime.Now.Ticks);
            const string textArray = "0123456789ABCDEFGHGKLMNPQRSTUVWXYZ";
            for (var i = 0; i < length; i++)
            {
                text2 = text2 + textArray.Substring(random.Next() % textArray.Length, 1);
            }
            return text2.ToString();
        }
    }
}
