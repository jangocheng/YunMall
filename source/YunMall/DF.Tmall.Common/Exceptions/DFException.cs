using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DF.Tmall.Common.Exceptions
{
    /// <summary>
    /// 自定义异常类
    /// </summary>
    public  class DFException : Exception
    {
        /// <summary>
        /// 异常状态码
        /// </summary>
        public string code { get; set; }

        public object data { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code">异常状态码</param>
        /// <param name="message">异常信息</param>
        public DFException(string code, string message) : base(message)
        {
            this.code = code;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code">异常状态码</param>
        /// <param name="message">异常信息</param>
        public DFException(string code, string message,object data) : base(message)
        {
            this.code = code;
            this.data = data;            
        }
    }
}
