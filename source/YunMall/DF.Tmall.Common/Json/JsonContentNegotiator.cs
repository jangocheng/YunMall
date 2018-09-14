using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using DF.Common;
using Newtonsoft.Json.Converters;

namespace DF.Tmall.Common.Json
{
    /// <summary>
    /// Json格式化类
    /// </summary>
    public class JsonContentNegotiator : IContentNegotiator
    {
        private JsonMediaTypeFormatter _jsonFormatter;

        /// <summary>
        /// 构造函数
        /// </summary>
        public JsonContentNegotiator()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="formatter">参数</param>
        public JsonContentNegotiator(JsonMediaTypeFormatter jsonFormatter)
        {
            _jsonFormatter = jsonFormatter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="request"></param>
        /// <param name="formatters"></param>
        /// <returns></returns>
        public ContentNegotiationResult Negotiate(Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
        {
            _jsonFormatter = new JsonMediaTypeFormatter();

            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            //这里使用自定义日期格式
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
            _jsonFormatter.SerializerSettings.Converters.Add(timeConverter);

            return new ContentNegotiationResult(_jsonFormatter, new MediaTypeHeaderValue("application/json")); ;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="type"></param>
        ///// <param name="response"></param>
        ///// <param name="formatters"></param>
        ///// <returns></returns>
        //public ContentNegotiationResult Negotiate1(Type type, HttpResponseMessage response, IEnumerable<MediaTypeFormatter> formatters)
        //{
        //    var result = new ContentNegotiationResult(_jsonFormatter, new MediaTypeHeaderValue("application/json"));
        //    return result;
        //}
    }
}