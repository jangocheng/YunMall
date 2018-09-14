namespace DF.Shop.Common.InvokeInterface
{
    /// <summary>
    /// APP接口调用响应报文处理封装类
    /// </summary>
    public sealed class AppBaseResp<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public AppBaseResp() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="model">占位模型</param>
        /// <param name="respCode">响应吗</param>
        /// <param name="respMsg">相应信息</param>
        public AppBaseResp(T model)
        {
            this.data = model;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="model">占位模型</param>
        /// <param name="respCode">响应吗</param>
        /// <param name="respMsg">相应信息</param>
        public AppBaseResp(T model, string respCode, string respMsg)
        {
            this.data = model;
            this.RespCode = respCode;
            this.RespMsg = respMsg;
        }

        ///响应吗       
        public string RespCode { get; set; } = "success";

        ///响应信息 响应码的说明     
        public string RespMsg { get; set; } = "成功";

        /// <summary>
        /// 发送数据
        /// </summary>
        public T data { get; set; }
    }
}
