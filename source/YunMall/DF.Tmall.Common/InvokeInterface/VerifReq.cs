using System;
using System.ComponentModel.DataAnnotations;
using DF.Common;
using DF.Tmall.Common.Attributes;
using Newtonsoft.Json;

namespace DF.Tmall.Common.InvokeInterface
{
    /// <summary>
    /// 接口调用请求报文处理封装类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class VerifReq<T>
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public VerifReq() { }

        /// <summary>
        /// 有参构造函数
        /// </summary>
        /// <param name="model">占位实体</param>
        /// <param name="version">版本号</param>
        /// <param name="appName">应用程序名称</param>
        public VerifReq(T model, string version, string appName)
        {
            this.data = model;
            this.Version = version;
            this.AppName = appName;
            this.Signa = this.MySigna;
        }
        /// <summary>
        /// 验签结果
        /// </summary>

        [MustBeTrue(ErrorMessage = "验签失败！")]
        public bool IsChecked { get { return this.Signa.Equals(this.MySigna); } }
        //public bool IsChecked { get { return true; } }

        //public bool IsChecked { get { return this.Signa.Equals(this.MySigna); } }
        //public bool IsChecked { get { return true; } }
        /// <summary>
        /// 版本号
        /// </summary>
        [Required(ErrorMessage = "版本不能为空！")]
        [Version("版本错误！")]
        public string Version { get; set; }

        /// <summary>
        /// 应用程序名称
        /// </summary>
        [Required(ErrorMessage = "应用程序名称不能为空")]
        [AppName(ErrorMessage = "该应用程序无权限进行此操作！")]
        public string AppName { get; set; }
        /// <summary>
        /// 发送数据
        /// </summary>
        public T data { get; set; }

        /// <summary>
        /// 发送签名
        /// </summary>
        public string Signa { get; set; }

        /// <summary>
        /// 验证签名
        /// </summary>
        private string MySigna
        {
            get
            {
                string a = JsonConvert.SerializeObject(this.data);
                return (this.Version + this.AppName + JsonConvert.SerializeObject(this.data)).MD5();
            }
        }
    }
}
