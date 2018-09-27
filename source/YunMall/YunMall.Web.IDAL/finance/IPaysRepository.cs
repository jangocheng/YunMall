using System.Collections.Generic;
using YunMall.Entity.db;
using YunMall.Entity.dbExt;

namespace YunMall.Web.IDAL.finance {
    public interface IPaysRepository : IAbsBaseRepository{
        /// <summary>
        /// 查询分页总数
        /// </summary>
        /// <param name="type"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        int SelectLimitCount(int type, string beginTime, string endTime, string where);

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="type"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        IList<PaysDetail> SelectLimit(int page, string limit, int tradeType, int type, string beginTime, string endTime, string where);
    }
}