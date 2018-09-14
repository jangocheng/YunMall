using  System;
using  System.Collections.Generic;
using  System.Linq;
using  System.Text;
using  System.Threading.Tasks;

namespace  DF.Redis.Cache
{
    /// <summary>
    /// Redis消息队列
    /// </summary>
    public class RedisMessageQueue
    {
        /// <summary>
        /// 消息队列名称
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        /// 初始化消息队列
        /// </summary>
        /// <param name="queuename"></param>
        public RedisMessageQueue(string queuename)
        {
            this.QueueName = queuename;
        }


        /// <summary>
        /// 发送一个消息到消息队列中,与Receive配对使用
        /// </summary>
        /// <param name="message">消息实体</param>
        /// <returns></returns>
        public void Send<T>(Message<T> message)
        {
            T content = message.Content;
            string strContent = Newtonsoft.Json.JsonConvert.SerializeObject(content);
            RedisList.LPush(QueueName, strContent);
        }


        /// <summary>
        /// 从消息队列中获取一个消息,与Send配对使用
        /// </summary>
        /// <returns>返回一个消息实体</returns>
        public Message<T> Receive<T>()
        {
            Message<T> msgObject = new Message<T>();
            // T content=default(T);
            try
            {
                string msg = RedisList.BlockingPopItemFromList(QueueName, (new TimeSpan(0, 0, 30)));
                msgObject.Content = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return msgObject;
        }

        /// <summary>
        /// 发送一个消息到消息队列中,与ReceiveT配对使用
        /// T为自定义消息实体
        /// </summary>
        /// <param name="message">自定义消息实体</param>
        /// <returns></returns>
        public void SendT<T>(T message)
        {
            string strContent = Newtonsoft.Json.JsonConvert.SerializeObject(message);
            RedisList.LPush(QueueName, strContent);
        }


        /// <summary>
        /// 从消息队列中获取一个消息,与SendT配对使用
        /// T为自定义的消息实体
        /// </summary>
        /// <returns>返回一个消息实体</returns>
        public T ReceiveT<T>()
        {
            T message = default(T);
            try
            {
                string msg = RedisList.BlockingPopItemFromList(QueueName, (new TimeSpan(0, 0, 30)));
                message = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return message;
        }

        /// <summary>
        /// 从消息队列中获取一个消息,与SendT配对使用yfx修改版
        /// T为自定义的消息实体
        /// </summary>
        /// <returns>返回一个消息实体</returns>
        public T ReceiveT1<T>()
        {
            T message = default(T);
            try
            {
                string msg = RedisList.BlockingPopItemFromList(QueueName, (new TimeSpan(0, 0, 30)));
                if (string.IsNullOrEmpty(msg))
                {
                    return message;
                }
                message = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return message;
        }

        /// <summary>
        /// 发送一个字符串消息到消息队列中,与string Receive配对使用
        /// </summary>
        /// <param name="message">字符串消息</param>
        /// <returns></returns>
        public void Send(string message)
        {
            RedisList.LPush(QueueName, message);
        }


        /// <summary>
        /// 从消息队列中获取一个消息,与Send配对使用
        /// </summary>
        /// <returns>返回一个字符串消息</returns>
        public string Receive()
        {
            string message = string.Empty;
            try
            {
                message = RedisList.BlockingPopItemFromList(QueueName, (new TimeSpan(0, 0, 30)));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return message;
        }

    }
}
