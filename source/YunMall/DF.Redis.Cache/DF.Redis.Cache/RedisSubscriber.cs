using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace  DF.Redis.Cache
{

    /// <summary>
    /// Redis消息订阅者类
    /// </summary>
    public class RedisSubscriber : RedisOperatorBase
    {
        /// <summary>
        /// 订阅类
        /// </summary>
        private RedisSubscription redisSubscription;
        private RedisClient redisClient ;
        //频道名称
        private string channelName;
        /// <summary>
        /// 实例化时频道名称
        /// </summary>
        /// <param name="channelName"></param>
        public RedisSubscriber(string channelName):this()
        {
            this.channelName = channelName;
        }
        /// <summary>
        /// 当订阅时事件,参数为频道
        /// </summary>
        public Action<string> OnSubscribe
        {
            get { return redisSubscription.OnSubscribe; }
            set { redisSubscription.OnSubscribe = value; }
        }

        /// <summary>
        /// 当有消息通知时事件
        /// 参数为频道和消息
        /// </summary>
        public Action<string, string> OnMessage
        {
            get { return redisSubscription.OnMessage; }
            set { redisSubscription.OnMessage = value; }
        }

        /// <summary>
        /// 当取消订阅时事件
        /// 参数为频道
        /// </summary>
        public Action<string> OnUnSubscribe
        {
            get { return redisSubscription.OnUnSubscribe; }
            set { redisSubscription.OnUnSubscribe = value; }
        }
        /// <summary>
        /// 订阅数量
        /// </summary>
        public long SubscriptionCount
        {
            get { return redisSubscription.SubscriptionCount; }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public RedisSubscriber()
        {
            this.redisClient = (RedisClient)RedisManager.GetClient();
            this.redisSubscription = new RedisSubscription(redisClient);
        }

        /// <summary>
        /// 订阅类实例化时指定的频道,阻塞线程等待消息
        /// </summary>
        public void SubscribeToChannels()
        {
            try {
                redisSubscription.SubscribeToChannels(channelName);
            }
            catch {
                SubscribeToChannels();
            }
        }


        /// <summary>
        /// 订阅频道,阻塞线程等待消息
        /// </summary>
        /// <param name="channels"></param>
        public void SubscribeToChannels(params string[] channels)
        {
            redisSubscription.SubscribeToChannels(channels);
        }

        /// <summary>
        /// 订阅匹配,阻塞线程等待消息
        /// </summary>
        /// <param name="patterns">正则表达式</param>
        public void SubscribeToChannelsMatching(params string[] patterns)
        {
            redisSubscription.SubscribeToChannelsMatching(patterns);
        }
    }
}
