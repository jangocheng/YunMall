using DF.Log;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using DF.Tmall.Common.Exceptions;

namespace DF.Tmall.Common.Filters.ApiFilter
{
    /// <summary>
    /// Api签名、版本、以及数据验证Filter
    /// </summary>
    public class VerifyApiFilter : ActionFilterAttribute
    {
        /// <summary>
        /// 拦截执行方法
        /// </summary>
        /// <param name="actionContext">上下文</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var modelState = actionContext.ModelState;

            //验证参数
            if (modelState != null && !modelState.IsValid && actionContext.Response != null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(modelState.FirstOrDefault().Value.Errors.FirstOrDefault().ErrorMessage.ToString())
                };
            }
        }
    }

    /// <summary>  
    /// 异常处理过滤器  
    /// </summary>  
    public class ExceptionApiFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// 异常处理过滤器执行方法
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(HttpActionExecutedContext context)
        {
            MyLog.Error("WebApi异常处理过滤器", "WebApi", "ERROR", context.Exception);
            var exception = context.Exception as DFException;
            //可以记录一些日志  
            string fLog = JsonConvert.SerializeObject(exception);
            MyLog.Info("WebApi异常处理过滤器返回状态", "WebApi", "Response", fLog);
            //篡改Response  
            context.Response = new HttpResponseMessage(HttpStatusCode.OK);
            context.Response.Content = new StringContent(fLog);
        }
    }
}
