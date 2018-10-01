using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using DF.Common;
using Microsoft.Practices.Unity;
using WebGrease.Css.Extensions;
using YunMall.Entity.db;
using YunMall.Entity.json;
using YunMall.Entity.ModelView;
using YunMall.Utility.LoginUtils;
using YunMall.Web.Controllers;
using YunMall.Web.IBLL.finance;
using YunMall.Web.IBLL.order;
using YunMall.Web.IBLL.product;
using YunMall.Web.IBLL.user;

namespace YunMall.Web.Areas.Order.Controllers
{
    /// <summary>
    /// 购买支付控制器
    /// </summary>
    public class BuyController : BaseController
    {
        [Dependency]
        public IProductService ProductService { get; set; }

        [Dependency]
        public IUserService UserService { get; set; }

        [Dependency]
        public IPayService PayService { get; set; }

        [Dependency]
        public IOrderService OrderService { get; set; }

        // GET: Order/Buy
        public ActionResult Index(string products) {
            var session = SessionInfo.GetSession();

            var user = session.UserDetail;

            var productDetails = ProductService.GetShopProducts(products);

            // 筛选重复数据

            var list = products.Split(',').GroupBy(item => item)
                .Select(group => new {
                    Pid = @Convert.ToInt32(group.Key),
                    Count = @group.Count()
                }).ToList();


            // 更新现有数据表
            productDetails.ForEach(item => {
                var product = list.Where(data => data.Pid == item.Pid).First();
                if (product != null) {
                    item.Count = product.Count;
                }
            });

            // 存储到页面中
            ViewBag.Products = productDetails;
            ViewBag.ProductMaps = list;

            return View(user);
        }


        public ActionResult Payment(int type, string security,  products) {
            if (security == null || security.IsEmpty() || security.Length < 5) return Json(new HttpResp(1, "请输入支付密码"));

            var session = SessionInfo.GetSession();
            var user = session.UserDetail;

            // 1.安全验证
            var cause = string.Empty;
            bool checkResult = PayService.CheckSecurityPassword(user.User, security, ref cause);
            if (!checkResult) return Json(new HttpResp(1, cause));

            // 2.判断支付类型(0=钱包支付, ^=站外支付平台)
            if (type == 1) return Redirect("/payment/toPay?type=1");

            // 3.生成记录
            var productList = products.Split(',').ToList();
            IList<Orders> orderses = new List<Orders>();
            productList.ForEach(p => {
                orderses.Add(new Orders()
                {
                    Pid = Convert.ToInt32(p),
                    Uid = session.Uid
                });
            });
            var placeOrder = OrderService.PlaceOrder(orderses);

            if(placeOrder) return Json(new HttpResp("支付成功"));

            return Json(new HttpResp(1, "支付失败"));
        }

    }
}