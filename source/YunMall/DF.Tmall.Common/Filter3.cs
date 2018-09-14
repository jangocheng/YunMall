using DF.Log;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DF.Tmall.Common
{
    /// <summary>
    /// 注册过滤器代理类
    /// </summary>
    public class Filter3 : DelegatingHandler
    {
        /// <summary>
        /// 重写发送HTTP请求到内部处理程序的方法
        /// </summary>
        /// <param name="request">请求信息</param>
        /// <param name="cancellationToken">取消操作的标记</param>
        /// <returns></returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        { 
            // 记录请求内容
            if (request.Content != null)
            {
                MyLog.Debug(request.RequestUri.AbsolutePath, "Request", string.Format("request=[{0}]", request.Content.ReadAsStringAsync().Result));
            }

            // 发送HTTP请求到内部处理程序，在异步处理完成后记录响应内容
            return base.SendAsync(request, cancellationToken).ContinueWith<HttpResponseMessage>(
            (task) =>
            {
                if (task.Status != TaskStatus.Canceled)
                {
                    HttpContent content = task.Result.Content;

                    if (content != null)
                    {
                        //MyLog.Debug(task.Result.RequestMessage.RequestUri.AbsolutePath, "Response", string.Format("response=[{0}]", content.ReadAsStringAsync().Result));
                    }
                    return task.Result;
                }
                return null;
            }
            );
        }
    }
}