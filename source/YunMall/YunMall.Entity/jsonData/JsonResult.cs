using System;
using YunMall.Utility.jsonData;

namespace YunMall.Entity.json {
    public class JsonResult<T> {
        public int Code { get; set; }

        public object Msg { get; set; }

        public JsonResult()
        {
        }

        public JsonResult(int code, object msg)
        {
            Code = code;
            Msg = msg ?? throw new ArgumentNullException(nameof(msg));
        }


        public JsonResult<T> SuccessfulAsString(T msg)
        {
            return new JsonResult<T>(200, JsonUtil.ToJson(msg));
        }

        public JsonResult<T> Successful(T msg)
        {
            return new JsonResult<T>(200, msg);
        }

        public JsonResult<T> ExceptionAsString(T msg)
        {
            return new JsonResult<T>(300, JsonUtil.ToJson(msg));
        }

        public JsonResult<T> NormalExceptionAsString(T msg)
        {
            return new JsonResult<T>(400, JsonUtil.ToJson(msg));
        }

        public JsonResult<T> Exception(T msg)
        {
            return new JsonResult<T>(300, msg);
        }

      
        public static JsonResult<T> Successful()
        {
            return new JsonResult<T>(200, "success");
        }

       
        public JsonResult<T> FailingAsString(T msg)
        {
            return new JsonResult<T>(500, JsonUtil.ToJson(msg));
        }

        public JsonResult<T> Failing(T msg)
        {
            return new JsonResult<T>(500, msg);
        }

        public static JsonResult<T> Failing()
        {
            return new JsonResult<T>(500, "fail");
        }
    }
}