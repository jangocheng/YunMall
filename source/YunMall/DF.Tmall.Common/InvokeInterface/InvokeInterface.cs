// ========================================描述信息========================================
//接口调用
// 
// 
// 
// ========================================创建信息========================================
// 创建人：   
// 创建时间： 2017-02-04 17:55:55    	
// ========================================变更信息========================================
// 日期          版本        修改人         描述
// 
// ========================================================================================

using System;
using System.Threading.Tasks;
using DF.Common;
using Newtonsoft.Json;

namespace DF.Tmall.Common.InvokeInterface
{
    /// <summary>
    /// 接口调用公共Helper类
    /// </summary>
    public class InvokeInterface
    {

        #region 项目之间调用接口公共方法--返回R
        /// <summary>
        /// 调用接口
        /// </summary>
        /// <typeparam name="R">返回类型</typeparam>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="model">参数</param>
        /// <param name="url">接口地址</param>
        /// <param name="version">请求接口版本号</param>
        /// <param name="appName">应用程序名称</param>
        /// <returns>R</returns>\
        public static R InvokeMethodReq<R, T>(T model, string url, string version, string appName)
        {
            try
            {
                //签名
                VerifReq<T> basemodel = new VerifReq<T>(model, version, appName);
                var json = JsonConvert.SerializeObject(basemodel);
                DF.Log.MyLog.Info("接口调用json数据", "InvokeInterface", "Request", json);

                VerifResp<R> resp = JsonConvert.DeserializeObject<VerifResp<R>>(HttpHelper1.Post(json, url));
                DF.Log.MyLog.Info("接口响应结果", "InvokeInterface", "Response", json);
                if (resp == null || !resp.IsChecked)
                    return default(R);
                return resp.data;
            }
            catch (Exception ex)
            {
                DF.Log.MyLog.Error("接口响应结果", "InvokeInterface", "Error", ex);
                return default(R);
            }
        }
        #endregion

        #region 项目之间调用接口公共方法--返回R
        /// <summary>
        /// 调用接口
        /// </summary>
        /// <typeparam name="R">返回类型</typeparam>
        /// <param name="data">参数</param>
        /// <param name="url">接口地址</param>
        /// <returns>R</returns>
        public static VerifResp<R> InvokeMethodReq<R>(string data, string url)
        {
            try
            {
                //签名
                VerifResp<R> resp = JsonConvert.DeserializeObject<VerifResp<R>>(HttpHelper1.Post(data, url));
                return resp;
            }
            catch (Exception ex)
            {
                return new VerifResp<R>(default(R), "ERROR", ex.ToString());
            }
        }
        #endregion

        #region 项目之间调用接口公共方法--返回VerifResp<R>
        /// <summary>
        /// 调用接口
        /// </summary>
        /// <typeparam name="R">返回类型</typeparam>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="model">参数</param>
        /// <param name="url">接口地址</param>
        /// <param name="version">请求接口版本号</param>
        /// <param name="appName">应用程序名称</param>
        /// <returns>R</returns>
        public static VerifResp<R> InvokeMethod<R, T>(T model, string url, string version, string appName)
        {
            try
            {
                //签名 InvokeInterface
                VerifReq<T> basemodel = new VerifReq<T>(model, version, appName);
                var json = JsonConvert.SerializeObject(basemodel);
                VerifResp<R> resp = JsonConvert.DeserializeObject<VerifResp<R>>(HttpHelper1.Post(json, url));
                if (resp.IsChecked)
                    return resp;

                return new VerifResp<R>(default(R), "EEEEEE", "验签失败！");
            }
            catch (Exception ex)
            {
                DF.Log.MyLog.Error("接口响应结果", "InvokeInterface", "InvokeMethod", ex);
                throw ex;
            }
        }
        #endregion

        #region 项目之间调用接口公共方法--返回VerifResp<R>并输出请求的Json
        /// <summary>
        /// 调用接口
        /// </summary>
        /// <typeparam name="R">返回类型</typeparam>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="model">参数</param>
        /// <param name="url">接口地址</param>
        /// <param name="version">请求接口版本号</param>
        /// <param name="appName">应用程序名称</param>
        /// <param name="json">请求的json</param>
        /// <returns>R</returns>
        public static VerifResp<R> InvokeMethod<R, T>(T model, string url, string version, string appName,out string json)
        {
            try
            {
                //签名
                VerifReq<T> basemodel = new VerifReq<T>(model, version, appName);
                json = JsonConvert.SerializeObject(basemodel);
                DF.Log.MyLog.Info("InvoleInterface", "InvoleInterface", "VerifReq", json);
                VerifResp<R> resp = JsonConvert.DeserializeObject<VerifResp<R>>(HttpHelper1.Post(json, url));
                if (resp.IsChecked)
                    return resp;
                return new VerifResp<R>(default(R), "EEEEEE", "验签失败！");
            }
            catch (Exception ex)
            {
                DF.Log.MyLog.Error("InvoleInterface", "InvoleInterface", "InvokeMethod", ex);
                throw ex;
            }
        }
        #endregion

        #region 项目之间调用接口公共方法Async--返回Task<R>
        /// <summary>
        /// 调用接口
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
                    //签名
                    VerifReq<T> basemodel = new VerifReq<T>(model, version, appName);
                    var json = JsonConvert.SerializeObject(basemodel);
                    VerifResp<R> resp = JsonConvert.DeserializeObject<VerifResp<R>>(HttpHelper1.Post(json, url));
                    return resp.IsChecked ? resp.data : default(R);
                }
                catch (Exception ex)
                {
                    DF.Log.MyLog.Error("接口响应结果", "InvokeInterface", "InvokeMethod", ex);
                    return default(R);
                }
            });
        }
        #endregion   


        #region 项目之间调用接口公共方法Async--无返回值
        /// <summary>
        /// 调用接口
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
                   //签名
                   VerifReq<T> basemodel = new VerifReq<T>(model, version, appName);
                   var json = JsonConvert.SerializeObject(basemodel);
                   //VerifResp<R> resp = JsonConvert.DeserializeObject<VerifResp<R>>(HttpHelper1.Post(json, url));
                   //return resp.IsChecked ? resp.data : default(R);
               }
               catch
               {

               }
           });
        }
        #endregion   




    }
}
