// ========================================描述信息========================================
// 通用ajax返回消息模型
// ========================================创建信息========================================
// 创建人：  DF
// 创建时间： 2017年9月17日16:08:44
// ========================================变更信息========================================
// 日期          版本        修改人         描述
// 
// ========================================================================================
using  System;
using  System.Collections.Generic;
using  System.Linq;
using  System.Text;
using  System.Threading.Tasks;

namespace  DF.Tmall.Common {
    public class MessageResp {
        /// <summary>
        /// 返回状态码
        /// </summary>
        public MessageStatusCode Code { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Msg { get; set; }


        /// <summary>
        /// 无参构造
        /// </summary>
        public MessageResp() {

        }

        /// <summary>
        /// 有参构造
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        public MessageResp(MessageStatusCode code, string msg) {
            this.Code = code;
            this.Msg = msg;
        }

        public MessageResp(MessageStatusCode code) {
            this.Code = code;
            this.Msg = null;
        }
    }

    /// <summary>
    /// 消息状态码
    /// </summary>
    public enum MessageStatusCode {
        SUCCESS,
        FAILD,
        ERROR,
        EXCEPTION,
        TIMEOUT,
        IDENTITY_FAILD
    }
}
