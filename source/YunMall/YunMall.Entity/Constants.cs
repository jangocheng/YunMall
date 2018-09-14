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
        public static string LoginKey { get; set; }

        /// <summary>
        /// 实体验证md5密匙
        /// </summary>
        public static string TokenMd5Key {
            get { return "YunMall";}
        }

    }
}
