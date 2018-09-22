using System.Collections.Generic;
using YunMall.Entity.db;
using YunMall.Entity.dbExt;

namespace YunMall.Web.IDAL.user {
    /// <summary>
    /// 用户数据仓储接口
    /// </summary>
    public interface IUserRepository : IAbsBaseRepository
    {
        int SelectLimitCount(int state, string beginTime, string endTime, string where);
        List<ProductDetail> SelectLimit(int page, string limit, int state, string beginTime, string endTime, string where);
    }
}