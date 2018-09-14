using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using YunMall.Entity;
using YunMall.Utility.LoginUtils;

namespace YunMall.Web.Filters
{
    public class MvcSecurityFilter : FilterAttribute, IActionFilter
    {

        public bool RoleFilter { get; set; }

        public void OnActionExecuted(ActionExecutedContext filterContext) {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext) {
            // 是否开启角色过滤器
            if (!RoleFilter) return;
            // 验证是否登录
            if (!IsLogin(filterContext)) {
                filterContext.Result = new RedirectResult("/Bootstrap/Index");
            }
            else {
                // 已登录

            }

        }


        /// <summary>
        /// 验证是否登录
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        private bool IsLogin(ActionExecutingContext filterContext) {
            var cookie = CookieHelper.GetCookieValue(Constants.LoginKey);
            //如果cookie不为null且session未登录说明session过期 或者是cookie为空时 直接跳转至登录页
            if ((!string.IsNullOrEmpty(cookie) && !SessionInfo.IsBackLogin) || string.IsNullOrEmpty(cookie)) {
                return false;
            }
            SessionInfo.BackPostponeKey();
            return true;
        }
    }
}