using System.Collections.Generic;
using System.Data.Common;
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
        /// 批量创建订单 
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        void PlaceOrder(IList<Orders> orders, ref IDictionary<string, DbParameter[]> dictionary);


        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        bool CommitLock(IDictionary<string, DbParameter[]> dictionary);
    }
}