using System.Collections.Generic;
using YunMall.Entity.dbExt;

namespace YunMall.Web.IDAL.product {
    /// <summary>
    /// 经营类目仓储接口
    /// </summary>
    public interface ICategoryRepository : IAbsBaseRepository
    {
        /// <summary>
        /// 查询经营类目详细信息列表
        /// </summary>
        /// <returns></returns>
        IList<CategoryDetail> QueryDetails();
    }
}