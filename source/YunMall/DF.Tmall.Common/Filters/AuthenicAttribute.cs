using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DF.Common.StringHelper;
using DF.Redis.Cache;
using DF.Tmall.Common.Login;
using DF.Tmall.Model;

namespace DF.Tmall.Common {
    public class AuthenicAttribute : ActionFilterAttribute {
        /// <summary>
        /// 是否启用拦截器
        /// </summary>
        public bool IsCheck { get; set; }

        /// <summary>
        /// Action方法执行之前执行此方法
        /// </summary>
        /// <param name="filterContext"></param>

        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            if (IsCheck) {
                //获取客户端
                var cookie = ExtractClient(filterContext, out var user);
                //验证用户真实性 
                if (VerifyUser(filterContext, user)) return;
                //验证客户是否有权限访问当前URL
                if (VerifyPermission(filterContext, user)) return;
                //延长客户有效在线时间
                RedisSession.Postpone(cookie);
            }
        }

        private bool VerifyPermission(ActionExecutingContext filterContext, Users user) {
            var urlAbsolutePath = filterContext.RequestContext.HttpContext.Request.Url.AbsolutePath;

            if (!user.Permissions.Any(predicate: o => {
                if (o.P_URL.LastIndexOf("*") > 0) {
                    return urlAbsolutePath.Contains(o.P_URL.Substring(0, o.P_URL.LastIndexOf("*", StringComparison.Ordinal)));
                }
                return o.P_URL.Equals(urlAbsolutePath);
            })) {
                filterContext.Result = ExtractContextResult("很抱歉，您没有权限访问本页面！");
                return true;
            }

            return false;
        }

        /// <summary>
        /// 验证用户权限
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private RedirectResult ExtractContextResult(string content) {
            return new RedirectResult(ConstBaseData.Manager + $"/common/error?content={content}");
        }

        /// <summary>
        /// 提取客户端相关信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string ExtractClient(ActionExecutingContext filterContext, out Users user) {
            var cookie = CookieHelper.GetCookieValue(ConstBaseData.LoginCookieKey);
            user = RedisString.GetValue<Users>(cookie);

            var str = ((System.Web.HttpRequestWrapper)filterContext.RequestContext.HttpContext.Request).Url.Query.Dencode();
            if (str.Contains("?before_page="))
            {
                str = str.Replace("?before_page=", "");
                if (str != null)
                {
                    var beforePage = str.AsAnyObject<BeforePage>();
                    if (beforePage != null)
                    {
                        CookieHelper.ClearCookie("brefore_page_url");
                        CookieHelper.ClearCookie("brefore_page_title");
                        CookieHelper.SetCookie("brefore_page_url", beforePage.Url);
                        CookieHelper.SetCookie("brefore_page_title", beforePage.Title);
                    }
                }
            }

            return cookie;
        }

        /// <summary>
        /// 验证用户是否存在
        /// </summary>
        /// <param name="filterContext"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private bool VerifyUser(ActionExecutingContext filterContext, Users user) {
            if (user == null) {
                filterContext.Result = new RedirectResult(ConstBaseData.Manager + "/home/index");
                return true;
            }
            return false;
        }
    }


    public class BeforePage {
        public string Url { get; set; }
        public string Title { get; set; }
    }
}
