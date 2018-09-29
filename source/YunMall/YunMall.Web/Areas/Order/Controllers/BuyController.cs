using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YunMall.Web.Controllers;

namespace YunMall.Web.Areas.Order.Controllers
{
    public class BuyController : BaseController
    {
        // GET: Order/Buy
        public ActionResult Index()
        {
            return View();
        }
    }
}