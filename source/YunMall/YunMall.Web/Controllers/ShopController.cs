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
        [Route("shop/u/{id}")]
        public ActionResult Index(int? id) {

            if(!id.HasValue) return Redirect("/error?c=请求参数不完整");

            User userDetail = UserService.GetUserById(id.Value);

            if (userDetail == null) return Redirect("/error?c=用户查询超时");

            IList<ShopProductDetail> shopProductDetails = ProductService.GetShopProducts(id.Value);

            ViewBag.User = userDetail;

            ViewBag.Products = shopProductDetails;

            ViewBag.Categorys = shopProductDetails.Select(item => item);

            return View();
        }


        /// <summary>
        /// 预览商品信息
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public ActionResult Preview(int pid) {
            IList<ProductDetail> shopProductDetails = ProductService.GetShopProducts(Convert.ToString(pid));
            if (shopProductDetails == null || shopProductDetails.Count == 0) return Redirect("/error?c=商品不存在");
            var shopProductDetail = shopProductDetails.First();
            return PartialView(shopProductDetail);
        }
    }
}