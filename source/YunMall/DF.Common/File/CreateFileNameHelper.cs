using  System;
using  System.Collections.Generic;
using  System.Linq;
using  System.Text;

namespace  DF.Common
{
    public static class CreateFileNameHelper
    {
        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="Ext"></param>
        /// <returns></returns>
        public static string GetFileNameByTime(string Ext)
        {
            RandomHelper random = new RandomHelper();
            int num = random.GetRandomInt(0, 9);
            return DateTime.Now.ToString("yyyyMMddHHmmssfff") + Ext+num.ToString();

        }


    }
}
