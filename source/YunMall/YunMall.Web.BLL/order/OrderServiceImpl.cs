using System;
using System.Collections;
using DF.Common;
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
        /// <returns></returns>
        public bool PlaceOrder(Orders orders) {
            // 1.校验商品是否存在
            var product = productRepository.QueryDetail(orders.Pid);
            if (product == null) throw new MsgException("商品不存在");
            if (product.Status != 1) throw new MsgException("商品已下架");
            if (product.State != 0) throw new MsgException("账户已禁用");

            // 2.生成订单
            Hashtable hash = new Hashtable();
            orders.OrderId = IdWorkTool.Instance().GetId();
            orders.AddTime = DateTime.Now;
            orders.EditTime = DateTime.Now;
            orderRepository.CreateOrder(orders, ref hash);

            // 3.事务下单
            orderRepository.CommitTransaction(hash);
            return true; // TODO::虚拟类商品无需做库存设置，如果雇主需要请在第二期计划里做
        }
    }
}