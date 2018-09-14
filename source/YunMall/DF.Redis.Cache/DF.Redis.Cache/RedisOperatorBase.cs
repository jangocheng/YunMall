
using  ServiceStack.Redis;
using  System;
using  System.Collections.Generic;
using  System.Linq;
using  System.Text;
using  System.Threading.Tasks;

namespace  DF.Redis.Cache
{
    // ========================================描述信息========================================
    // redis操作基类，继承自(new RedisBase())类，主要作为Redis公用方法的基类操作
    // ========================================创建信息========================================
    // 创建人：   
    // 创建时间： 2016-5-21     	
    // ========================================变更信息========================================
    // 日期          版本        修改人         描述
    // 
    // ========================================================================================
    /// <summary>
    /// redis操作基类
    /// </summary>
    public class RedisOperatorBase : RedisBase
    {


        /// <summary>
        /// 发送消息到指定Channel       
        /// </summary>
        /// <typeparam name="toChannel">频道</typeparam>
        /// <param name="message">要发送的消息</param>
        /// <returns></returns>
        public static long PublishMessage(string toChannel, string message)
        {
            Func<IRedisClient, long> fun = (IRedisClient client) =>
            {
                return client.PublishMessage(toChannel, message);
            };
            return RedisPoolReadWrite(fun);
        }



        /// <summary>
        /// 返回根据条件查找到的KEY对象列表        
        /// </summary>
        /// <typeparam name="IList<string>">字符串列表</typeparam>
        /// <param name="pattern">正则表达式</param>
        /// <returns>返回字符串列表</returns>
        public static IList<string> SearchKeys(string pattern)
        {
            Func<IRedisClient, IList<string>> fun = (IRedisClient client) =>
            {
                return client.SearchKeys(pattern);
            };
            return RedisPoolReadWrite(fun);
        }

        /// <summary>
        /// 获取key的value值
        /// </summary>
        public static T GetValue<T>(string key)
        {
            Func<IRedisClient, T> fun = (IRedisClient client) =>
            {
                return client.Get<T>(key);
            };
            return RedisPoolReadWrite(fun);
        }

        /// <summary>
        /// 获取key的value值
        /// </summary>
        public static string GetValue(string key)
        {
            Func<IRedisClient, string> fun = (IRedisClient client) =>
            {
                return client.GetValue(key);
            };
            return RedisPoolReadWrite(fun);
        }

        /// <summary>
        /// 设置缓存过期
        /// </summary>
        /// <param name="key">要设置过期的键key</param>
        /// <param name="datetime">过期截止时间</param>
        /// <returns>成功:true,失败:false</returns>
        public static bool SetExpire(string key, DateTime datetime)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                return client.ExpireEntryAt(key, datetime);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 设置缓存过期时间
        /// </summary>
        /// <param name="key">要设置过期的键key</param>
        /// <param name="expireIn">多久过期的TimeSpan</param>
        /// <returns>成功:true,失败:false</returns>
        public static bool SetExpire(string key, TimeSpan expireIn)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                return client.ExpireEntryIn(key, expireIn);
            };
            return RedisPoolReadWrite(fun);
        }


        /// <summary>
        /// 设置缓存过期
        /// </summary>
        /// <param name="key">要设置过期的键key</param>
        /// <param name="value">要设置过期的键对应的值</param>
        /// <param name="datetime">过期截止时间</param>
        /// <returns>成功:true,失败:false</returns>
        public static bool Set<T>(string key, T value, DateTime datetime)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                return client.Set<T>(key, value, datetime);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 设置缓存过期时间
        /// </summary>
        /// <param name="key">要设置过期的键key</param>
        /// <param name="value">要设置过期的键对应的值</param>
        /// <param name="expireIn">多久过期的TimeSpan</param>
        /// <returns>成功:true,失败:false</returns>
        public static bool Set<T>(string key, T value, TimeSpan expireIn)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                return client.Set<T>(key, value, expireIn);
            };
            return RedisPoolReadWrite(fun);
        }


        /// <summary>
        /// 设置缓存过期时间
        /// </summary>
        /// <param name="key">要设置过期的键key</param>
        /// <param name="value">要设置过期的键对应的值</param>
        /// <returns>成功:true,失败:false</returns>
        public static bool Set<T>(string key, T value)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                return client.Set<T>(key, value);
            };
            return RedisPoolReadWrite(fun);
        }


        /// <summary>
        /// 获取当前key所剩的存活时间
        /// </summary>
        /// <param name="key">键key</param>
        /// <returns>剩余存活时间</returns>
        public static TimeSpan GetTimeToLive(string key)
        {
            Func<IRedisClient, TimeSpan> fun = (IRedisClient client) =>
            {
                return client.GetTimeToLive(key);
            };
            return RedisPoolReadWrite(fun);
        }


        /// <summary>
        /// 判断指定的键key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns>存在true,不存在false</returns>
        public static bool ContainsKey(string key)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                return client.ContainsKey(key);
            };
            return RedisPoolReadWrite(fun);
        }


        /// <summary>
        /// 移除键值
        /// </summary>
        /// <param name="key">要移除的键key</param>
        /// <param name="tp">多久过期的TimeSpan</param>
        /// <returns>成功:true,失败:false</returns>
        public static bool Remove(string key)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                return client.Remove(key);
            };
            return RedisPoolReadWrite(fun);
        }

        /// <summary>
        /// 批量移除键值--yfx
        /// </summary>
        /// <param name="key">要移除的键key</param>        
        /// <returns>成功:true,失败:false</returns>
        public static bool RemoveAll(IEnumerable<string> key)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                client.RemoveAll(key);
                return true;
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 清空数据库
        /// </summary>
        public static void PushAll()
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                client.FlushAll();
                return true;
            };
            RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// key重命名
        /// </summary>
        public static void RenameKey(string fromName, string toName)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                client.RenameKey(fromName, toName);
                return true;
            };
            RedisPoolReadWrite(fun);
        }
    }
}
