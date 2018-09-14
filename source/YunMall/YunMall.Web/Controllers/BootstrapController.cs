using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YunMall.Web.Controllers
{
    /// <summary>
    /// 启动页控制器 韦德 2018年9月12日16:57:23
    /// </summary>
    public class BootstrapController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}