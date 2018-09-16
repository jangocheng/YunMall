using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DF.Redis.Cache;
using Microsoft.Practices.Unity;
using YunMall.Entity.db;
using YunMall.Entity.enums;
using YunMall.Entity.json;
using YunMall.Utility.LoginUtils;
using YunMall.Web.IBLL.user;

namespace YunMall.Web.Controllers
{
    /// <summary>
    /// 启动页控制器 韦德 2018年9月12日16:57:23
    /// </summary>
    public class BootstrapController : BaseController {

        [Dependency]
        public IUserService UserService { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 账户登录 韦德 2018年9月16日11:15:05
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public JsonResult Login(string username, string password) {
            var user = new User();
            var loginResult = UserService.Login(username, password, ref user);
            // 登录成功后将用户信息刷入缓存服务里
            if (loginResult == LoginResult.L00000) {
                SessionInfo.SetSession(new SessionModel() {
                    Uid = user.Uid,
                    Level = user.Level,
                    RoleId = user.RoleId,
                    ParentId = user.ParentId,
                    Depth = user.Depth,
                    RegIp = user.RegIp,
                    LastIp = user.LastIp,
                    LastTime = user.LastTime,
                    QQ = user.QQ,
                    State = user.State,
                    RealName = user.RealName,
                    AddTime = user.AddTime
                });
            }
            return Json(new HttpResp(loginResult.ToString()));
        }
    }
}