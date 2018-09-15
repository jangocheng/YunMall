namespace YunMall.Entity.ModelView {
    /// <summary>
    /// HTTP参数类
    /// </summary>
    public class HttpParam {

        /// <summary>
        /// 请求方法
        /// </summary>
        public Method Method { get; set; } = Method.Get;

        /// <summary>
        /// 请求地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 请求主体
        /// </summary>
        public string Body { get; set; }
    }

    public enum Method {
        Post,
        Get
    }
}