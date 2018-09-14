using  System;
using  System.Collections.Generic;
using  System.Linq;
using  System.Text;
using  System.Threading.Tasks;
using  System.Web;
using  System.Web.Mvc;

namespace  DF.Tmall.Common {
    /// <summary>
    /// 验证过滤器  2017年9月4日21:35:53
    /// </summary>
    public class IdentityAttribute : AuthorizeAttribute {
        protected override bool AuthorizeCore(HttpContextBase httpContext) {
            //return base.AuthorizeCore(httpContext);
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext) {
            base.HandleUnauthorizedRequest(filterContext);
            if (filterContext == null) {
                throw new ArgumentException("filterContext");
            }
            else {
                string path = filterContext.HttpContext.Request.Path;
                filterContext.HttpContext.Response.Redirect(HttpUtility.UrlEncode(path),true);
            }
        }
    }
}
