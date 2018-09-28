using System;
using YunMall.Web.Models;

namespace YunMall.Entity.db {
    /// <summary>
    /// 财务交易记录表
    /// </summary>
    public class Pays {
        [GenerateId]
        public long PayId { get; set; }

        /// <summary> 
        /// 交易主体账户id 
        /// </summary> 
        public int FromUid { get; set; }

        /// <summary> 
        /// 交易主体账户名称 
        /// </summary> 
        [IgnoreField]
        public string FromName { get; set; }

        /// <summary> 
        /// 交易对方账户id 
        /// </summary> 
        public int ToUid { get; set; }

        /// <summary> 
        /// 交易对方账户名称 
        /// </summary> 
        [IgnoreField]
        public string ToName { get; set; }

        /// <summary> 
        /// 渠道类型 
        /// </summary> 
        public int ChannelType { get; set; }

        /// <summary> 
        /// 渠道名称 
        /// </summary> 
        [IgnoreField]
        public string ChannelName { get; set; }

        /// <summary> 
        /// 商品类别 
        /// </summary> 
        public int ProductType { get; set; }

        /// <summary> 
        /// 商品名称 
        /// </summary> 
        [IgnoreField]
        public string ProductName { get; set; }

        /// <summary> 
        /// 交易类型 
        /// </summary> 
        public int TradeType { get; set; }

        /// <summary> 
        /// 交易名称 
        /// </summary> 
        [IgnoreField]
        public string TradeName { get; set; }

        /// <summary> 
        /// 发生时间 
        /// </summary> 
        public DateTime AddTime { get; set; }

        /// <summary> 
        /// 交易总额 
        /// </summary> 
        public double Amount { get; set; }

        /// <summary> 
        /// 系统交易流水单号 
        /// </summary> 
        public long SystemRecordId { get; set; }

        /// <summary> 
        /// 摘要 
        /// </summary> 
        public string Remark { get; set; }

        /// <summary> 
        /// 渠道交易流水单号 
        /// </summary> 
        public string ChannelRecordId { get; set; }

        /// <summary> 
        /// 状态(0=正常,1=退款) 
        /// </summary> 
        public int Status { get; set; }

        /// <summary> 
        /// 渠道交易到账响应时间 
        /// </summary> 
        public DateTime ToAccountTime { get; set; }


    }
}