using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YunMall.Entity
{
    public class Constants {
        /// <summary>
        /// 网站基本信息配置
        /// </summary>
        public static string Title { get; set; } = "请设置您的网站标题";
        public static string KeyWords { get; set; }
        public static string Description { get; set; }
        public static string Brand { get; set; } = "主旋律科技有限公司";

        /// <summary>
        /// 登录key(存放在cookie中)
        /// </summary>
        public static string LoginKey { get; set; } = "Account";

        /// <summary>
        /// 实体验证md5密匙
        /// </summary>
        public static string TokenMd5Key {
            get { return "YunMall";}
        }

        /// <summary>
        /// 默认商品主图地址
        /// </summary>
        public static string DefaultProductImage { get; set; } = "~/content/images/defaultProduct.png";

        /// <summary>
        /// 程序调试数据埋点
        /// </summary>
        public static bool Debug { get; set; }

        /// <summary>
        /// 热点账户ID
        /// </summary>
        public static int HotAccountID { get; set; } = 1;


        public static class DynamicMap {
            /// <summary>
            /// finance.pays.channel.internal 站内交易
            /// </summary>
            public static int DefaultChannelType { get; set; } = 1; // finance.pays.channel.internal 站内交易

          
            public static int AlipayChannelType { get; set; } = 2; // finance.pays.channel.alipay 支付宝

            /// <summary>
            /// finance.pays.product.currency 通用货币
            /// </summary>
            public static int DefaultProductType { get; set; } = 4; // finance.pays.product.currency 通用货币

            public static int GoodsProductType { get; set; } = 5; // finance.pays.product.goods 商品

            /// <summary>
            /// finance.pays.trade.deduction 扣费
            /// </summary>

            public static int DefaultTradeType { get; set; } = 8; // finance.pays.trade.deduction 扣费

            public static int RechargeTradeType { get; set; } = 6; // finance.pays.trade.recharge 充值

            public static int WithdrawTradeType { get; set; } = 7; // finance.pays.trade.withdraw 提现

            public static int ConsumeTradeType { get; set; } = 9; // finance.pays.trade.consume 消费

        }

    }
}
