using System;

namespace YunMall.Entity.db {
    /// <summary>
    /// 钱包表
    /// </summary>
    public class Wallet {
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