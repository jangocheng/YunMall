using  DF.Redis.Cache;
using  System;
using  System.Collections.Generic;
using  System.Linq;
using  System.Text;
using  System.Threading.Tasks;

namespace  Common.MessagePublish
{
    /// <summary>
    /// redis消息队列消息发布类
    /// </summary>
    public class MessagePush
    {
        /// <summary>
        /// 生产者对象
        /// </summary>
        public static RedisProducer redisProducer { get; set; }

        /// <summary>
        /// 构造函数获取生产者对象
        /// </summary>
        public MessagePush()
        {
            redisProducer = new RedisProducer();
        }

        /// <summary>
        /// 消息发布方法
        /// </summary>
        /// <typeparam name="T">参数类型占位符</typeparam>
        /// <param name="model">参数：消息内容</param>
        /// <param name="channel">发布频道</param>
        public static void PushMessage<T>(T model,string channel)
        {
            if (redisProducer == null) { redisProducer = new RedisProducer(); }
            redisProducer.Channel = channel;
            redisProducer.Publish<T>(model);
        }

    }
}
