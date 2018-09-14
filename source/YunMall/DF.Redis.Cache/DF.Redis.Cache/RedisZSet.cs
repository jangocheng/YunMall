using  ServiceStack.Redis;
using  System;
using  System.Collections.Generic;
using  System.Linq;
using  System.Text;
using  System.Threading.Tasks;

namespace  DF.Redis.Cache
{
    // ========================================描述信息========================================
    // redis操作有序集合（sorted sets操作类，继承自RedisOperatorBase类，主要用于操作数据结构为有
    //序集合的缓存
    // ========================================创建信息========================================
    // 创建人：   
    // 创建时间： 2016-5-21     	
    // ========================================变更信息========================================
    // 日期          版本        修改人         描述
    // ========================================================================================
    public class RedisZSet : RedisOperatorBase
    {
        #region 添加
        /// <summary>
        /// 添加key/value，默认分数是从1.多*10的9次方以此递增的,自带自增效果
        /// </summary>
        public static bool AddItemToSortedSet(string key, string value)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                return client.AddItemToSortedSet(key, value);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 添加key/value,并设置value的分数
        /// </summary>
        public static bool AddItemToSortedSet(string key, string value, double score)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                return client.AddItemToSortedSet(key, value, score);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 为key添加values集合，values集合中每个value的分数设置为score
        /// </summary>
        public static bool AddRangeToSortedSet(string key,List<string> values,double score)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                return client.AddRangeToSortedSet(key, values, score);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 为key添加values集合，values集合中每个value的分数设置为score
        /// </summary>
        public static bool AddRangeToSortedSet(string key, List<string> values, long score)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                return client.AddRangeToSortedSet(key, values, score);
            };
            return RedisPoolReadWrite(fun);
        }
        #endregion

        #region 获取
        /// <summary>
        /// 获取key的所有集合
        /// </summary>
        public static List<string> GetAllItemsFromSortedSet(string key)
        {
            Func<IRedisClient, List<string>> fun = (IRedisClient client) =>
            {
                return client.GetAllItemsFromSortedSet(key);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 获取key的所有集合，倒序输出
        /// </summary>
        public static List<string> GetAllItemsFromSortedSetDesc(string key)
        {
            Func<IRedisClient, List<string>> fun = (IRedisClient client) =>
            {
                return client.GetAllItemsFromSortedSetDesc(key);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 获取给定键的有序集合，带分数
        /// </summary>
        public static IDictionary<string, double> GetAllWithScoresFromSortedSet(string key)
        {
            Func<IRedisClient, IDictionary<string, double>> fun = (IRedisClient client) =>
            {
                return client.GetAllWithScoresFromSortedSet(key);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 获取key为value的下标值
        /// </summary>
        public static long GetItemIndexInSortedSet(string key, string value)
        {
            Func<IRedisClient, long> fun = (IRedisClient client) =>
            {
                return client.GetItemIndexInSortedSet(key,value);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 倒序排列获取key为value的下标值
        /// </summary>
        public static long GetItemIndexInSortedSetDesc(string key, string value)
        {
            Func<IRedisClient, long> fun = (IRedisClient client) =>
            {
                return client.GetItemIndexInSortedSetDesc(key, value);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 获取key为value的分数
        /// </summary>
        public static double GetItemScoreInSortedSet(string key,string value)
        {
            Func<IRedisClient, double> fun = (IRedisClient client) =>
            {
                return client.GetItemScoreInSortedSet(key, value);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 获取key所有集合的数据总数
        /// </summary>
        public static long GetSortedSetCount(string key)
        {
            Func<IRedisClient, long> fun = (IRedisClient client) =>
            {
                return client.GetSortedSetCount(key);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// key集合数据从分数为fromscore到分数为toscore的数据总数
        /// </summary>
        public static long GetSortedSetCount(string key,double fromScore,double toScore)
        {
            Func<IRedisClient, long> fun = (IRedisClient client) =>
            {
                return client.GetSortedSetCount(key, fromScore, toScore);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 获取key集合从高分到低分排序数据，分数从fromscore到分数为toscore的数据
        /// </summary>
        public static List<string> GetRangeFromSortedSetByHighestScore(string key, double fromscore, double toscore)
        {
            Func<IRedisClient, List<string>> fun = (IRedisClient client) =>
            {
                return client.GetRangeFromSortedSetByHighestScore(key, fromscore, toscore);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 获取key集合从低分到高分排序数据，分数从fromscore到分数为toscore的数据
        /// </summary>
        public static List<string> GetRangeFromSortedSetByLowestScore(string key, double fromscore, double toscore)
        {
            Func<IRedisClient, List<string>> fun = (IRedisClient client) =>
            {
                return client.GetRangeFromSortedSetByLowestScore(key, fromscore, toscore);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 获取key集合从高分到低分排序数据，分数从fromscore到分数为toscore的数据，带分数
        /// </summary>
        public static IDictionary<string, double> GetRangeWithScoresFromSortedSetByHighestScore(string key, double fromscore, double toscore)
        {
            Func<IRedisClient, IDictionary<string, double>> fun = (IRedisClient client) =>
            {
                return client.GetRangeWithScoresFromSortedSetByHighestScore(key, fromscore, toscore);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        ///  获取key集合从低分到高分排序数据，分数从fromscore到分数为toscore的数据，带分数
        /// </summary>
        public static IDictionary<string, double> GetRangeWithScoresFromSortedSetByLowestScore(string key, double fromscore, double toscore)
        {
            Func<IRedisClient, IDictionary<string, double>> fun = (IRedisClient client) =>
            {
                return client.GetRangeWithScoresFromSortedSetByLowestScore(key, fromscore, toscore);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        ///  获取key集合数据，下标从fromRank到分数为toRank的数据
        /// </summary>
        public static List<string> GetRangeFromSortedSet(string key, int fromRank, int toRank)
        {
            Func<IRedisClient, List<string>> fun = (IRedisClient client) =>
            {
                return client.GetRangeFromSortedSet(key, fromRank, toRank);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 获取key集合倒叙排列数据，下标从fromRank到分数为toRank的数据
        /// </summary>
        public static List<string> GetRangeFromSortedSetDesc(string key, int fromRank, int toRank)
        {
            Func<IRedisClient, List<string>> fun = (IRedisClient client) =>
            {
                return client.GetRangeFromSortedSetDesc(key, fromRank, toRank);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 获取key集合数据，下标从fromRank到分数为toRank的数据，带分数
        /// </summary>
        public static IDictionary<string, double> GetRangeWithScoresFromSortedSet(string key, int fromRank, int toRank)
        {
            Func<IRedisClient, IDictionary<string, double>> fun = (IRedisClient client) =>
            {
                return client.GetRangeWithScoresFromSortedSet(key, fromRank, toRank);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        ///  获取key集合倒叙排列数据，下标从fromRank到分数为toRank的数据，带分数
        /// </summary>
        public static IDictionary<string, double> GetRangeWithScoresFromSortedSetDesc(string key, int fromRank, int toRank)
        {
            Func<IRedisClient, IDictionary<string, double>> fun = (IRedisClient client) =>
            {
                return client.GetRangeWithScoresFromSortedSetDesc(key, fromRank, toRank);
            };
            return RedisPoolReadWrite(fun);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除key为value的数据
        /// </summary>
        public static bool RemoveItemFromSortedSet(string key,string value)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                return client.RemoveItemFromSortedSet(key, value);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 删除下标从minRank到maxRank的key集合数据
        /// </summary>
        public static long RemoveRangeFromSortedSet(string key,int minRank,int maxRank)
        {
            Func<IRedisClient, long> fun = (IRedisClient client) =>
            {
                return client.RemoveRangeFromSortedSet(key, minRank, maxRank);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 删除分数从fromscore到toscore的key集合数据
        /// </summary>
        public static long RemoveRangeFromSortedSetByScore(string key, double fromscore, double toscore)
        {
            Func<IRedisClient, long> fun = (IRedisClient client) =>
            {
                return client.RemoveRangeFromSortedSetByScore(key, fromscore, toscore);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 删除key集合中分数最大的数据
        /// </summary>
        public static string PopItemWithHighestScoreFromSortedSet(string key)
        {
            Func<IRedisClient, string> fun = (IRedisClient client) =>
            {
                return client.PopItemWithHighestScoreFromSortedSet(key);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 删除key集合中分数最小的数据
        /// </summary>
        public static string PopItemWithLowestScoreFromSortedSet(string key)
        {
            Func<IRedisClient, string> fun = (IRedisClient client) =>
            {
                return client.PopItemWithLowestScoreFromSortedSet(key);
            };
            return RedisPoolReadWrite(fun);
        }
        #endregion

        #region 其它
        /// <summary>
        /// 判断key集合中是否存在value数据
        /// </summary>
        public static bool SortedSetContainsItem(string key, string value)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                return client.SortedSetContainsItem(key,value);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 为key集合值为value的数据，分数加scoreby，返回相加后的分数
        /// </summary>
        public static double IncrementItemInSortedSet(string key,string value,double scoreBy)
        {
            Func<IRedisClient, double> fun = (IRedisClient client) =>
            {
                return client.IncrementItemInSortedSet(key, value, scoreBy);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 获取keys多个集合的交集，并把交集添加的newkey集合中，返回交集数据的总数
        /// </summary>
        public static long StoreIntersectFromSortedSets(string newkey, string[] keys)
        {
            Func<IRedisClient, long> fun = (IRedisClient client) =>
            {
                return client.StoreIntersectFromSortedSets(newkey, keys);
            };
            return RedisPoolReadWrite(fun);
        }
        /// <summary>
        /// 获取keys多个集合的并集，并把并集数据添加到newkey集合中，返回并集数据的总数
        /// </summary>
        public static long StoreUnionFromSortedSets(string newkey, string[] keys)
        {
            Func<IRedisClient, long> fun = (IRedisClient client) =>
            {
                return client.StoreUnionFromSortedSets(newkey, keys);
            };
            return RedisPoolReadWrite(fun);
        }
        #endregion
    }
}
