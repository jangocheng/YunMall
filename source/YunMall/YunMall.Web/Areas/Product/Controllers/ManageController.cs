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
using YunMall.Web.Controllers;
using YunMall.Web.Filters;
using YunMall.Web.IBLL.user;

namespace YunMall.Web.Areas.Product.Controllers
{
    [AuthenticationFilter(Role = "admin,supplier,merchant")]
    public class ManageController : BaseController
    {
        [Dependency]
        public IUserService UserService { get; set; }

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
            IList<ProductDetail> list = UserService.GetLimit(page.Value, limit, condition, state.HasValue ? state.Value : 0, beginTime, endTime);
            JsonArrayResult<ProductDetail> jsonArrayResult = new JsonArrayResult<ProductDetail>(0, list);
            if (condition.IsEmpty()
                && beginTime.IsEmpty()
                && endTime.IsEmpty()
                && (state == 0))
            {
                count = UserService.GetCount();
            }
            else
            {
                count = UserService.GetLimitCount(condition, state.HasValue ? state.Value : 0, beginTime, endTime);
            }
            jsonArrayResult.count = count;
            return Json(jsonArrayResult);
        }
    }
}