using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using YunMall.Entity.dbExt;
using YunMall.Entity.json;
using YunMall.Utility.LoginUtils;
using YunMall.Web.Controllers;
using YunMall.Web.Filters;
using YunMall.Web.IBLL.product;
using YunMall.Web.IBLL.user;

namespace YunMall.Web.Areas.Product.Controllers
{
    [AuthenticationFilter(Role = "admin,supplier,merchant")]
    public class ManageController : BaseController
    {
        [Dependency]
        public IUserService UserService { get; set; }

        [Dependency]
        public IProductService ProductService { get; set; }

        // GET: Product/Manage
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="condition"></param>
        /// <param name="state"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public JsonResult GetProducts(int? page, string limit, string condition, int? state, string beginTime, string endTime) {
            int count = 0;
            IList<ProductDetail> list = ProductService.GetLimit(page.Value, limit, condition, state.HasValue ? state.Value : 0, beginTime, endTime);
            JsonArrayResult<ProductDetail> jsonArrayResult = new JsonArrayResult<ProductDetail>(0, list);
            if (condition.IsEmpty()
                && beginTime.IsEmpty()
                && endTime.IsEmpty()
                && (state == 0))
            {
                count = ProductService.GetCount();
            }
            else
            {
                count = ProductService.GetLimitCount(condition, state.HasValue ? state.Value : 0, beginTime, endTime);
            }
            jsonArrayResult.count = count;
            return Json(jsonArrayResult);
        }


        /// <summary>
        /// 商品上架
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public JsonResult Putaway(string productId) {
            var session = SessionInfo.GetSession();
            var sessionUid = session.Uid;
            var cause = "上架失败";
            bool result = ProductService.Putaway(sessionUid, productId, ref cause);
            if (result) {
                return Json(new HttpResp("上架成功"));
            }
            return Json(new HttpResp(1, cause));
        }

        /// <summary>
        /// 商品下架
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public JsonResult UnShelve(string productId)
        {
            var session = SessionInfo.GetSession();
            var sessionUid = session.Uid;
            var cause = "下架失败";
            bool result = ProductService.UnShelve(sessionUid, productId, ref cause);
            if (result)
            {
                return Json(new HttpResp("下架成功"));
            }
            return Json(new HttpResp(1, cause));
        }


    }
}