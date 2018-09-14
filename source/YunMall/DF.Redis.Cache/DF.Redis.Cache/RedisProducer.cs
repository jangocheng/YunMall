using  System;
using  System.Collections.Generic;
using  System.Linq;
using  System.Text;
using  System.Threading.Tasks;

namespace  DF.Redis.Cache
{

    /// <summary>
    /// redis生产者,用于消息的发布订阅模式
    /// </summary>
    public class RedisProducer : RedisOperatorBase
    {
        /// <summary>
        /// 订阅的频道
        /// </summary>
        private string channelName;
        /// <summary>
        /// 构造函数,实例化一个频道
        /// </summary>
        /// <param name="channel"></param>
        public RedisProducer(string channel)
        {
            this.channelName = channel;
        }

        public RedisProducer()
        { }

        /// <summary>
        /// 频道属性,不使用构造函数时使用
        /// </summary>
        public string Channel
        {
            get { return channelName; }

            set { channelName = value; }
        }

        /// <summary>
        /// 生产一个字符串消息到发布到类实例化时指定的频道上
        /// </summary>
        /// <param name="message">字符串消息</param>
        public void Publish(string message)
        {
            PublishMessage(channelName, message);
        }

        /// <summary>
        /// 生产一个自定义消息到发布到类实例化时指定的频道上
        /// </summary>
        /// <typeparam name="T">自定义实体类</typeparam>
        /// <param name="messageBody">实体类对象</param>
        public void Publish<T>(T messageBody)
        {
            string strContent = Newtonsoft.Json.JsonConvert.SerializeObject(messageBody);
            PublishMessage(channelName, strContent);
        }

        /// <summary>
        /// 生产一个使用Message消息包装的实体对象到发布到指定的频道上
        /// </summary>
        /// <typeparam name="T">自定义实体类</typeparam>
        /// <param name="message">消息对象</param>
        public void Publish<T>(Message<T> message)
        {
            T content = message.Content;
            string strContent = Newtonsoft.Json.JsonConvert.SerializeObject(content);
            PublishMessage(channelName, strContent);
        }


        /// <summary>
        /// 生产一个消息到发布到对应的频道上
        /// </summary>
        /// <param name="channel">频道名称</param>
        /// <param name="message">字符串消息</param>
        public void Publish(string channel,string message)
        {
            PublishMessage(channel, message);
        }

        /// <summary>
        /// 生产一个自定义消息到发布到对应的频道上
        /// </summary>
        /// <param name="channel">频道名称</param>
        /// <typeparam name="T">自定义实体类</typeparam>
        /// <param name="messageBody">实体类对象</param>
        public void Publish<T>(string channel,T messageBody)
        {
            string strContent = Newtonsoft.Json.JsonConvert.SerializeObject(messageBody);
            PublishMessage(channel, strContent);
        }

        /// <summary>
        /// 生产一个使用Message消息包装的实体对象到发布到对应的频道上
        /// </summary>
        /// <param name="channel">频道名称</param>
        /// <typeparam name="T">自定义实体类</typeparam>
        /// <param name="message">消息对象</param>
        public void Publish<T>(string channel,Message<T> message)
        {
            T content = message.Content;
            string strContent = Newtonsoft.Json.JsonConvert.SerializeObject(content);
            PublishMessage(channel, strContent);
        }
    }
}
