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
using YunMall.Utility.LoginUtils;
using YunMall.Web.Controllers;
using YunMall.Web.Filters;
using YunMall.Web.IBLL.finance;
using YunMall.Web.IBLL.user;

namespace YunMall.Web.Areas.Finance.Controllers
{
    [AuthenticationFilter(Role = "admin")]
    public class PaysController : BaseController
    {
        [Dependency(name: "PaysServiceImpl")]
        public IFinanceService PayService { get; set; }

        [Dependency]
        public IUserService UserService { get; set; }

        [Dependency(name: "AccountsServiceImpl")]
        public IFinanceService AccountService { get; set; }

        // GET: Finance/Pays
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
        /// <param name="tradeType"></param>
        /// <param name="type"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public JsonResult GetPayLimit(int? page, string limit, string condition, int? tradeType, int? type, string beginTime, string endTime)
        {
            int count = 0;
            if (condition != null && condition.Contains("[add]")) condition = condition.Replace("[add]", "+");
            if (condition != null && condition.Contains("[reduce]")) condition = condition.Replace("[reduce]", "-");
            IList<PaysDetail> list = PayService.GetPayPageLimit(page.Value, limit, condition, tradeType.HasValue ? tradeType.Value : 0 , type.HasValue ? type.Value : 0, beginTime, endTime);
            JsonArrayResult<PaysDetail> jsonArrayResult = new JsonArrayResult<PaysDetail>(0, list);
            if (condition.IsEmpty()
                && beginTime.IsEmpty()
                && endTime.IsEmpty()
                && (type == 0))
            {
                count = PayService.GetCount();
            }
            else
            {
                count = PayService.GetPaysPageLimitCount(condition, tradeType.HasValue ? tradeType.Value : 0, type.HasValue ? type.Value : 0, beginTime, endTime);
            }
            jsonArrayResult.count = count;
            return Json(jsonArrayResult);
        }


        /// <summary>
        /// 查询公户信息 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetSystemAccount() {
            var session = SessionInfo.GetSession();
            var userDetail = session.UserDetail;
            var result = AccountService.GetUserAmount(userDetail);
            if(result != null  && result.Count > 0) return Json(new HttpResp(result));
            return Json(new HttpResp(1, "查询失败"));
        }
    }
}