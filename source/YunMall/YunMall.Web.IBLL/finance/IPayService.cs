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
    }
}