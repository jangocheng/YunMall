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
    public class ManageController : BaseController
    {
        [Dependency]
        public ICategoryService CategoryService { get; set; }

        // GET: Category/Manage
        [HttpGet] [AuthenticationFilter(Role = "admin")] public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查询经营类目类别 韦德 2018年9月26日09:39:56
        /// </summary>
        /// <returns></returns>
        [HttpGet] [AuthenticationFilter(Role = "admin,supplier")] public JsonResult GetCategorys() {
            var categoryDetails = CategoryService.GetCategoryDetails();
            if (categoryDetails != null && categoryDetails.Count > 0) {
                return Json(new JsonArrayResult<CategoryDetail>(categoryDetails));
            }
            return Json(new HttpResp(1, "加载经营类目列表失败"));
        }


        /// <summary>
        /// 添加经营类目页面 韦德 2018年9月26日14:50:11
        /// </summary>
        /// <returns></returns>
        public ActionResult AddView(int? categoryId) {
            if (categoryId.HasValue) {
                CategoryDetail categoryDetail = CategoryService.GetCategoryDetail(categoryId.Value);
                var categoryDetails = new List<CategoryDetail>();
                categoryDetails.Add(categoryDetail);
                ViewBag.CategoryList = categoryDetails;
                return PartialView(categoryDetail);
            }
            else {
                var categoryDetails = CategoryService.GetCategoryDetails().Where(c => c.ParentId == 0).ToList();
                ViewBag.CategoryList = categoryDetails;
            }
            return PartialView();
        }


        /// <summary>
        /// 添加经营类目 韦德 2018年9月26日16:00:18
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        [HttpPost] [AuthenticationFilter(Role = "admin")] public JsonResult Add(int? parentId, string categoryName) {
            if (!parentId.HasValue) parentId = 0;
            bool result = CategoryService.Add(parentId.Value, categoryName);
            if(result) return Json(new HttpResp("添加成功"));
            return Json(new HttpResp(1,"添加失败"));
        }

        /// <summary>
        /// 编辑经营类目页面 韦德 2018年9月26日14:50:11
        /// </summary>
        /// <returns></returns>
        [AuthenticationFilter(Role = "admin")] public ActionResult EditView(int? categoryId)
        {
            CategoryDetail categoryDetail = CategoryService.GetCategoryDetail(categoryId.Value);
            var categoryDetails = CategoryService.GetCategoryDetails().Where(c => c.ParentId == 0).ToList();
            ViewBag.CategoryList = categoryDetails;
            return PartialView(categoryDetail);
        }


        /// <summary>
        /// 编辑经营类目 韦德 2018年9月26日16:00:18
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="categoryId"></param>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        [HttpPost] [AuthenticationFilter(Role = "admin")] public JsonResult Edit(int? parentId, int categoryId, string categoryName)
        {
            if (!parentId.HasValue) parentId = 0;
            bool result = CategoryService.Edit(parentId.Value, categoryId, categoryName);
            if (result) return Json(new HttpResp("保存成功"));
            return Json(new HttpResp(1, "保存失败"));
        }
    }
}