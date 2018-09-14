using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DF.Tmall.Common {
    /// <summary>
    /// 无验签接口调用相应报文处理封装类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class NoSignaResp<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public NoSignaResp() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="model">占位模型</param>
        /// <param name="respCode">响应吗</param>
        /// <param name="respMsg">相应信息</param>
        public NoSignaResp(T model, string respCode, string respMsg)
        {
            this.data = model;
            this.RespCode = respCode;
            this.RespMsg = respMsg;
        }

        ///响应吗       
        public string RespCode { get; set; }

        ///响应信息 响应码的说明     
        public string RespMsg { get; set; }
        /// <summary>
        /// 发送数据
        /// </summary>
        public T data { get; set; }
        
    }
}
