using System.Collections.Generic;
using YunMall.Entity.db;

namespace YunMall.Web.BLL.Facade {
    public interface IOrderFacade {
        /// <summary>
        /// 批量创建订单
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        bool PlaceOrder(IList<Orders> orders);
    }
}