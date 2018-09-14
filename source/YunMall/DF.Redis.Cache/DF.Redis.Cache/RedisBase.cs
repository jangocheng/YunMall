
using  ServiceStack.Redis;
using  System;
using  System.Collections.Generic;
using  System.Linq;
using  System.Text;
using  System.Threading.Tasks;

namespace  DF.Redis.Cache
{
    // ========================================描述信息========================================
    // 是redis操作的基类，继承自IDisposable接口，主要用于释放内存
    // ========================================创建信息========================================
    // 创建人：   
    // 创建时间： 2016-5-21     	
    // ========================================变更信息========================================
    // 日期          版本        修改人         描述
    // 
    // ========================================================================================
    /// <summary>
    /// (new RedisBase())类，是redis操作的基类，继承自IDisposable接口，主要用于释放内存
    /// </summary>
    public class RedisBase
    {
        /// <summary>
        /// redis配置文件信息
        /// </summary>
        private static RedisConfig RedisConfig = RedisConfig.GetConfig();

        /// <summary>
        /// 保存数据DB文件到硬盘
        /// </summary>
        public static void Save()
        {
            IRedisClient Core;
            try
            {
                using  (Core = RedisManager.GetReadOnlyClient())
                {
                    Core.Save();
                }
            }
            catch (RedisException ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// 异步保存数据DB文件到硬盘
        /// </summary>
        public static void SaveAsync()
        {
            IRedisClient Core;
            try
            {
                using  (Core = RedisManager.GetReadOnlyClient())
                {
                    Core.SaveAsync();
                }
            }
            catch (RedisException ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// 从链接池中获取一个只读连接,并执行对应的方法
        /// </summary>
        /// <typeparam name="F"></typeparam>
        /// <param name="doRead"></param>
        /// <returns></returns>
        public static F RedisRead<F>(Func<IRedisClient, F> doRead)
        {
            IRedisClient Core;
            try
            {
                using  (Core = RedisManager.GetReadOnlyClient())
                {
                    return doRead(Core);
                }
            }
            catch (RedisException ex)
            {
               // return default(F);
                 throw new Exception("Redis写入异常.Host:"+ex.Message);
            }

        }

        /// <summary>
        /// 从链接池中获取一个连接,并执行对应的方法
        /// </summary>
        /// <typeparam name="F"></typeparam>
        /// <param name="doReadWrite">方法表达式</param>
        /// <returns></returns>
        public static F RedisPoolReadWrite<F>(Func<IRedisClient, F> doReadWrite)
        {
            IRedisClient Core;
            try
            {
                using  (Core = RedisManager.GetClient())
                {
                    return doReadWrite(Core);
                }
            }
            catch (RedisException ex)
            {
              
                // return default(F);
               // throw new Exception("Redis写入异常.Host:" + Core.Host + ",Port:" + Core.Port);
                throw new Exception(ex.Message);
            }

        }
    }
}
