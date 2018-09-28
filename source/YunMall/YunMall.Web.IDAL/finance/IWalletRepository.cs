using System.Collections.Generic;
using System.Data.Common;
using YunMall.Entity.db;

namespace YunMall.Web.IDAL.finance {
    public interface IWalletRepository : IAbsBaseRepository
    {
        /// <summary>
        /// 刷新钱包账目-出账
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="amount"></param>
        /// <param name="version"></param>
        /// <param name="dictionary"></param>
        void OutAccounts(int uid, double amount, int version, ref IDictionary<string, DbParameter[]> dictionary);

        /// <summary>
        /// 刷新钱包账目-进账
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="amount"></param>
        /// <param name="version"></param>
        /// <param name="dictionary"></param>
        void PutAccounts(int uid, double amount, int version, ref IDictionary<string, DbParameter[]> dictionary);

        /// <summary>
        /// 查询钱包信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        Wallet SelectById(int uid);
    }
}