using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using YunMall.Utility.LoginUtils;

namespace YunMall.Web.Filters
{
    /// <summary>
    /// 权限认证
    /// </summary>
    public class AuthenticationFilter : ActionFilterAttribute
    {
        /// <summary> 
        /// 角色名称 
        /// </summary> 
        public string Role { get; set; }
        /// <summary> 
        /// 验证权限（action执行前会先执行这里） 
        /// </summary> 
        /// <param name="filterContext"></param> 
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            if (Role == "*") {
                var session = SessionInfo.GetSession();
                if (session == null) {
                    RedirectResult redirectResult = new RedirectResult("/error?c=身份已过期, 请您重新登录");
                    filterContext.Result = redirectResult;
                    redirectResult.ExecuteResult(filterContext);
                }
                return;
            }

            //如果存在身份信息 
            if (!SessionInfo.IsLogin)
            {
                ContentResult Content = new ContentResult();
                Content.Content = "<script type='text/javascript'>alert('请先登录！');location.href='/bootstrap/index';</script>";
                filterContext.Result = Content;
            }
            else {
                var permissions = SessionInfo.GetSession().UserDetail.Permissions;
                string[] permissionArray = permissions.Select(item => item.RoleName).ToArray();
                string permissionJoinStrings = string.Join(",", permissionArray);
                var isContains = Role.Contains(permissionJoinStrings);
                if (!isContains)//验证权限 
                {
                    //验证不通过 
                    ContentResult Content = new ContentResult();
                    Content.Content = "<script type='text/javascript'>alert('您没有权限访问！');location.href='/bootstrap/index';</script>";
                    filterContext.Result = Content;
                }
            }
        }
    }

}