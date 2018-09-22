using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using YunMall.Web.Filters;

namespace YunMall.Web.Controllers
{
    [MvcExceptionFilter]
    public class BaseController : Controller {

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding) {
            contentType = "application/json";
            contentEncoding = Encoding.UTF8;
            return base.Json(data, contentType, contentEncoding);
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior) {
            behavior = JsonRequestBehavior.AllowGet;
            return base.Json(data, contentType, contentEncoding, behavior);
        }


        protected JsonResult Json(object data)
        {
            return base.Json(data, behavior: JsonRequestBehavior.AllowGet);
        }
    }
}