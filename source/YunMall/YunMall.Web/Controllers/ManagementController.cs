using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YunMall.Web.Filters;

namespace YunMall.Web.Controllers
{
    /// <summary>
    /// 管理员用户控制器 韦德 2018年9月14日16:26:18
    /// </summary>
    [AuthenticationFilter(Code = "admin")]
    public class ManagementController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}