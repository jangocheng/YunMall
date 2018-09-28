using System;

namespace YunMall.Web.Exceptions {
    [Serializable]
    public class MsgException : ApplicationException
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MsgException() { }

        public MsgException(string message)
            : base(message)
        { }

        public MsgException(string message, Exception inner)
            : base(message, inner)
        { }

    }
}