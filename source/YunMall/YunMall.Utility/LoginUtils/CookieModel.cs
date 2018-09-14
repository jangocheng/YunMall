using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DF.Common;
using YunMall.Entity;

namespace YunMall.Utility.LoginUtils
{
    public class CookieModel
    {
        public CookieModel(string value)
        {
            this.UI_ID = value;
            this.Md5Key = DESEncrypt.MD5(this.UI_ID + Constants.TokenMd5Key);
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UI_ID { get; set; }

        /// <summary>
        /// 用户ID的Md5值
        /// </summary>
        public string Md5Key { get; set; }

        /// <summary>
        /// 检查是否被篡改
        /// </summary>
        public bool Checked
        {
            get
            {
                return string.Equals(DESEncrypt.MD5(this.UI_ID + Constants.TokenMd5Key), this.Md5Key, StringComparison.OrdinalIgnoreCase);
            }
        }

        public override string ToString()
        {
            return DESEncrypt.Encrypt(JsonConvert.SerializeObject(this));
        }
    }
}
