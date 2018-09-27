using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Microsoft.Practices.Unity;
using YunMall.Entity.db;
using YunMall.Entity.dbExt;
using YunMall.Entity.json;
using YunMall.Web.Controllers;
using YunMall.Web.Filters;
using YunMall.Web.IBLL.finance;

namespace YunMall.Web.Areas.Finance.Controllers
{
    [AuthenticationFilter(Role = "admin")]
    public class AccountsController : BaseController
    {
        [Dependency(name: "AccountsServiceImpl")]
        public IFinanceService AccountsService { get; set; }

        // GET: Finance/Accounts
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
        /// <param name="type"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public JsonResult GetAccountsLimit(int? page, string limit, string condition, int? type, string beginTime, string endTime)
        {
            int count = 0;
            if (condition != null && condition.Contains("[add]")) condition = condition.Replace("[add]", "+");
            if (condition != null && condition.Contains("[reduce]")) condition = condition.Replace("[reduce]", "-");
            IList<Accounts> list = AccountsService.GetAccountPageLimit(page.Value, limit, condition, type.HasValue ? type.Value : 0, beginTime, endTime);
            JsonArrayResult<Accounts> jsonArrayResult = new JsonArrayResult<Accounts>(0, list);
            if (condition.IsEmpty()
                && beginTime.IsEmpty()
                && endTime.IsEmpty()
                && (type == 0))
            {
                count = AccountsService.GetCount();
            }
            else
            {
                count = AccountsService.GetPageLimitCount(condition, type.HasValue ? type.Value : 0, beginTime, endTime);
            }
            jsonArrayResult.count = count;
            return Json(jsonArrayResult);
        }
    }
}