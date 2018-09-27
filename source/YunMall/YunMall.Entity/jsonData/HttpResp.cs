using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YunMall.Utility.jsonData;

namespace YunMall.Entity.json
{
    public class HttpResp
    {
        public int Code { get; set; }

        public string Msg { get; set; }

        public HttpResp(int code, string msg)
        {
            Code = code;
            Msg = msg ?? throw new ArgumentNullException(nameof(msg));
        }

        public HttpResp(string msg)
        {
            Code = 0;
            Msg = msg ?? throw new ArgumentNullException(nameof(msg));
        }

        public HttpResp(object msg)
        {
            Code = 0;
            Msg = JsonUtil.ToJson(msg);
        }
    }
}
