
using  ServiceStack.Redis;
using  System;
using  System.Collections.Generic;
using  System.Configuration;
using  System.Linq;
using  System.Text;
using  System.Threading.Tasks;

namespace  DF.Redis.Cache
{
    // ========================================描述信息========================================
    // redis数据链接管理类，主要用于读取配置文件中的redis配置节，建立线程池并返回客户端操作实例
    // 列表的缓存
    // ========================================创建信息========================================
    // 创建人：   
    // 创建时间： 2016-5-21     	
    // ========================================变更信息========================================
    // 日期          版本        修改人         描述
    // ========================================================================================
    public class RedisManager
    {
       
        /// <summary>
        /// 过期时间
        /// </summary>
      internal static int TimeOut = 20;
        /// <summary>
        /// redis配置文件信息
        /// </summary>
        private static RedisConfig RedisConfig = RedisConfig.GetConfig();

        private static PooledRedisClientManager prcm;

        /// <summary>
        /// 静态构造方法，初始化链接池管理对象
        /// </summary>
        static RedisManager()
        {
            CreateManager();
        }

        /// <summary>
        /// 创建链接池管理对象,自动负载均衡
        /// </summary>
        private static void CreateManager()
        {

            string[] WriteServerConStr = SplitString(RedisConfig.WriteServerConStr, ",");
            string[] ReadServerConStr = SplitString(RedisConfig.ReadServerConStr, ",");
            TimeOut = (TimeOut == 0) ? TimeOut : RedisConfig.SessionTimeOut;
            int timeOut = RedisConfig.SessionTimeOut;//设置session有效时间
            if (timeOut != 0)
            {
                TimeOut = timeOut;
            }

            prcm = new PooledRedisClientManager(WriteServerConStr, ReadServerConStr,
                             new RedisClientManagerConfig
                             {
                                 MaxWritePoolSize = RedisConfig.MaxWritePoolSize,
                                 MaxReadPoolSize = RedisConfig.MaxReadPoolSize,
                                 AutoStart = RedisConfig.AutoStart,
                             });

        }

        private static string[] SplitString(string strSource, string split)
        {
            return strSource.Split(split.ToArray());
        }
        /// <summary>
        /// 获取读写连接
        /// </summary>
        public static IRedisClient GetClient()
        {
            if (prcm == null)
                CreateManager();
            return prcm.GetClient();
        }

        /// <summary>
        /// 获取只读操作连接
        /// </summary>
        public static IRedisClient GetReadOnlyClient()
        {
            if (prcm == null)
                CreateManager();
            return prcm.GetReadOnlyClient();
        }
    }
}
