using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YunMall.Entity.dbExt;
using YunMall.Utility.LoginUtils;
using YunMall.Web.Filters;

namespace YunMall.Web.Controllers
{
    /// <summary>
    /// 管理员用户控制器 韦德 2018年9月14日16:26:18
    /// </summary>
    [AuthenticationFilter(Role = "admin,supplier,merchant,user")]
    public class ManagementController : BaseController
    {
        // GET: Admin
        [HttpGet]
        public ActionResult Index() {
            var userDetail = SessionInfo.GetSession().UserDetail;
            // 设置用户权限等级别名
            switch (userDetail.User.Level) {
                case 0:
                    ViewBag.LevelName = "会员";
                    break;
                case 1:
                    ViewBag.LevelName = "管理员";
                    break;
                case 2:
                    ViewBag.LevelName = "供货商";
                    break;
                case 3:
                    // 判断属于一级下的几级代理商
                    if (userDetail.ParentUsers == null) {
                        ViewBag.LevelName = "甲级代理商";
                    }
                    else {
                        if (userDetail.ParentUsers.Count == 3) {
                            ViewBag.LevelName = "甲级代理商下游商户";
                        }
                        else {
                            ViewBag.LevelName = "未知身份";
                        }
                    }
                    break;
                case 4:
                    // 判断属于二级下的几级代理商
                    if (userDetail.ParentUsers == null)
                    {
                        ViewBag.LevelName = "乙级代理商";
                    }
                    else
                    {
                        if (userDetail.ParentUsers.Count == 3)
                        {
                            ViewBag.LevelName = "乙级代理商下游商户";
                        }
                        else
                        {
                            ViewBag.LevelName = "未知身份";
                        }
                    }
                    break;
                case 5:
                    ViewBag.LevelName = "代理商";
                    break;
            }
            return View(userDetail);
        }
    }
}