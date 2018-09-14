// ========================================描述信息========================================
// 
// ========================================创建信息========================================
// 创建人：   
// 创建时间： 2017-3-13     	
// ========================================变更信息========================================
// 日期          版本        修改人         描述
// 
// ========================================================================================
using  System;
using  System.IO;
using  System.Net.Http;
using  System.Net.Http.Formatting;
using  System.Net.Http.Headers;
using  System.Text;
using  System.Threading.Tasks;
using  Newtonsoft.Json;

namespace  DF.Common
{
    /// <summary>
    /// 详情请访问 http://www.cnblogs.com/dudu/p/csharp-httpclient-attention.html 
    ///            https://news.cnblogs.com/n/553217/
    /// <para>HttpClient 应该只初始化一次，并在应用程序的整个生存期内重用。在负载很高的情况下，为每个请求初始化一个 HttpClient 类会耗尽可用的套接字数量。这会导致 SocketException 错误。</para>
    /// <para>客户端虽然保持着TCP连接，但TCP连接是两口子的事，服务器端呢？你不告诉服务器，服务器怎么知道你要一直保持TCP连接呢？对于客户端，保持TCP连接的开销不大；但是对于服务器，则完全不一样的，如果默认都保持TCP连接，那可是要保持成千上万客户端的连接啊。所以，一般的Web服务器都会根据客户端的诉求来决定是否保持TCP连接，这就是keep-alive存在的理由。</para>
    /// <para>所以，我们还要给HttpClient增加一个Connection:keep-alive的请求头，</para>
    /// </summary>
    public static class HttpHelper
    {

        private readonly static HttpClient httpClient;

        static HttpHelper()
        {
            //Http预热和保持长链接
            httpClient = new HttpClient() { BaseAddress = new Uri("http://www.baidu.com") };

            //开始请预热
            httpClient.SendAsync(new HttpRequestMessage()
            {
                Method = new HttpMethod("HEAD"),
                RequestUri = new Uri("http://www.baidu.com")
            });
            //是否保持连接
            httpClient.DefaultRequestHeaders.Connection.Add("keep-alive");

            //请求方式
            httpClient.DefaultRequestHeaders.Add("Method", "post");

            //Json 还是XML
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="reqUrl"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public async static Task<TResult> PostJsonAsync<TData, TResult>(string reqUrl, TData t)
        {
            HttpResponseMessage httpResp = await httpClient.PostAsJsonAsync(reqUrl, t);

            if (httpResp.IsSuccessStatusCode)
            {
                return await httpResp.Content.ReadAsAsync<TResult>();
            }
            else
            {
                return default(TResult);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="reqUrl"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public async static Task<String> PostJsonAsync<TData>(string reqUrl, TData t)
        {
            HttpResponseMessage httpResp = await httpClient.PostAsJsonAsync(reqUrl, t);

            if (httpResp.IsSuccessStatusCode)
            {
                return await httpResp.Content.ReadAsStringAsync();
            }
            else
            {
                return default(String);
            }
        }

        public async static Task<TResult> PostXmlAsync<TData, TResult>(string reqUrl, TData t)
        {
            HttpResponseMessage httpResp = await httpClient.PostAsXmlAsync(reqUrl, t);

            if (httpResp.IsSuccessStatusCode)
            {
                return await httpResp.Content.ReadAsAsync<TResult>();
            }
            else
            {
                return default(TResult);
            }
        }

        public async static Task<TResult> GetjsonAsync<TResult>(string reqUrl)
        {
            HttpResponseMessage response = await httpClient.GetAsync(reqUrl);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<TResult>();
            }
            else return default(TResult);
        }

        public async static Task<TResult> PutJsonAsync<TData, TResult>(string reqUrl, TData t)
        {
            HttpResponseMessage response = await httpClient.PutAsJsonAsync(reqUrl, t);

            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<TResult>();
            }
            else return default(TResult);
        }

        public async static Task<TResult> PutXmlAsync<TData, TResult>(string reqUrl, TData t)
        {
            HttpResponseMessage response = await httpClient.PutAsXmlAsync(reqUrl, t);

            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<TResult>();
            }
            else return default(TResult);
        }

        public static async Task<TResult> DeleteJsonAsync<TResult>(string reqUrl)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync(reqUrl);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<TResult>();
            }

            else return default(TResult);
        }


        public class JsonMediaTypeFormatter : BaseJsonMediaTypeFormatter
        {
            public override JsonReader CreateJsonReader(Type type, Stream readStream, Encoding effectiveEncoding)
            {
                throw new NotImplementedException();
            }

            public override JsonWriter CreateJsonWriter(Type type, Stream writeStream, Encoding effectiveEncoding)
            {
                throw new NotImplementedException();
            }

            public override int MaxDepth
            {
                get
                {
                    return base.MaxDepth;
                }

                set
                {
                    base.MaxDepth = 4;
                }
            }

        }
    }
}
