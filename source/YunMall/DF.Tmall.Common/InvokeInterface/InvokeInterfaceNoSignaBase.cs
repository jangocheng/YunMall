using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DF.Tmall.Common.InvokeInterface
{
    public class InvokeInterfaceNoSignaBase
    {
        #region POST请求
        #region 项目之间调用接口公共方法(POST方式)--返回R--杨峰
        /// <summary>
        /// 调用接口--杨峰
        /// </summary>
        /// <typeparam name="R">返回类型</typeparam>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="model">参数</param>
        /// <param name="url">接口地址</param>
        /// <param name="version">请求接口版本号</param>
        /// <param name="appName">应用程序名称</param>
        /// <returns>R</returns>
        public virtual R InvokeMethodReq<R, T>(T model, string url, string version, string appName)
        {
            throw new Exception();
        }
        #endregion

        #region 项目之间调用接口公共方法带版本(POST方式)--返回R--杨峰
        /// <summary>
        /// 调用接口--杨峰
        /// </summary>
        /// <typeparam name="R">返回类型</typeparam>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="model">参数</param>
        /// <param name="url">接口地址</param>
        /// <param name="version">请求接口版本号</param>
        /// <param name="appName">应用程序名称</param>
        /// <returns>R</returns>
        public virtual R InvokeMethodReqParm<R, T>(T model, string url, string version, string appName)
        {
            throw new Exception();
        }
        #endregion

        #region 项目之间调用接口公共方法带版本(POST方式)--返回R--杨峰
        /// <summary>
        /// 调用接口--杨峰
        /// </summary>
        /// <typeparam name="R">返回类型</typeparam>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="model">参数</param>
        /// <param name="url">接口地址</param>
        /// <param name="version">请求接口版本号</param>
        /// <param name="appName">应用程序名称</param>
        /// <returns>R</returns>
        public virtual R InvokeMethodReqParm1<R, T>(T model, string url, string version, string appName)
        {
            throw new Exception();
        }
        #endregion

        #region 项目之间调用接口公共方法Async(POST方式)--返回Task<R>--杨峰
        /// <summary>
        /// 调用接口--杨峰
        /// </summary>
        /// <typeparam name="R">返回类型</typeparam>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="model">参数</param>
        /// <param name="url">接口地址</param>
        /// <param name="version">请求接口版本号</param>
        /// <param name="appName">应用程序名称</param>
        /// <returns>R</returns>
        public virtual Task<R> InvokeMethodReqAsync<R, T>(T model, string url, string version, string appName)
        {
            throw new Exception();
        }
        #endregion   


        #region 项目之间调用接口公共方法Async(POST方式)--无返回值--杨峰
        /// <summary>
        /// 调用接口--杨峰
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="model">参数</param>
        /// <param name="url">接口地址</param>
        /// <param name="version">请求接口版本号</param>
        /// <param name="appName">应用程序名称</param>
        /// <returns>R</returns>
        public virtual void InvokeMethodReqAsync<T>(T model, string url, string version, string appName)
        {
            throw new Exception();
        }
        #endregion 
        #endregion

        #region GET请求
        #region 项目之间调用接口公共方法(GET方式)--返回R--杨峰
        /// <summary>
        /// 调用接口--杨峰
        /// </summary>
        /// <typeparam name="R">返回类型</typeparam>
        /// <param name="url">接口地址</param>
        /// <param name="version">请求接口版本号</param>
        /// <param name="appName">应用程序名称</param>
        /// <returns>R</returns>
        public virtual R InvokeMethodGetReq<R>(string url, string version, string appName)
        {
            throw new Exception();
        }
        #endregion



        #region 项目之间调用接口公共方法(GET方式,未捕获异常)
        /// <summary>
        /// 不要捕获异常,因为有异常过滤器
        /// </summary>
        /// <typeparam name="R">返回类型</typeparam>
        /// <param name="url">接口地址</param>
        /// <returns>R</returns>
        public virtual R InvokeMethodGetReq<R>(string url)
        {
            throw new Exception();
        }
        #endregion

        #region 项目之间调用接口公共方法(GET方式)--返回Task<R>--杨峰
        /// <summary>
        /// 调用接口--杨峰
        /// </summary>
        /// <typeparam name="R">返回类型</typeparam>
        /// <param name="url">接口地址</param>
        /// <param name="version">请求接口版本号</param>
        /// <param name="appName">应用程序名称</param>
        /// <returns>R</returns>
        public virtual Task<R> InvokeMethodGetReqAsync<R>(string url, string version, string appName)
        {
            throw new Exception();
        }
        #endregion

        #region 项目之间调用接口公共方法带参数(GET方式)--返回R--杨峰
        /// <summary>
        /// 项目之间调用接口公共方法带参数(GET方式)--返回R--杨峰
        /// </summary>
        /// <typeparam name="R">返回类型</typeparam>
        /// <typeparam name="Tkey">参数名</typeparam>
        /// <typeparam name="TValue">参数值</typeparam>
        /// <param name="parm">参数键值</param>
        /// <param name="url">接口地址</param>
        /// <param name="version">请求接口版本号</param>
        /// <param name="appName">应用程序名称</param>
        /// <returns>R</returns>
        public virtual R InvokeMethodGetReqParm<R, Tkey, TValue>(Dictionary<Tkey, TValue> parm, string url, string version, string appName)
        {
            throw new Exception();
        }


        /// <summary>
        /// 项目之间调用接口公共方法带参数(GET方式)--返回R--杨峰
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <typeparam name="Tkey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="parm"></param>
        /// <param name="url"></param>
        /// <param name="version"></param>
        /// <param name="appName"></param>
        /// <returns></returns>
        public virtual R InvokeMethodGetReqParm<R, Tkey, TValue>(IDictionary<Tkey, TValue> parm, string url, string version, string appName)
        {
            throw new Exception();
        }
        #endregion



        #region 项目之间调用接口公共方法(GET方式,未捕获异常)
        /// <summary>
        /// 不要捕获异常,因为有异常过滤器
        /// </summary>
        /// <param name="R">返回类型</param>
        /// <param name="url">接口地址</param>
        /// <param name="param">接口地址</param>
        /// <returns>R</returns>
        public virtual R InvokeMethodGetReq<R>(string url, Hashtable param = null)
        {
            throw new Exception();
        }
        #endregion
        #endregion
    }
}
