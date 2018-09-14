using System.Threading.Tasks;
using DF.Shop.Common.InvokeInterface;

namespace DF.Tmall.Common.InvokeInterface
{
    public class InvokeInterfaceWrapper : InvokeInterfaceBase
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
        /// <returns>R</returns>
        public override R InvokeMethodReq<R, T>(T model, string url, string version, string appName)
        {
            return InvokeInterface.InvokeMethodReq<R, T>(model, url, version, appName);
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
        public override VerifResp<R> InvokeMethodReq<R>(string data, string url)
        {
            return InvokeInterface.InvokeMethodReq<R>(data, url);
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
        public override VerifResp<R> InvokeMethod<R, T>(T model, string url, string version, string appName)
        {
            return InvokeInterface.InvokeMethod<R, T>(model, url, version, appName);
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
        public override VerifResp<R> InvokeMethod<R, T>(T model, string url, string version, string appName, out string json)
        {
            return InvokeInterface.InvokeMethod<R, T>(model, url, version, appName, out json);
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
        public override Task<R> InvokeMethodReqAsync<R, T>(T model, string url, string version, string appName)
        {
            return InvokeInterface.InvokeMethodReqAsync<R, T>(model, url, version, appName);
        }
        #endregion   
    }
}
