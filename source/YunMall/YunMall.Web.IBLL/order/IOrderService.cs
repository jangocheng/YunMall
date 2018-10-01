using System.Collections.Generic;
using YunMall.Entity.db;

namespace YunMall.Web.IBLL.order {
    public interface IOrderService {
        /// <summary>
        /// 创建订单 
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        bool PlaceOrder(Orders orders);

        /// <summary>
        /// 创建订单 
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        bool PlaceOrder(IList<Orders> orders);
    }
}