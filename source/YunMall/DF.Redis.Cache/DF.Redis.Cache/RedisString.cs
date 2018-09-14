using  ServiceStack.Redis;
using  System;
using  System.Collections.Generic;
using  System.Linq;
using  System.Text;
using  System.Threading.Tasks;

namespace  DF.Redis.Cache
{
    // ========================================描述信息========================================
    // redis操作字符串数据结构的操作类，继承自RedisOperatorBase类，主要用于操作数据结构为字符串
    //的缓存
    // ========================================创建信息========================================
    // 创建人：   
    // 创建时间： 2016-5-21     	
    // ========================================变更信息========================================
    // 日期          版本        修改人         描述
    // ========================================================================================
    public class RedisString : RedisOperatorBase
    {
        #region 赋值
        /// <summary>
        /// 设置key的value
        /// </summary>
        public static bool Set(string key, string value)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
               return client.Set<string>(key, value);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 设置key的value并设置过期时间
        /// </summary>
        public static bool Set(string key, string value, DateTime dt)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                return client.Set<string>(key, value,dt);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 设置key的value并设置过期时间
        /// </summary>
        public static bool Set(string key, string value, TimeSpan expireIn)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                return client.Set<string>(key, value, expireIn);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 设置多个key/value
        /// </summary>
        public static void Set(Dictionary<string, string> dic)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                 client.SetAll(dic);
                 return true;
            };
            RedisPoolReadWrite(fun);
        }

        #endregion

        #region 追加
        /// <summary>
        /// 在原有key的value值之后追加value
        /// </summary>
        public static long Append(string key, string value)
        {
            Func<IRedisClient, long> fun = (IRedisClient client) =>
            {
               return client.AppendToValue(key,value);
            };
            return RedisPoolReadWrite(fun);
        }
        #endregion

        #region 获取值
        /// <summary>
        /// 获取key的value值
        /// </summary>
        public static string Get(string key)
        {
            Func<IRedisClient, string> fun = (IRedisClient client) =>
            {
                return client.GetValue(key);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 获取多个key的value值
        /// </summary>
        public static List<string> Get(List<string> keys)
        {
            Func<IRedisClient, List<string>> fun = (IRedisClient client) =>
            {
                return client.GetValues(keys);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 获取多个key的value值
        /// </summary>
        public static List<T> Get<T>(List<string> keys)
        {
            Func<IRedisClient, List<T>> fun = (IRedisClient client) =>
            {
                return client.GetValues<T>(keys);
            };
            return RedisPoolReadWrite(fun);
        }
        #endregion

        #region 获取旧值赋上新值
        /// <summary>
        /// 获取旧值赋上新值,暂时不能使用
        /// </summary>
        public static string GetAndSetValue(string key, string value)
        {
            //Func<IRedisClient, string> fun = (IRedisClient client) =>
            //{
            //    return client.GetAndSetValue(key,value);
            //};
            //return RedisPoolReadWrite(fun);
            return string.Empty;
          
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 获取值的长度,暂时不能使用
        /// </summary>
        public static long GetCount(string key)
        {
            //Func<IRedisClient, long> fun = (IRedisClient client) =>
            //{
            //    return client.GetStringCount(key);
            //};
            //return RedisPoolReadWrite(fun);
            return 0;
        }
        /// <summary>
        /// 自增1，返回自增后的值
        /// </summary>
        public static long Incr(string key)
        {
            Func<IRedisClient, long> fun = (IRedisClient client) =>
            {
                return client.IncrementValue(key);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 自增count，返回自增后的值
        /// </summary>
        public static long IncrBy(string key, int count)
        {
            Func<IRedisClient, long> fun = (IRedisClient client) =>
            {
                return client.IncrementValueBy(key, count);
            };
            return RedisPoolReadWrite(fun);
        }

        ///// <summary>
        ///// 自增count，返回自增后的值
        ///// </summary>
        //public static double IncrBy(string key, double count)
        //{
        //    Func<IRedisClient, double> fun = (IRedisClient client) =>
        //    {
        //        return client.IncrementValueBy(key, count);
        //    };
        //    return RedisPoolReadWrite(fun);
        //}
        /// <summary>
        /// 自减1，返回自减后的值
        /// </summary>
        public static long Decr(string key)
        {
            Func<IRedisClient, long> fun = (IRedisClient client) =>
            {
                return client.DecrementValue(key);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 自减count ，返回自减后的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static long DecrBy(string key, int count)
        {
            Func<IRedisClient, long> fun = (IRedisClient client) =>
            {
                return client.DecrementValueBy(key,count);
            };
            return RedisPoolReadWrite(fun);
        }
        #endregion
    }
}
