using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Microsoft.Practices.Unity;
using YunMall.Entity.dbExt;
using YunMall.Entity.json;
using YunMall.Utility.LoginUtils;
using YunMall.Web.Controllers;
using YunMall.Web.Filters;
using YunMall.Web.IBLL.product;
using YunMall.Web.IBLL.user;

namespace YunMall.Web.Areas.Product.Controllers
{
    [AuthenticationFilter(Role = "admin,supplier")]
    public class PublishController : BaseController
    {
        [Dependency]
        public IProductService ProductService { get; set; }

        [Dependency]
        public IUserService UserService { get; set; }

        [Dependency]
        public ICategoryService CategoryService { get; set; }

        // GET: Product/Publish
        [HttpGet]
        public ActionResult Index(int? productId)
        {
            // 查询平台利率
            var session = SessionInfo.GetSession();
            var returnRate = session.UserDetail.Permissions.First().ReturnRate;
            ViewBag.ReturnRate = returnRate;

            // 查询经营类目
            var categoryDetails = CategoryService.GetCategoryDetails();
            ViewBag.Categorys = categoryDetails;

            // 商品路由器
            if (productId.HasValue) {
                ProductDetail product = ProductService.GetProduct(productId.Value);
                ViewBag.RealPrice = product.Amount - (product.Amount * product.ReturnRate / 100);
                return View(product);
            }
            return View();
        }

        /// <summary>
        /// 发布商品 韦德 2018年9月20日19:22:42
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="price"></param>
        /// <param name="categoryId"></param>
        /// <param name="type"></param>
        /// <param name="description"></param>
        /// <param name="mainImage"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(int? pid, string productName, double price, int categoryId, int type, string description, string mainImage) {
            if (productName.IsEmpty()) return Json(new HttpResp(1, "请输入商品名称")); 
            if (description.IsEmpty()) return Json(new HttpResp(1, "请输入商品描述")); 
            if (price <= 0) return Json(new HttpResp(1, "请输入正确的定价"));
            if (categoryId <= 0) return Json(new HttpResp(1, "请挂靠正确的经营类目"));
            if (type < 0 || type > 2) return Json(new HttpResp(1, "请选择正确的商品类型"));

            var user = SessionInfo.GetSession();
            if(user == null || user.Uid <= 0) return Json(new HttpResp(1, "请先登录"));

            string cause = string.Empty;

            // 新增或更新
            if (!pid.HasValue) {
                var result = ProductService.CreateProduct(new Entity.db.Product()
                {
                    ProductName = productName,
                    Amount = price,
                    CategoryId = categoryId,
                    Type = type,
                    Description = description,
                    Sid = user.Uid,
                    MainImage = mainImage
                }, ref cause);

                if (result) return Json(new HttpResp("发布成功"));

            }
            else {
                var result = ProductService.EditProduct(new Entity.db.Product()
                {
                    Pid = pid.Value,
                    ProductName = productName,
                    Amount = price,
                    CategoryId = categoryId,
                    Type = type,
                    Description = description,
                    Sid = user.Uid,
                    MainImage = mainImage
                }, ref cause);

                if (result) return Json(new HttpResp("编辑成功"));

            }


            return Json(new HttpResp(1, cause));
        }




    }
}