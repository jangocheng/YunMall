using  ServiceStack.Redis;
using  System;
using  System.Collections.Generic;
using  System.Linq;
using  System.Text;
using  System.Threading.Tasks;

namespace  DF.Redis.Cache
{
    // ========================================描述信息========================================
    // redis操作Set集合数据结构的操作类，继承自RedisOperatorBase类，主要用于操作数据结构为set集合缓存
    // ========================================创建信息========================================
    // 创建人：   
    // 创建时间： 2016-5-21     	
    // ========================================变更信息========================================
    // 日期          版本        修改人         描述
    // ========================================================================================
    public class RedisSet: RedisOperatorBase
    {
        #region 添加
        /// <summary>
        /// key集合中添加value值
        /// </summary>
        public static void Add(string key, string value)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                client.AddItemToSet(key,value);
                return true;
            };
            RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// key集合中添加list集合
        /// </summary>
        public static void Add(string key, List<string> list)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                client.AddRangeToSet(key, list);
                return true;
            };
            RedisPoolReadWrite(fun);
        }
        #endregion

        #region 获取
        /// <summary>
        /// 随机获取key集合中的一个值
        /// </summary>
        public static string GetRandomItemFromSet(string key)
        {
            Func<IRedisClient, string> fun = (IRedisClient client) =>
            {
               return client.GetRandomItemFromSet(key);
            };
           return  RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 获取key集合值的数量
        /// </summary>
        public static long GetCount(string key)
        {
            Func<IRedisClient, long> fun = (IRedisClient client) =>
            {
                return client.GetSetCount(key);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 获取所有key集合的值
        /// </summary>
        public static HashSet<string> GetAllItemsFromSet(string key)
        {
            Func<IRedisClient, HashSet<string>> fun = (IRedisClient client) =>
            {
                return client.GetAllItemsFromSet(key);
            };
            return RedisPoolReadWrite(fun);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 随机删除key集合中的一个值
        /// </summary>
        public static string PopItemFromSet(string key)
        {
            Func<IRedisClient, string> fun = (IRedisClient client) =>
            {
                return client.PopItemFromSet(key);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 删除key集合中的value
        /// </summary>
        public static void RemoveItemFromSet(string key, string value)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                 client.RemoveItemFromSet(key, value);
                  return true;
            };
            RedisPoolReadWrite(fun);
        }
        #endregion

        #region 其它
        /// <summary>
        /// 从fromkey集合中移除值为value的值，并把value添加到tokey集合中
        /// </summary>
        public static void MoveBetweenSets(string fromkey,string tokey,string value)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                client.MoveBetweenSets(fromkey, tokey, value);
                return true;
            };
            RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 返回keys多个集合中的并集，返还hashset
        /// </summary>
        public static HashSet<string> GetUnionFromSets(string[] keys)
        {
            Func<IRedisClient, HashSet<string>> fun = (IRedisClient client) =>
            {
                return client.GetUnionFromSets(keys);
             
            };
           return  RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// keys多个集合中的并集，放入newkey集合中
        /// </summary>
        public static void StoreUnionFromSets(string newkey, string[] keys)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                client.StoreUnionFromSets(newkey, keys);
                return true;
            };
            RedisPoolReadWrite(fun);
         
        }
        /// <summary>
        /// 把fromkey集合中的数据与keys集合中的数据对比，fromkey集合中不存在keys集合中，则把这些不存在的数据放入newkey集合中
        /// </summary>
        public static void StoreDifferencesFromSet(string newkey, string fromkey, string[] keys)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                client.StoreDifferencesFromSet(newkey, fromkey, keys);
                return true;
            };
            RedisPoolReadWrite(fun);
        }
        #endregion
    }
}
