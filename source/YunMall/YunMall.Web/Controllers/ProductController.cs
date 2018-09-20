using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YunMall.Web.Controllers
{
    public class ProductController : Controller {
        // GET: Product
        [HttpGet]
        public ActionResult Index() {
            return View();
        }
    }
}