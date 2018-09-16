using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DF.Common;
using DF.Redis.Cache;

namespace YunMall.Utility.LoginUtils
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionInfo
    {
        #region Session存--yfx
        /// <summary>
        /// 存储用户信息
        /// </summary>
        public static void SetSession(SessionModel model)
        {
            var _sessionid = model.Uid.ToString() + Guid.NewGuid();  //每次登陆唯一标识解决不同客户端登陆登出互相影响问题
            CookieInfo.SetLoginCookie(_sessionid);
            RedisSession.Set<SessionModel>(_sessionid, model);

        }
        #endregion

        #region Session取--yfx
        /// <summary>
        /// 获取用户信息
        /// </summary>
        public static SessionModel GetSession()
        {
            return GetSessionKey();
        }
        #endregion

        #region 快捷判断登陆状态--yfx
        /// <summary>
        /// 是否登录
        /// </summary>  
        public static bool IsLogin
        {
            get { return GetSession() != null ? true : false; }
        }
        #endregion       

        #region 根据sessionID清除Session  public--yfx
        /// <summary>
        /// 根据SessionID清除Session信息
        /// </summary>
        /// <param name="key">sessionID</param>
        public static void ClareSessionKey()
        {
            var _sessionid = GetSessionID();
            if (string.IsNullOrEmpty(_sessionid))
            {
                return;
            }
            RedisSession.ClearKey(_sessionid);
        }
        #endregion

        #region 根据sessionID给Session续期 public--yfx
        /// <summary>
        /// 根据Key给Session续期
        /// </summary>
        /// <param name="key">sessionID</param>
        /// <returns></returns>
        public static bool PostponeKey()
        {
            var _sessionid = GetSessionID();
            if (!string.IsNullOrEmpty(_sessionid))
            {
                return RedisSession.Postpone(GetSessionID());
            }
            return false;
        }
        #endregion

        #region Redis 取 private--yfx
        /// <summary>
        /// 获取Redis中的Session信息     
        /// </summary>
        /// <returns></returns>
        private static SessionModel GetSessionKey()
        {

            string useriD = GetSessionID();
            if (string.IsNullOrEmpty(useriD))
            {
                return null;
            }
            var model = RedisSession.Get<SessionModel>(useriD);
            return model;
        }
        #endregion

        #region 获取sessionID private--yfx 
        /// <summary>
        /// 获得sessionID(用户ID)
        /// useriD cookiesID
        /// </summary>
        /// <returns></returns>
        private static string GetSessionID()
        {
            return CookieInfo.GetLoginCookie();
        }
        #endregion


        #region 后台登录 qzz
        #region Session存--yfx
        /// <summary>
        /// 存储用户信息
        /// </summary>
        public static void SetBackSession(SessionModel model)
        {
            var _sessionid = model.Uid.ToString() + Guid.NewGuid();  //每次登陆唯一标识解决不同客户端登陆登出互相影响问题
            CookieInfo.SetBackLoginCookie(_sessionid);
            RedisSession.Set<SessionModel>(_sessionid, model);

        }
        #endregion

        #region Session取--yfx
        /// <summary>
        /// 获取用户信息
        /// </summary>
        public static SessionModel GetBackSession()
        {
            return GetBackSessionKey();
        }
        #endregion

        #region 快捷判断登陆状态--yfx
        /// <summary>
        /// 是否登录
        /// </summary>  
        public static bool IsBackLogin
        {
            get { return GetBackSession() != null ? true : false; }
        }
        #endregion       

        #region 根据sessionID清除Session  public--yfx
        /// <summary>
        /// 根据SessionID清除Session信息
        /// </summary>
        /// <param name="key">sessionID</param>
        public static void ClareBackSessionKey()
        {
            var _sessionid = GetBackSessionID();
            if (string.IsNullOrEmpty(_sessionid))
            {
                return;
            }
            RedisSession.ClearKey(_sessionid);
        }
        #endregion

        #region 根据sessionID给Session续期 public--yfx
        /// <summary>
        /// 根据Key给Session续期
        /// </summary>
        /// <param name="key">sessionID</param>
        /// <returns></returns>
        public static bool BackPostponeKey()
        {
            var _sessionid = GetBackSessionID();
            if (!string.IsNullOrEmpty(_sessionid))
            {
                return RedisSession.Postpone(GetBackSessionID());
            }
            return false;
        }
        #endregion

        #region Redis 取 private--yfx
        /// <summary>
        /// 获取Redis中的Session信息     
        /// </summary>
        /// <returns></returns>
        private static SessionModel GetBackSessionKey()
        {

            string useriD = GetBackSessionID();
            if (string.IsNullOrEmpty(useriD))
            {
                return null;
            }
            var model = RedisSession.Get<SessionModel>(useriD);
            return model;
        }
        #endregion

        #region 获取sessionID private--yfx 
        /// <summary>
        /// 获得sessionID(用户ID)
        /// useriD cookiesID
        /// </summary>
        /// <returns></returns>
        private static string GetBackSessionID()
        {
            return CookieInfo.GetBackLoginCookie();
        }
        #endregion 
        #endregion

    }
}
