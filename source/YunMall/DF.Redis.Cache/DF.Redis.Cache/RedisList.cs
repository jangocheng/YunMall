using  ServiceStack.Redis;
using  System;
using  System.Collections.Generic;
using  System.Linq;
using  System.Text;
using  System.Threading.Tasks;

namespace  DF.Redis.Cache
{
    // ========================================描述信息========================================
    // redis操作List列表数据结构的操作类，继承自RedisOperatorBase类，主要用于操作数据结构为List
    // 列表的缓存
    // ========================================创建信息========================================
    // 创建人：   
    // 创建时间： 2016-5-21     	
    // ========================================变更信息========================================
    // 日期          版本        修改人         描述
    // ========================================================================================
    public class RedisList: RedisOperatorBase
    {
        #region 添加
        /// <summary>
        /// 从左侧向list中添加值
        /// </summary>
        public static void LPush(string key,string value)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                client.PushItemToList(key,value);
                return true;
            };
             RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 从左侧向list中添加值，并设置过期时间
        /// </summary>
        public static bool LPush(string key, string value,DateTime dt)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                client.PushItemToList(key, value);
                return client.ExpireEntryAt(key, dt);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 从左侧向list中添加值，设置过期时间
        /// </summary>
        public static bool LPush(string key, string value, TimeSpan sp)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                client.PushItemToList(key, value);
                return client.ExpireEntryIn(key, sp);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 从右侧向list中添加值
        /// </summary>
        public static void RPush(string key, string value)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                client.PrependItemToList(key, value);
                return true;
            };
            RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 从右侧向list中添加值，并设置过期时间
        /// </summary>    
        public static bool RPush(string key, string value, DateTime dt)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                client.PrependItemToList(key, value);
                return client.ExpireEntryAt(key, dt);
               
            };
             return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 从右侧向list中添加值，并设置过期时间
        /// </summary>        
        public static bool RPush(string key, string value, TimeSpan sp)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                client.PrependItemToList(key, value);
                return client.ExpireEntryIn(key, sp);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 添加key/value
        /// </summary>     
        public static void Add(string key, string value)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                client.AddItemToList(key, value);
                return true;
            };
             RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 添加key/value ,并设置过期时间
        /// </summary>  
        public static bool Add(string key, string value,DateTime dt)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                client.AddItemToList(key, value);
                return client.ExpireEntryAt(key, dt);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 添加key/value。并添加过期时间
        /// </summary>  
        public static bool Add(string key, string value,TimeSpan sp)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                client.AddItemToList(key, value);
                return client.ExpireEntryIn(key, sp);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 为key添加多个值
        /// </summary>  
        public static void Add(string key, List<string> values)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                client.AddRangeToList(key, values);
                return true;
            };
             RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 为key添加多个值，并设置过期时间
        /// </summary>  
        public static void Add(string key, List<string> values,DateTime dt)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                client.AddRangeToList(key, values);
                return client.ExpireEntryAt(key, dt);
            };
            RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 为key添加多个值，并设置过期时间
        /// </summary>  
        public static void Add(string key, List<string> values,TimeSpan sp)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                client.AddRangeToList(key, values);
                return client.ExpireEntryIn(key, sp);
            };
            RedisPoolReadWrite(fun);
        }
        #endregion

        #region 获取值
        /// <summary>
        /// 获取list中key包含的数据数量
        /// </summary>  
        public static long Count(string key)
        {
            Func<IRedisClient, long> fun = (IRedisClient client) =>
            {
               return  client.GetListCount(key);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 获取key包含的所有数据集合
        /// </summary>  
        public static List<string> Get(string key)
        {
            Func<IRedisClient, List<string>> fun = (IRedisClient client) =>
            {
                return client.GetAllItemsFromList(key);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 获取key中下标为star到end的值集合,可用于分页
        /// </summary>  
        public static List<string> Get(string key,int star,int end)
        {
            Func<IRedisClient, List<string>> fun = (IRedisClient client) =>
            {
                return client.GetRangeFromList(key,star,end);
            };
            return RedisPoolReadWrite(fun);
        }        
        #endregion

        #region 阻塞命令
        /// <summary>
        ///  阻塞命令：从list中keys的尾部移除一个值，并返回移除的值，阻塞时间为sp
        /// </summary>  
        public static string BlockingPopItemFromList(string key,TimeSpan? sp)
        {
            Func<IRedisClient, string> fun = (IRedisClient client) =>
            {
                return client.BlockingPopItemFromList(key, sp);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        ///  阻塞命令：从list中keys的尾部移除一个值列表，并返回移除的值，阻塞时间为sp
        /// </summary>  
        public static ItemRef BlockingPopItemFromLists(string[] keys, TimeSpan? sp)
        {
            Func<IRedisClient, ItemRef> fun = (IRedisClient client) =>
            {
                return client.BlockingPopItemFromLists(keys, sp);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        ///  阻塞命令：从list中keys的尾部移除一个值，并返回移除的值，阻塞时间为sp
        /// </summary>  
        public static string BlockingDequeueItemFromList(string key, TimeSpan? sp)
        {
            Func<IRedisClient, string> fun = (IRedisClient client) =>
            {
                return client.BlockingDequeueItemFromList(key, sp);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 阻塞命令：从list中keys的尾部移除一个值列表，并返回移除的值列表，阻塞时间为sp
        /// </summary>  
        public static ItemRef BlockingDequeueItemFromLists(string[] keys, TimeSpan? sp)
        {
            Func<IRedisClient, ItemRef> fun = (IRedisClient client) =>
            {
                return client.BlockingDequeueItemFromLists(keys, sp);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 阻塞命令：从list中key的头部移除一个值，并返回移除的值，阻塞时间为sp
        /// </summary>  
        public static string BlockingRemoveStartFromList(string keys, TimeSpan? sp)
        {
            Func<IRedisClient, string> fun = (IRedisClient client) =>
            {
                return client.BlockingRemoveStartFromList(keys, sp);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 阻塞命令：从list中key的头部移除一个值，并返回移除的值，阻塞时间为sp
        /// </summary>  
        public static ItemRef BlockingRemoveStartFromLists(string[] keys, TimeSpan? sp)
        {
            Func<IRedisClient, ItemRef> fun = (IRedisClient client) =>
            {
                return client.BlockingRemoveStartFromLists(keys, sp);
            };
            return RedisPoolReadWrite(fun);
        }
        ///// <summary>
        ///// 阻塞命令：从list中一个fromkey的尾部移除一个值，添加到另外一个tokey的头部，并返回移除的值，阻塞时间为sp
        ///// </summary>  
        //public string BlockingPopAndPushItemBetweenLists(string fromkey, string tokey, TimeSpan? sp)
        //{
        //    return (new RedisBase()).Core.bBlockingPopAndPushItemBetweenLists(fromkey, tokey, sp);
        //}
        #endregion

        #region 删除
        /// <summary>
        /// 从尾部移除数据，返回移除的数据
        /// </summary>  
        public static string PopItemFromList(string key)
        {
            Func<IRedisClient, string> fun = (IRedisClient client) =>
            {
                return client.PopItemFromList(key);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 移除list中，key/value,与参数相同的值，并返回移除的数量
        /// </summary>  
        public static long RemoveItemFromList(string key,string value)
        {
            Func<IRedisClient, long> fun = (IRedisClient client) =>
            {
                return client.RemoveItemFromList(key,value);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 从list的尾部移除一个数据，返回移除的数据
        /// </summary>  
        public static string RemoveEndFromList(string key)
        {
            Func<IRedisClient, string> fun = (IRedisClient client) =>
            {
                return client.RemoveEndFromList(key);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 从list的头部移除一个数据，返回移除的值
        /// </summary>  
        public static string RemoveStartFromList(string key)
        {
            Func<IRedisClient, string> fun = (IRedisClient client) =>
            {
                return client.RemoveStartFromList(key);
            };
            return RedisPoolReadWrite(fun);
        }       
        #endregion

        #region 其它
        /// <summary>
        /// 从一个list的尾部移除一个数据，添加到另外一个list的头部，并返回移动的值
        /// </summary>  
        public static string PopAndPushItemBetweenLists(string fromKey, string toKey)
        {
            Func<IRedisClient, string> fun = (IRedisClient client) =>
            {
                return client.PopAndPushItemBetweenLists(fromKey, toKey);
            };
            return RedisPoolReadWrite(fun);
          
        }
        #endregion
    }
}
