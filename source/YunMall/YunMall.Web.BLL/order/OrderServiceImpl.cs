using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using DF.Common;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using YunMall.Entity.db;
using YunMall.Web.Exceptions;
using YunMall.Web.IBLL.order;
using YunMall.Web.IDAL.order;
using YunMall.Web.IDAL.product;

namespace YunMall.Web.BLL.order {
    public class OrderServiceImpl : IOrderService {

        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository productRepository;

        [InjectionConstructor]
        public OrderServiceImpl(IOrderRepository orderRepository, IProductRepository productRepository) {
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="orders"></param>
        /// <para>
        ///  
        /// </para>
        /// <returns></returns>
        public bool PlaceOrder(Orders orders) {
            // 1.校验商品是否存在
            var product = productRepository.QueryDetail(orders.Pid);
            if (product == null) throw new MsgException("商品不存在");
            if (product.Status != 1) throw new MsgException("商品已下架");
            if (product.State != 0) throw new MsgException("账户已禁用");

            // 2.生成订单
            IDictionary<string, DbParameter[]> dictionary = new Dictionary<string, DbParameter[]>();
            orders.OrderId = IdWorkTool.Instance().GetId();
            orders.AddTime = DateTime.Now;
            orders.EditTime = DateTime.Now;
            orderRepository.CreateOrder(orders, ref dictionary);

            // 3.事务下单
            orderRepository.CommitTransactionLock(dictionary);
            return true; // TODO::虚拟类商品无需做库存设置，如果雇主需要请在第二期计划里做
        }


        /// <summary>
        /// 批量创建订单
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public void PlaceOrder(IList<Orders> orders, ref IDictionary<string, DbParameter[]> dictionary) {
            IDictionary<string, DbParameter[]> nDictionary = new Dictionary<string, DbParameter[]>();

            // 1.校验商品是否存在
            var product = productRepository.QueryDetails(string.Join(",", orders.Select(item => item.Pid).ToList()));
            if (product == null) throw new MsgException("商品不存在");
            if (product.Count < orders.Count) throw new MsgException("清单中包含已下架的商品, 请您重新下单！");

            // 2.生成订单
            var groupId = IdWorkTool.Instance().GetId();

            orders.ForEach(order => {
                order.OrderId = IdWorkTool.Instance().GetId();
                order.GroupId = groupId;
                order.AddTime = DateTime.Now;
                order.EditTime = DateTime.Now;
                order.Amount = product.First(item => item.Pid == order.Pid).Amount;
                order.Sid = product.First(item => item.Pid == order.Pid).Sid;
                orderRepository.CreateOrder(order, ref nDictionary);
            });
            dictionary = nDictionary;
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public bool CommitLock(IDictionary<string, DbParameter[]> dictionary) {
            return orderRepository.CommitTransactionLock(dictionary);
        }
    }
}