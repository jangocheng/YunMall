using DF.Common;
using Newtonsoft.Json;

namespace DF.Tmall.Common.InvokeInterface
{
    /// <summary>
    /// 接口调用相应报文处理封装类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class VerifResp<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public VerifResp() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="model">占位模型</param>
        /// <param name="respCode">响应吗</param>
        /// <param name="respMsg">相应信息</param>
        public VerifResp(T model, string respCode, string respMsg)
        {
            this.data = model;
            this.RespCode = respCode;
            this.RespMsg = respMsg;
            this.Signa = this.MySigna;
        }
        /// <summary>
        /// 验签结果
        /// </summary>
        public bool IsChecked { get { return this.Signa.Equals(this.MySigna); } }

        ///响应吗       
        public string RespCode { get; set; }

        ///响应信息 响应码的说明     
        public string RespMsg { get; set; }
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
                return DESEncrypt.MD5(this.RespCode + this.RespMsg + JsonConvert.SerializeObject(this.data));
            }
        }
    }
}
