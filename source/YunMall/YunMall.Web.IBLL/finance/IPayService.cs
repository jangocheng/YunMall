using System.Collections.Generic;
using System.Data.Common;
using YunMall.Entity.db;

namespace YunMall.Web.IBLL.finance {
    public interface IPayService {
        /// <summary>
        /// 人工充值转账
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        bool Recharge(int uid, double amount);

        /// <summary>
        /// 直接充值转账
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        bool DirectRecharge(int uid, double amount);

        /// <summary>
        /// 验证支付密码
        /// </summary>
        /// <param name="user"></param>
        /// <param name="security"></param>
        /// <param name="cause"></param>
        /// <returns></returns>
        bool CheckSecurityPassword(User user, string security, ref string cause);

        /// <summary>
        /// 借贷转账
        /// </summary>
        /// <param name="fromUid"></param>
        /// <param name="toUid"></param>
        /// <param name="amount"></param>
        /// <param name="remark"></param>
        /// <param name="dictionary"></param>
        void Transfer(int fromUid, int toUid, double amount, string remark,
            ref IDictionary<string, DbParameter[]> dictionary);
    }
}