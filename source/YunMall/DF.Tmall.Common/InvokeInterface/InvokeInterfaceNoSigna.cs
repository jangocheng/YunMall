// ========================================描述信息========================================
//接口调用(不带验签)
// 
// 
// 
// ========================================创建信息========================================
// 创建人：   --
// 创建时间： 2017-02-04 17:55:55    	
// ========================================变更信息========================================
// 日期          版本        修改人         描述
// 
// ========================================================================================
using DF.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DF.Common;

namespace DF.Tmall.Common {
    /// <summary>
    /// 接口调用(不带验签)
    /// </summary>
    public class InvokeInterfaceNoSigna
    {
        #region POST请求
        #region 项目之间调用接口公共方法(POST方式)--返回R--
        /// <summary>
        /// 调用接口--
        /// </summary>
        /// <typeparam name="R">返回类型</typeparam>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="model">参数</param>
        /// <param name="url">接口地址</param>
        /// <param name="version">请求接口版本号</param>
        /// <param name="appName">应用程序名称</param>
        /// <returns>R</returns>
        public static R InvokeMethodReq<R, T>(T model, string url, string version, string appName)
        {
            try
            {
                var json = JsonConvert.SerializeObject(model);
                string html = HttpHelper1.Post(json, url);
                Log.MyLog.Info("InvokeInterfaceNoSigna", "InvokeMethodReq", "Html", html);
                if (string.IsNullOrEmpty(html)) {
                    Log.MyLog.Info("InvokeInterfaceNoSigna", "InvokeMethodReq", "HtmlNull", json);
                    return default(R);
                }
                R resp = JsonConvert.DeserializeObject<R>(html);
                return resp;
            }
            catch(Exception e)
            {
                Log.MyLog.Error("InvokeInterfaceNoSigna", "InvokeMethodReq", "Exception", e);
                return default(R);
            }
        }
        #endregion

        #region 项目之间调用接口公共方法带版本(POST方式)--返回R--
        /// <summary>
        /// 调用接口--
        /// </summary>
        /// <typeparam name="R">返回类型</typeparam>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="model">参数</param>
        /// <param name="url">接口地址</param>
        /// <param name="version">请求接口版本号</param>
        /// <param name="appName">应用程序名称</param>
        /// <returns>R</returns>
        public static R InvokeMethodReqParm<R, T>(T model, string url, string version, string appName)
        {
            try
            {
                //签名
                NoSignaReq<T> basemodel = new NoSignaReq<T>(model, version, appName);
                var json = JsonConvert.SerializeObject(basemodel);
                NoSignaResp<R> resp = JsonConvert.DeserializeObject<NoSignaResp<R>>(HttpHelper1.Post(json, url));
                return resp.data;
            }
            catch
            {
                return default(R);
            }
        }
        #endregion

        #region 项目之间调用接口公共方法带版本(POST方式)--返回R--
        /// <summary>
        /// 调用接口--
        /// </summary>
        /// <typeparam name="R">返回类型</typeparam>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="model">参数</param>
        /// <param name="url">接口地址</param>
        /// <param name="version">请求接口版本号</param>
        /// <param name="appName">应用程序名称</param>
        /// <returns>R</returns>
        public static R InvokeMethodReqParm1<R, T>(T model, string url, string version, string appName)
        {
            try
            {
                //签名
                NoSignaReq<T> basemodel = new NoSignaReq<T>(model, version, appName);
                var json = JsonConvert.SerializeObject(basemodel);
                R resp = JsonConvert.DeserializeObject<R>(HttpHelper1.Post(json, url));
                return resp;
            }
            catch
            {
                return default(R);
            }
        }
        #endregion

        #region 项目之间调用接口公共方法Async(POST方式)--返回Task<R>--
        /// <summary>
        /// 调用接口--
        /// </summary>
        /// <typeparam name="R">返回类型</typeparam>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="model">参数</param>
        /// <param name="url">接口地址</param>
        /// <param name="version">请求接口版本号</param>
        /// <param name="appName">应用程序名称</param>
        /// <returns>R</returns>
        public static Task<R> InvokeMethodReqAsync<R, T>(T model, string url, string version, string appName)
        {
            return Task.Run(() =>
            {
                try
                {
                    var json = JsonConvert.SerializeObject(model);
                    R resp = JsonConvert.DeserializeObject<R>(HttpHelper1.Post(json, url));
                    return resp;
                }
                catch
                {
                    return default(R);
                }
            });
        }
        #endregion   


