using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using YunMall.Entity.dbExt;
using YunMall.Entity.json;
using YunMall.Web.Controllers;
using YunMall.Web.Filters;
using YunMall.Web.IBLL.product;

namespace YunMall.Web.Areas.Category.Controllers
{
    [AuthenticationFilter(Role = "admin")]
    public class ManageController : BaseController
    {
        [Dependency]
        public ICategoryService CategoryService { get; set; }

        // GET: Category/Manage
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查询经营类目类别 韦德 2018年9月26日09:39:56
        /// </summary>
        /// <returns></returns>
        [HttpGet] public JsonResult GetCategorys() {
            var categoryDetails = CategoryService.GetCategoryDetails();
            if (categoryDetails != null && categoryDetails.Count > 0) {
                return Json(new JsonArrayResult<CategoryDetail>(categoryDetails));
            }
            return Json(new HttpResp(1, "加载经营类目列表失败"));
        }
    }
}