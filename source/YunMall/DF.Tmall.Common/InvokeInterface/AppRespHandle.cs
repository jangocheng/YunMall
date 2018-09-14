using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DF.Shop.Common.InvokeInterface
{
    /// <summary>
    /// 移动接口响应处理  支持链式编程
    /// </summary>
    public static class AppRespHandle
    {
        /// <summary>
        /// Model初始化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static BaseModel<T> InitiBaseModel<T>(this T model) where T : class
        {
            return BaseModel<T>.NoSet();
        }

        /// <summary>
        /// Model初始化赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static BaseModel<T> InitiBaseModelModel<T>(this T model) where T : class
        {
            return BaseModel<T>.SetModel(model);
        }
        /// <summary>
        /// List初始化赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static BaseModel<T> InitiBaseModelList<T> (this IList<T> list) where T : class
        {
            return BaseModel<T>.SetList(list);
        }
        /// <summary>
        /// Result初始化赋值
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static BaseModel<string> InitiBaseModelResult(this string result) 
        {
            return BaseModel<string>.SetResult(result); 
        }

        /// <summary>
        /// AppResp初始化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseModel"></param>
        /// <returns></returns>
        public static AppResp<T> InitiAppResp<T>(this BaseModel<T> baseModel) where T : class
        {
            return AppResp<T>.InitiAppResp(baseModel);
        }


        /// <summary>
        /// AppResp初始化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseModel"></param>
        /// <returns></returns>
        public static AppResp<T> InitiAppResp<T>(this BaseModel<T> baseModel, string respCode, string respMsg) where T : class
        {

            return AppResp<T>.InitiAppResp(baseModel, respCode, respMsg);
        }

        /// <summary>
        /// AppResp初始化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseModel"></param>
        /// <param name="respCode"></param>
        /// <param name="respMsg"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static AppResp<T> InitiAppResp<T>(this BaseModel<T> baseModel, string respCode, string respMsg,string errorMsg) where T : class
        {
            return AppResp<T>.InitiAppResp(baseModel, respCode, respMsg, errorMsg);
        }

        /// <summary>
        /// RespCode赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="appResp"></param>
        /// <param name="respCode"></param>
        /// <returns></returns>
        public static AppResp<T> SetRespCode<T>(this AppResp<T> appResp,string respCode) where T : class
        {
            appResp.RespCode = respCode;
            return appResp;
        }

        /// <summary>
        /// RespMsg赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="appResp"></param>
        /// <param name="respMsg"></param>
        /// <returns></returns>
        public static AppResp<T> SetRespMsg<T>(this AppResp<T> appResp, string respMsg) where T : class
        {
            appResp.RespMsg = respMsg;
            return appResp;
        }

        /// <summary>
        /// ErrorMsg赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="appResp"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static AppResp<T> SetErrorMsg<T>(this AppResp<T> appResp, string errorMsg) where T : class
        {
            appResp.ErrorMsg = errorMsg;
            return appResp;
        }


    }
}
