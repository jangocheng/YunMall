using System;

namespace YunMall.Entity.dbExt {
    public class UserFinanceDetail {
        /*users*/
        public int Uid { get; set; }

        public string Username { get; set; }

        public int Level { get; set; }

        public string RoleId { get; set; }

        public string ParentId { get; set; }

        public int Depth { get; set; }

        public string QQ { get; set; }

        public int State { get; set; }

        public string CashAccount { get; set; }

        public string RealName { get; set; }

        /*wallets*/
        public int WalletId { get; set; }

        /// <summary> 
        /// 用户id 
        /// </summary> 
        public int UserId { get; set; }

        /// <summary> 
        /// 余额 
        /// </summary> 
        public double Balance { get; set; }

        /// <summary> 
        /// 最后一次更新时间 
        /// </summary> 
        public DateTime UpdateTime { get; set; }

        /// <summary> 
        /// 乐观锁 
        /// </summary> 
        public int Version { get; set; }
    }
}