using System.Collections.Generic;
using System.Data.Common;
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

        /// <summary>
        /// 插入交易流水
        /// </summary>
        /// <param name="payAccounts"></param>
        /// <param name="dictionary"></param>
        void InsertAccounts(Pays payAccounts, ref IDictionary<string, DbParameter[]> dictionary);
    }
}