using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using YunMall.Entity.db;

namespace YunMall.Web.IDAL.order {
    public interface IOrderRepository : IAbsBaseRepository
    {
        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="orders"></param>
        /// <param name="dictionary"></param>
        void CreateOrder(Orders orders, ref IDictionary<string, DbParameter[]> dictionary);
    }
}