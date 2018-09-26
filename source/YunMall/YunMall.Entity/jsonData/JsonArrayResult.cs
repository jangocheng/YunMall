using System;
using System.Collections;
using System.Collections.Generic;

namespace YunMall.Entity.json {
    public class JsonArrayResult <T> {
        public int code { get; set; }
        public int count { get; set; }
        public IList<T> data { get; set; }

        public JsonArrayResult(IList<T> data) {
            this.code = 0;
            if (data == null || data.Count == 0)
            {
                this.data = new List<T>();
            }
            else {
                this.count = data.Count;
                this.data = data;
            }
        }

        public JsonArrayResult(int code, IList<T> data)
        {
            this.code = code;
            this.data = data;
        }

           
        public static JsonResult<T> Failing()
        {
            return new JsonResult<T>(1, "error");
        }
    }
}