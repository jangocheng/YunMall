using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using YunMall.Entity.db;
using YunMall.Entity.dbExt;
using YunMall.Web.IBLL.product;
using YunMall.Web.IBLL.user;

namespace YunMall.Web.Controllers
{
    /// <summary>
    /// 商家商铺控制器
    /// </summary>
    public class ShopController : BaseController
    { 
        [Dependency]
        public IProductService ProductService { get; set; }

        [Dependency]
        public IUserService UserService { get; set; }

        // GET: Shop
        [HttpGet]
        [Route("shop/{id}")]
        public ActionResult Index(int? id) {

            if(!id.HasValue) return Redirect("/error");

            User userDetail = UserService.GetUserById(id.Value);

            if (userDetail == null) return Redirect("/error");

            IList<ShopProductDetail> shopProductDetails = ProductService.GetShopProducts(id.Value);

            ViewBag.User = userDetail;

            ViewBag.Products = shopProductDetails;

            ViewBag.Categorys = shopProductDetails.Select(item => item.CategoryName);

            return View();
        }
    }
}