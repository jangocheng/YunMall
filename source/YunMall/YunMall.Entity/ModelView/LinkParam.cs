using YunMall.Entity.enums;

namespace YunMall.Entity.ModelView
{
    public class LinkParam {

        /// <summary>
        /// 链路通道
        /// </summary>
        public LinkChannels Channel { get; set; }

        /// <summary>
        /// 链路主体
        /// </summary>
        public string Body { get; set; }
    }
}