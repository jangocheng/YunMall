
using  ServiceStack.Redis;
using  System;
using  System.Collections.Generic;
using  System.Linq;
using  System.Text;
using  System.Threading.Tasks;

namespace  DF.Redis.Cache
{
    // ========================================描述信息========================================
    // redis操作hash数据结构的操作类，继承自RedisOperatorBase类，主要用于操作数据结构为hash缓存
    // ========================================创建信息========================================
    // 创建人：   
    // 创建时间： 2016-5-21     	
    // ========================================变更信息========================================
    // 日期          版本        修改人         描述
    // ========================================================================================
    public class RedisHash : RedisOperatorBase
    {
        #region 添加
        /// <summary>
        /// 向hash集合中添加key/value
        /// </summary>  
        /// <param name="hashid">hash的id key键</param>
        /// <param name="key">hash key键对应的field</param>
        /// <param name="value">hash field对应的值</param>
        /// <returns>添加成功则返回true,添加失败返回false</returns>

        public static bool SetEntryInHash(string hashid, string key, string value)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                return client.SetEntryInHash(hashid,key,value);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 加key/value 如果hashid集合中存在key/value,则不添加返回false，如果不存在则添加,返回true
        /// </summary>
        /// <param name="hashid">hash的id key键</param>
        /// <param name="key">hash key键对应的field</param>
        /// <param name="value">hash field对应的值</param>
        /// <returns>添加成功则返回true,添加失败返回false</returns>
        public static bool SetEntryInHashIfNotExists(string hashid, string key, string value)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                return client.SetEntryInHashIfNotExists(hashid, key, value);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 存储对象T t到hash集合中
        /// </summary>
        /// <typeparam name="T">类型占位符</typeparam>
        /// <param name="t">类型对象</param>
        public static void StoreAsHash<T>(T t)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                client.StoreAsHash<T>(t);
                return true;
            };
            RedisPoolReadWrite(fun);
        }
        #endregion

        #region 获取
        /// <summary>
        /// 获取对象T中ID为id的数据
        /// </summary>
        /// <typeparam name="T">类型占位符</typeparam>
        /// <param name="id">对象标识</param>
        /// <returns></returns>
        public static T GetFromHash<T>(object id)
        {
            Func<IRedisClient, T> fun = (IRedisClient client) =>
            {
               return client.GetFromHash<T>(id);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 获取所有hashid数据集的key/value数据集合
        /// </summary>
        /// <param name="hashid">hash的id key键</param>
        /// <returns>返回hashid下所有的域及值对应的字典</returns>
        public static Dictionary<string, string> GetAllEntriesFromHash(string hashid)
        {
            Func<IRedisClient, Dictionary<string, string>> fun = (IRedisClient client) =>
            {
                return client.GetAllEntriesFromHash(hashid);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 获取hashid数据集中的数据总数
        /// </summary>
        /// <param name="hashid">hash的id key键</param>
        /// <returns>返回找到的数量</returns>
        public static long GetHashCount(string hashid)
        {
            Func<IRedisClient, long> fun = (IRedisClient client) =>
            {
                return client.GetHashCount(hashid);
            };
            return RedisPoolReadWrite(fun);
        }

        /// <summary>
        /// 获取hashid数据集中所有key的集合
        /// </summary>
        /// <param name="hashid">hash的id key键</param>
        /// <returns>返回该hash id下所有key的集合</returns>
        public static List<string> GetHashKeys(string hashid)
        {
            Func<IRedisClient, List<string>> fun = (IRedisClient client) =>
            {
                return client.GetHashKeys(hashid);
            };
            return RedisPoolReadWrite(fun);      
        }
       
        /// <summary>
        /// 获取hashid数据集中的所有value集合
        /// </summary>
        /// <param name="hashid">hash的id key键</param>
        /// <returns>返回该hash id下所有value的集合</returns>
        public static List<string> GetHashValues(string hashid)
        {
            Func<IRedisClient, List<string>> fun = (IRedisClient client) =>
            {
                return client.GetHashValues(hashid);
            };
            return RedisPoolReadWrite(fun);
        }

        /// <summary>
        /// 获取hashid数据集中，key的value数据
        /// </summary>
        /// <param name="hashid">hash的id key键</param>
        /// <param name="key">hash的id key键</param>
        /// <returns>返回该hash id下field对应的value值</returns>
        public static string GetValueFromHash(string hashid, string key)
        {
            Func<IRedisClient, string> fun = (IRedisClient client) =>
            {
                return client.GetValueFromHash(hashid,key);
            };
            return RedisPoolReadWrite(fun);
        }
    
        /// <summary>
        /// 获取hashid数据集中，多个keys的value集合
        /// </summary>
        /// <param name="hashid">hash的id key键</param>
        /// <param name="keys">hash的id key键数组</param>
        /// <returns>返回该hash id下field数组对应的value列表</returns>
        public static List<string> GetValuesFromHash(string hashid, string[] keys)
        {
            Func<IRedisClient, List<string>> fun = (IRedisClient client) =>
            {
                return client.GetValuesFromHash(hashid, keys);
            };
            return RedisPoolReadWrite(fun);
        }
        #endregion

        #region 删除
        

        /// <summary>
        ///删除hashid数据集中的key数据
        /// </summary>
        /// <param name="hashid">hash的id key键</param>
        /// <param name="key">hash的id key键</param>
        /// <returns>删除成功返回true,删除失败返回false</returns>
        public static bool RemoveEntryFromHash(string hashid, string key)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                return client.RemoveEntryFromHash(hashid, key);
            };
            return RedisPoolReadWrite(fun);
        }
        #endregion


        #region 其它
        /// <summary>
        ///判断hashid数据集中是否存在key的数据
        /// </summary>
        /// <param name="hashid">hash的id key键</param>
        /// <param name="key">hash的id key键</param>
        /// <returns>存在返回true,不存在返回false</returns>
        public static bool HashContainsEntry(string hashid, string key)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                return client.HashContainsEntry(hashid, key);
            };
            return RedisPoolReadWrite(fun);  
        }

        /// <summary>
        ///给hashid数据集key的value加countby，返回相加后的数据
        /// </summary>
        /// <param name="hashid">hash的id key键</param>
        /// <param name="key">hash的id key键</param>
        /// <returns>返回相加后的数据</returns>
        public static long IncrementValueInHash(string hashid, string key, int countBy)
        {

            Func<IRedisClient, long> fun = (IRedisClient client) =>
            {
                return client.IncrementValueInHash(hashid, key, countBy);
            };
            return RedisPoolReadWrite(fun);
        }


        #endregion
    }
}
