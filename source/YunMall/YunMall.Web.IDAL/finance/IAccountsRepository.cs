using System.Collections.Generic;
using System.Data.Common;
using YunMall.Entity.db;

namespace YunMall.Web.IDAL.finance {
    public interface IAccountsRepository : IAbsBaseRepository
    {
        /// <summary>
        /// 查询分页总数
        /// </summary>
        /// <param name="state"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        int SelectLimitCount(int state, string beginTime, string endTime, string where);

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="state"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        IList<Accounts> SelectLimit(int page, string limit, int state, string beginTime, string endTime, string where);

        /// <summary>
        /// 查询收入支出金额
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        IDictionary<string, string> SelectAmount(int uid);
        
        /// <summary>
        /// 批量插入流水
        /// </summary>
        /// <param name="currentAccounts"></param>
        /// <param name="dictionary"></param>
        void BatchInsertAccounts(IList<Accounts> currentAccounts, ref IDictionary<string, DbParameter[]> dictionary);


        /// <summary>
        /// 批量插入流水-实时计算
        /// </summary>
        /// <param name="currentAccounts"></param>
        /// <param name="dictionary"></param>
        void BatchInsertSumAccounts(IList<Accounts> currentAccounts,
            ref IDictionary<string, DbParameter[]> dictionary);
    }
}