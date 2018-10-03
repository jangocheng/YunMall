using System;

namespace YunMall.Entity.dbExt {
    public class AccountsDetail {
        public long AccountsId { get; set; }

        /// <summary> 
        /// 交易记录id 
        /// </summary> 
        public long PayId { get; set; }

        /// <summary> 
        /// 交易主体账户id 
        /// </summary> 
        public int TradeAccountId { get; set; }

        /// <summary> 
        /// 交易主体账户名称 
        /// </summary> 
        public string TradeAccountName { get; set; }

        /// <summary> 
        /// 账目类型(1=进账,2=出账) 
        /// </summary> 
        public int AccountsType { get; set; }

        /// <summary> 
        /// 货币种类(0=现金,1=虚拟货币) 
        /// </summary> 
        public int Currency { get; set; }

        /// <summary> 
        /// 账目总额 
        /// </summary> 
        public double Amount { get; set; }

        /// <summary> 
        /// 操作前余额 
        /// </summary> 
        public double BeforeBalance { get; set; }

        /// <summary> 
        /// 操作后余额 
        /// </summary> 
        public double AfterBalance { get; set; }

        /// <summary> 
        /// 发生时间 
        /// </summary> 
        public DateTime AddTime { get; set; }

        /// <summary> 
        /// 摘要 
        /// </summary> 
        public string Remark { get; set; }

    }
}