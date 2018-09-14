// ========================================描述信息========================================
// json转换器
// ========================================创建信息========================================
// 创建人：  DF
// 创建时间： 2017年9月19日20:57:49
// ========================================变更信息========================================
// 日期          版本        修改人         描述
// 
// ========================================================================================

using  Newtonsoft.Json;

namespace  DF.Tmall.Common {
    public static class JsonConverter {

        /// <summary>
        /// 序列化对象 2017年9月19日20:57:19
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string AsJson<T>(this T param) {
            return JsonConvert.SerializeObject(param);
        }

        /// <summary>
        /// 反序列化对象 2017年9月19日21:19:55
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="param"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static T AsAnyObject<T>(this string param) {
            return JsonConvert.DeserializeObject<T>(param);
        }
    }
}
