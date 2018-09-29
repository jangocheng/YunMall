using System;

namespace YunMall.Entity.db {
    public class Orders {
        public long OrderId { get; set; }

        /// <summary> 
        /// 商品id 
        /// </summary> 
        public int Pid { get; set; }

        /// <summary> 
        /// 商品名称 
        /// </summary> 
        public string Pname { get; set; }

        /// <summary> 
        /// 商家id 
        /// </summary> 
        public int Sid { get; set; }

        /// <summary> 
        /// 商家名称 
        /// </summary> 
        public string Sname { get; set; }

        /// <summary> 
        /// 用户id 
        /// </summary> 
        public int Uid { get; set; }

        /// <summary> 
        /// 用户名称 
        /// </summary> 
        public string Uname { get; set; }

        /// <summary> 
        /// 交易方式(0=站内,1=支付宝) 
        /// </summary> 
        public int TradeType { get; set; }

        /// <summary> 
        /// 交易额 
        /// </summary> 
        public double Amount { get; set; }

        /// <summary> 
        /// 流水id 
        /// </summary> 
        public long PayId { get; set; }

        /// <summary> 
        /// 创建时间 
        /// </summary> 
        public DateTime AddTime { get; set; }

        /// <summary> 
        /// 更新时间 
        /// </summary> 
        public DateTime EditTime { get; set; }

        /// <summary> 
        /// 摘要 
        /// </summary> 
        public string Remark { get; set; }

    }
}