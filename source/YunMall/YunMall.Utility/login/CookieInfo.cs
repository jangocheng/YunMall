using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DF.Common;
using YunMall.Entity;

namespace YunMall.Utility.LoginUtils
{
    /// <summary>
    /// Cookie存取帮助类
    /// </summary>
    public class CookieInfo
    {
        #region 登陆站点保存登录Cookie--yfx
        /// <summary>
        /// 保存登录Cookie yfx
        /// </summary>
        /// <param name="value"></param>
        public static void SetLoginCookie(string value)
        {
            CookieModel model = new CookieModel(value); //每次登陆唯一标识解决不同客户端登陆登出互相影响问题);
            CookieHelper.SetCookie(Constants.LoginKey, model.ToString());

        }
        #endregion

        #region 获取cookie--yfx
        /// <summary>
        /// 获取登录Cookie   yfx
        /// </summary>
        public static string GetLoginCookie()
        {
            var cookievalue = CookieHelper.GetCookieValue(Constants.LoginKey);
            return AnalysisCookie(cookievalue);
        }
        #endregion

        #region 解析cookie信息--yfx
        /// <summary>
        /// 解析cookie信息
        /// </summary>
        /// <param name="cookievalue">cookie</param>
        /// <returns>返回解析后的cookie</returns>
        private static string AnalysisCookie(string cookievalue)
        {
            if (string.IsNullOrEmpty(cookievalue)) //判断Cookie是否为空
            {
                return null;
            }
            var cookie = JsonConvert.DeserializeObject<CookieModel>(DESEncrypt.Decrypt(cookievalue));//获取cookie值
            return (cookie != null && cookie.Checked) ? cookie.UI_ID : string.Empty;
        }
        #endregion

        #region 登陆站点保存登录Cookie(后台)--qzz
        /// <summary>
        /// 保存登录Cookie yfx
        /// </summary>
        /// <param name="value"></param>
        public static void SetBackLoginCookie(string value)
        {
            CookieModel model = new CookieModel(value); //每次登陆唯一标识解决不同客户端登陆登出互相影响问题); BackLoginCookieKey
            CookieHelper.SetCookie(Constants.LoginKey, model.ToString());

        }
        #endregion

        #region 获取cookie(后台)--qzz
        /// <summary>
        /// 获取登录Cookie
        /// </summary>
        public static string GetBackLoginCookie()
        {
            var cookievalue = CookieHelper.GetCookieValue(Constants.LoginKey); // BackLoginCookieKey
            return AnalysisCookie(cookievalue);
        }
        #endregion

    }
}
