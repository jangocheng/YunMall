using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YunMall.Web.Controllers
{
    /// <summary>
    /// 商家商铺控制器
    /// </summary>
    public class ShopController : BaseController
    {
        // GET: Shop
        public ActionResult Index()
        {
            return View();
        }
    }
}