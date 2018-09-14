using  System;
using  System.Collections.Generic;
using  System.Linq;
using  System.Text;
using  System.Threading.Tasks;

namespace  DF.Redis.Cache
{
    /// <summary>
    /// 消息类
    /// </summary>
    /// <typeparam name="T">消息体中封装的内容</typeparam>
    public class Message<T>
    {
        public Message()
        { }

        /// <summary>
        /// 消息的优先级
        /// </summary>
        public int Priority { get; set; }


        /// <summary>
        /// 消息内容
        /// </summary>
        public T Content { get; set; }

        /// <summary>
        /// 消息的有效时间
        /// </summary>

        public TimeSpan Expire { get; set; }


        /// <summary>
        /// 消息的建立时间
        /// </summary>

        public DateTime CreateTime { get; set; }


    }
}
