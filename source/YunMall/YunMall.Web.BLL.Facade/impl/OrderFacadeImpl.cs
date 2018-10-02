using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using YunMall.Entity.db;
using YunMall.Web.IBLL.finance;
using YunMall.Web.IBLL.order;

namespace YunMall.Web.BLL.Facade.impl {
    public class OrderFacadeImpl : IOrderFacade {

        private readonly IOrderService orderService;
        private readonly IPayService payService;

        [InjectionConstructor]
        public OrderFacadeImpl(IOrderService orderService, IPayService payService) {
            this.orderService = orderService;
            this.payService = payService;
        }


        /// <summary>
        /// 批量下单
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public bool PlaceOrder(IList<Orders> orders) { 
            IDictionary<string, DbParameter[]> dictionary = new Dictionary<string, DbParameter[]>();

            orderService.PlaceOrder(orders, ref dictionary);

            var groups = orders.GroupBy(item => item).Select(group => new {
                Pid = @group.Key.Pid,
                Count = @group.Count()
            });

            orders.ForEach(order => {
                var count = groups.Count(item => item.Pid == order.Pid);
                payService.Transfer(order.Uid, order.Sid, order.Amount, "购买商品[" + order.Pid + "],件数[" + count + "]件", ref dictionary);
            });

            return orderService.CommitLock(dictionary);
        }
    }
}