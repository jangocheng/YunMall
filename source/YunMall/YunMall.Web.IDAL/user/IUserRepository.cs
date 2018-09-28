using System.Collections.Generic;
using YunMall.Entity.db;
using YunMall.Entity.dbExt;

namespace YunMall.Web.IDAL.user {
    /// <summary>
    /// 用户数据仓储接口
    /// </summary>
    public interface IUserRepository : IAbsBaseRepository
    {
        /// <summary>
        /// 查询用户财务详情
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        UserFinanceDetail SelectFinanceDetail(int uid);

        /// <summary>
        /// 查询多个用户的财务详情
        /// </summary>
        /// <param name="uids"></param>
        /// <returns></returns>
        IList<UserFinanceDetail> SelectFinanceDetails(int[] uids);
    }
}