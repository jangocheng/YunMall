using  System;
using  System.Collections.Generic;
using  System.Linq;
using  System.Text;
using  System.Threading.Tasks;

namespace  DF.Redis.Cache
{
    // ========================================描述信息========================================
    // Session管理类，主要用于统计使用RedisSession方式登录的用户数量及用户列表
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
    public class SessionManager
    {
      
        /// <summary>
        /// 用户数量
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return RedisHash.SearchKeys("session_" + "*").Count;
        }
       
        /// <summary>
        /// 在线用户SessionId列表
        /// </summary>
        /// <returns></returns>
        public IList<string> GetKeyAll() 
        {
            return RedisHash.SearchKeys("session_" + "*");
        }
    }
}
