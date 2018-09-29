using System.Collections;
using YunMall.Entity.db;

namespace YunMall.Web.IDAL.order {
    public interface IOrderRepository : IAbsBaseRepository
    {
        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="orders"></param>
        /// <param name="hash"></param>
        void CreateOrder(Orders orders, ref Hashtable hash);
    }
}