using System;
using System.IO;
using System.Net;
using System.Text;

namespace DF.Common {
    public class HttpHelper1
    {
        public static String Post(string Msg, string Url)
        {
            try
            {
                //将报文发送到银行   
                byte[] data = Encoding.GetEncoding("UTF-8").GetBytes(Msg);
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(Url);

                myRequest.Method = "POST";
                myRequest.ContentType = "application/json";
                myRequest.ContentLength = data.Length;
                using (Stream newStream = myRequest.GetRequestStream())
                {
                    // Send the data.
                    newStream.Write(data, 0, data.Length);
                    // Get response
                    HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                    using (StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.GetEncoding("UTF-8")))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch(Exception ex) {
                DF.Log.MyLog.Info("PostException", "InvokeException", "response",ex);
                DF.Log.MyLog.Info("PostException", "InvokeException", "url", Url);
                DF.Log.MyLog.Info("PostException", "InvokeException", "msg", Msg);
                return ""; }
        }
        public static String PostX(string Msg, string Url)
        {
            try
            {
                //将报文发送到银行   
                byte[] data = Encoding.GetEncoding("UTF-8").GetBytes(Msg);

                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(Url);
                myRequest.Method = "POST";
                myRequest.ContentType = "application/x-www-form-urlencoded";

                myRequest.ContentLength = data.Length;
                Stream newStream = myRequest.GetRequestStream();

                newStream.Write(data, 0, data.Length);
                newStream.Close();

                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
                string content = reader.ReadToEnd();

                //返回处理信息
                return content;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        /// <summary>
        /// Get请求，参数写到url里
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static String Get(string Url)
        {
            try
            {
                string result = "";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(Url);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();
                try
                {
                    //获取内容  
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        result = reader.ReadToEnd();
                    }
                }
                finally
                {
                    stream.Close();
                }
                return result;
            }
            catch (Exception ex) { return ""; }
        }


        /// <summary>
        /// Get请求，不要捕获异常,因为有异常过滤器
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static String GetString(string Url)
        {
           
                string result = string.Empty;
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(Url);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();
                try
                {
                    //获取内容  
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        result = reader.ReadToEnd();
                    }
                }
                finally
                {
                    stream.Close();
                }
                return result;
          
        }
    }
}
