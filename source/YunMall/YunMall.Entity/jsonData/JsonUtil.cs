using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace YunMall.Utility.jsonData
{
    public class JsonUtil
    {
        /// <summary>
        /// 将任意对象序列化成JSON格式的数据字符串
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string ToJson(object content) {
            if (content == null) throw new ArgumentNullException(nameof(content));
            return JsonConvert.SerializeObject(content);
        }
    }
}
