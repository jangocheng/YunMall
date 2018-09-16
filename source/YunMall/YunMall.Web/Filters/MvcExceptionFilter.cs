using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using YunMall.Entity.json;

namespace YunMall.Web.Filters
{
    public class MvcExceptionFilter : System.Web.Mvc.FilterAttribute, System.Web.Mvc.IExceptionFilter
    {
        #region 请求的action发生异常时会执行此方法
        /// <summary>
        /// 请求的action发生异常时会执行此方法
        /// </summary>
        /// <param name="filterContext"></param>
        void System.Web.Mvc.IExceptionFilter.OnException(System.Web.Mvc.ExceptionContext filterContext)
        {
            /*//在这里你可以记录发生异常时你要干什么，比例写日志
            string message = filterContext.Exception.Message;
            filterContext.Controller.ViewData["ErrorMessage"] = message;

            //返回的结果给客户端
            filterContext.Result = new System.Web.Mvc.ContentResult()
            {
                Content = "出错了:)",
                ContentEncoding = System.Text.Encoding.UTF8
            };


            filterContext.ExceptionHandled = true;  //告诉系统，这个异常已经处理了，不用再处理

            //filterContext.ExceptionHandled = false;  //告诉系统，这个异常没有处理，需要再处理 */

            var error = filterContext.Exception;
            var message = error.Message;//错误信息 
            var url = HttpContext.Current.Request.RawUrl;//错误发生地址 

            Console.Error.WriteLine(String.Format(url + "::" + message));

            filterContext.ExceptionHandled = true;
            filterContext.Result = new JsonResult() {
                ContentEncoding = Encoding.UTF8,
                ContentType = "application/json",
                Data = new HttpResp(1, "o(╥﹏╥)o 系统发生故障啦~~~")
            };//跳转至错误提示页面 
        }
        #endregion
    }
}