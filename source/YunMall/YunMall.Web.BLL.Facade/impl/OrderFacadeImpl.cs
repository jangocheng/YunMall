using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using YunMall.Entity;
using YunMall.Entity.db;
using YunMall.Entity.dbExt;
using YunMall.Web.Exceptions;
using YunMall.Web.IBLL.finance;
using YunMall.Web.IBLL.order;
using YunMall.Web.IBLL.user;

namespace YunMall.Web.BLL.Facade.impl {
    public class OrderFacadeImpl : IOrderFacade {

        private readonly IOrderService orderService;
        private readonly IPayService payService;
        private readonly IUserService userService;
        private readonly IDictionarysService dictionaryService;

        [InjectionConstructor]
        public OrderFacadeImpl(IOrderService orderService, IPayService payService, IUserService userService, IDictionarysService dictionaryService) {
            this.orderService = orderService;
            this.payService = payService;
            this.userService = userService;
            this.dictionaryService = dictionaryService;
        }


        /// <summary>
        /// 批量下单
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public bool PlaceOrder(IList<Orders> orders) { 
            IDictionary<string, DbParameter[]> dictionary = new Dictionary<string, DbParameter[]>();

            // 创建订单 
            orderService.PlaceOrder(orders, ref dictionary);


            // 创建流水
            var productGroups = orders.GroupBy(item => item).Select(group => new {
                Pid = @group.Key.Pid,
                Count = @group.Count()
            }); 

            var count = productGroups.Sum(item => item.Count);
            var amount = orders.Sum(item => item.Amount);
            var productList = string.Join(",",orders.Select(item => item.Pid).ToList());
            payService.Transfer(orders.First().Uid, orders.First().Sid, amount, "购买商品ID[" + productList + "],总件数[" + count + "]件", ref dictionary);


            // 如果会员隶属于某一个用户，构成了上下级关系，应抽取返利送给上级，返利属于供货商业务成本
            var customer = userService.GetUserById(orders.First().Uid);
            if (customer != null && customer.ParentId > 0) {

                var returnRate = dictionaryService.GetValueById(Constants.DynamicMap.ReturnRate);

                if(returnRate == null) throw new MsgException("交易时发生异常，请稍后重试！");

                payService.Transfer(orders.First().Sid, customer.ParentId, amount: amount * (Convert.ToDouble(returnRate.Value) / 100), remark: "购物返利红包", dictionary: ref dictionary);
            }

            // 事务提交
            return orderService.CommitTransation(dictionary);
        }
    }
}