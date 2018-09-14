using  System;
using  System.Collections.Generic;
using  System.Linq;
using  System.Text;
using  System.Threading.Tasks;
using  System.Web;

namespace  DF.Redis.Cache
{
    // ========================================描述信息========================================
    // redis操作Session管理类，主要用于根据Cookie读取对应的缓存,使用此类注意此session并不是原生
    // 的Session
    /// </summary>
    // 备注：使用客户端cookie获取用户标示,然后根据此标识读取Redis中对应的缓存信息,以此来实现模拟
    // session的作用
    // ========================================创建信息========================================
    // 创建人：   
    // 创建时间： 2016-5-21     	
    // ========================================变更信息========================================
    // 日期          版本        修改人         描述
    // ========================================================================================
    public class RedisSession
    {
        //Session前缀名,用于分析识别
        private const string preSession = "session_";
       
        /// <summary>
        /// 当前SessionId
        /// </summary>
        public string SessionId { get; set; }


        /// <summary>
        /// 初始化Session使用cookieID
        /// </summary>
        /// <param name="sessionID">session对应的CookieID=sesssionID</ param >
        public RedisSession(string sessionID)
        {
            if (sessionID != null)
            {
                SessionId = sessionID;
            }
        }

        /// <summary>
        /// 读取Session值
        /// </summary>
        /// <param name="key">cookieID</param>
        /// <returns></returns>
        public object this[string key]
        {
            get
            {
                string sessionField = preSession + key;
                string test = RedisHash.GetValueFromHash(SessionId, sessionField);
 //               HeShang365.Log.Logger.Info("获取键值", test + "," + sessionField + "," + SessionId + "," + DateTime.Now.ToShortDateString());
                return test;
            }
            set
            {
                SetSession(key, value);
            }
        }


        /// <summary>
        /// 判断指定的用户是否存在（通过userid,即cookieKey）是否存在
        /// </summary>
        /// <param name="CookieKey">用户的userID</param>
        /// <returns>存在true,不存在false</returns>
        public bool ContainsKey(string CookieKey)
        {
            return RedisHash.ContainsKey(CookieKey);
        }

        /// <summary>
        /// 存入到缓存中
        /// </summary>
        /// <param name="key">要存入的键</param>
        /// <param name="value">要存入的键对应的值</param>
        private void SetSession(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new Exception("Key is Null or Epmty");
            }
            string sessionField = preSession + key;
            //写入键值
            bool b = RedisHash.SetEntryInHash(SessionId, sessionField, value.ToString());
//            HeShang365.Log.Logger.Info("设置键值", b.ToString() + "," + key + "," + SessionId + ","+value+"," + DateTime.Now.ToShortDateString());
           
            bool c = RedisHash.SetExpire(SessionId, new TimeSpan(0, RedisManager.TimeOut, 0));//设置有效期
//            HeShang365.Log.Logger.Info("设置有效期", c.ToString() + "," + SessionId + DateTime.Now.ToShortDateString());

        }

        /// <summary>
        /// 设置Session到Redis
        /// </summary>
        /// <param name="key">sessionID=cookieID即用户ID </param>
        /// <param name="value">值</param>
        public static bool Set(string key, string value)
        {
            bool b = RedisString.Set(key, value, new TimeSpan(0, RedisManager.TimeOut, 0));
//            HeShang365.Log.Logger.Info("设置键值1:", key + "," + value + "," + DateTime.Now.ToString());
            return b;
        }


        /// <summary>
        /// 获取Session值
        /// </summary>
        /// <param name="key">sessionID=cookieID即用户ID</param>
        /// <returns>返回SessionID对应的值</returns>
        public static  string Get(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new Exception("Key is Null or Epmty");
            }
            string keyValues = RedisString.Get(key);
           // bool b = RedisString.SetExpire(key,new TimeSpan(0, RedisManager.TimeOut, 0));
            //  bool b1 = redisString.Set(SessionId + key, value.ToString(), new TimeSpan(0, RedisManager.TimeOut, 0));
//            HeShang365.Log.Logger.Info("获取键值1:", key + "," + keyValues + "," + DateTime.Now.ToString());
            return keyValues;
        }


        /// <summary>
        /// 设置Session到Redis
        /// </summary>
        /// <param name="key">sessionID=cookieID即用户ID </param>
        /// <param name="value">自定义类型</param>
        public static bool Set<T>(string key, T value)
        {
            bool b = RedisString.Set<T>(key, value, new TimeSpan(0, RedisManager.TimeOut, 0));
//            HeShang365.Log.Logger.Info("设置键值1:", key + "," + value + "," + DateTime.Now.ToString());
            return b;
        }


        // <summary>
        //获取Session值
        //</summary>
        //<param name="key">sessionID = cookieID即用户ID</param>
        //<returns>返回SessionID对应的自定义对象值</returns>
        public static T Get<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new Exception("Key is Null or Epmty");
            }
            return RedisString.GetValue<T>(key);
        }


        /// <summary>
        /// 清空Sesion
        /// </summary>
        public bool Clear()
        {
           return RedisHash.Remove(SessionId);//删除该hash键
        }
        /// <summary>
        /// 删除session
        /// 7.25 zl
        /// </summary>
        /// <returns></returns>
        public static bool ClearKey(string sessionId)
        {
            return RedisHash.Remove(sessionId);//删除该hash键
        }

        /// <summary>
        /// 给Session续期
        /// </summary>
        public bool Postpone()
        {
            bool b = RedisHash.SetExpire(SessionId, new TimeSpan(0, RedisManager.TimeOut, 0));
//            HeShang365.Log.Logger.Info("续期", b.ToString() + "," + SessionId + DateTime.Now.ToShortDateString());
            return b;

        }


        /// <summary>
        /// 给Session续期
        /// </summary>
        public static bool Postpone(string sessionId)
        {
            bool b = RedisHash.SetExpire(sessionId, new TimeSpan(0, RedisManager.TimeOut, 0));
//            HeShang365.Log.Logger.Info("续期", b.ToString() + "," + sessionId + DateTime.Now.ToShortDateString());
            return b;

        }
    }
}