        #region 项目之间调用接口公共方法Async(POST方式)--无返回值--
        /// <summary>
        /// 调用接口--
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="model">参数</param>
        /// <param name="url">接口地址</param>
        /// <param name="version">请求接口版本号</param>
        /// <param name="appName">应用程序名称</param>
        /// <returns>R</returns>
        public static void InvokeMethodReqAsync<T>(T model, string url, string version, string appName)
        {
            Task.Run(() =>
            {
                try
                {
                    var json = JsonConvert.SerializeObject(model);
                    HttpHelper1.Post(json, url);
                    //return resp.IsChecked ? resp.data : default(R);

                }
                catch
                {

                }
            });
        }
        #endregion 
        #endregion

        #region GET请求
        #region 项目之间调用接口公共方法(GET方式)--返回R--
        /// <summary>
        /// 调用接口--
        /// </summary>
        /// <typeparam name="R">返回类型</typeparam>
        /// <param name="url">接口地址</param>
        /// <param name="version">请求接口版本号</param>
        /// <param name="appName">应用程序名称</param>
        /// <returns>R</returns>
        public static R InvokeMethodGetReq<R>(string url, string version, string appName)
        {
            try
            {
                R resp = JsonConvert.DeserializeObject<R>(HttpHelper1.Get(url));
                return resp;
            }
            catch
            {
                return default(R);
            }
        }
        #endregion


        
        #region 项目之间调用接口公共方法(GET方式,未捕获异常)
        /// <summary>
        /// 不要捕获异常,因为有异常过滤器
        /// </summary>
        /// <typeparam name="R">返回类型</typeparam>
        /// <param name="url">接口地址</param>
        /// <returns>R</returns>
        public static R InvokeMethodGetReq<R>(string url)
        {
            R resp = JsonConvert.DeserializeObject<R>(HttpHelper1.GetString(url));
            return resp;
        }
        #endregion

        #region 项目之间调用接口公共方法(GET方式)--返回Task<R>--
        /// <summary>
        /// 调用接口--
        /// </summary>
        /// <typeparam name="R">返回类型</typeparam>
        /// <param name="url">接口地址</param>
        /// <param name="version">请求接口版本号</param>
        /// <param name="appName">应用程序名称</param>
        /// <returns>R</returns>
        public static Task<R> InvokeMethodGetReqAsync<R>(string url, string version, string appName)
        {
            return Task.Run(() =>
            {
                try
                {
                    R resp = JsonConvert.DeserializeObject<R>(HttpHelper1.Get(url));
                    return resp;
                }
                catch
                {
                    return default(R);
                }
            });
        }
        #endregion

        #region 项目之间调用接口公共方法带参数(GET方式)--返回R--
        /// <summary>
        /// 项目之间调用接口公共方法带参数(GET方式)--返回R--
        /// </summary>
        /// <typeparam name="R">返回类型</typeparam>
        /// <typeparam name="Tkey">参数名</typeparam>
        /// <typeparam name="TValue">参数值</typeparam>
        /// <param name="parm">参数键值</param>
        /// <param name="url">接口地址</param>
        /// <param name="version">请求接口版本号</param>
        /// <param name="appName">应用程序名称</param>
        /// <returns>R</returns>
        public static R InvokeMethodGetReqParm<R, Tkey, TValue>(Dictionary<Tkey, TValue> parm, string url, string version, string appName)
        {
            try
            {
                url += string.Format("?Version={0}&AppName={1}", version, appName);
                foreach (var model in parm)
                {
                    url += string.Format("&{0}={1}", model.Key, model.Value);
                }

                R resp = JsonConvert.DeserializeObject<R>(HttpHelper1.Get(url));
                return resp;
            }
            catch
            {
                return default(R);
            }
        }


        /// <summary>
        /// 项目之间调用接口公共方法带参数(GET方式)--返回R--
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <typeparam name="Tkey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="parm"></param>
        /// <param name="url"></param>
        /// <param name="version"></param>
        /// <param name="appName"></param>
        /// <returns></returns>
        public static R InvokeMethodGetReqParm<R, Tkey, TValue>(IDictionary<Tkey, TValue> parm, string url, string version, string appName)
        {
            try
            {
                url += string.Format("?Version={0}&AppName={1}", version, appName);
                foreach (var model in parm)
                {
                    url += string.Format("&{0}={1}", model.Key, model.Value);
                }

                R resp = JsonConvert.DeserializeObject<R>(HttpHelper1.Get(url));
                return resp;
            }
            catch
            {
                return default(R);
            }
        }
        #endregion 
        #endregion


    }
}
