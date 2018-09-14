using DF.Log;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Mvc;
using DF.Tmall.Model;

namespace DF.Tmall.Common.Filters.WebFilter
{
    /// <summary>
    /// ajax异常处理过滤器--
    /// </summary>
    public class AjaxExceptionFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            MyLog.Error("WebAjax异常处理过滤器", "WebAjax", "ERROR", filterContext.Exception);
            var resp = new WebResp("ERROR", "操作异常");
            //篡改Response  
            filterContext.HttpContext.Response.Clear();
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            filterContext.Result = new JsonResult()
            {
                Data = resp,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                ContentType = "application/json",
            };
        }


    }


}
