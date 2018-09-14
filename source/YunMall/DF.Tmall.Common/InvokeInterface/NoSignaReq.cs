using DF.Tmall.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DF.Tmall.Common {
    /// <summary>
    /// 接口调用请求报文处理封装类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class NoSignaReq<T>
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public NoSignaReq() { }

        /// <summary>
        /// 有参构造函数
        /// </summary>
        /// <param name="model">占位实体</param>
        /// <param name="version">版本号</param>
        /// <param name="appName">应用程序名称</param>
        public NoSignaReq(T model, string version, string appName)
        {
            this.data = model;
            this.Version = version;
            this.AppName = appName;
        }
       

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string AppName { get; set; }
        /// <summary>
        /// 发送数据
        /// </summary>
        public T data { get; set; }
    }
}
