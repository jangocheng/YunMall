using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DF.Shop.Common.InvokeInterface
{
    public class AppResp<T> where T :class
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="model">占位模型</param>
        private AppResp(BaseModel<T> model)
        {
            model.Isdata = IsData(model);
            this.data = model;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="model">占位模型</param>
        /// <param name="respCode">响应吗</param>
        /// <param name="respMsg">相应信息</param>
        private AppResp(BaseModel<T> model, string respCode, string respMsg)
        {
            model.Isdata = IsData(model);
            this.data = model;
            this.RespCode = respCode;
            this.RespMsg = respMsg;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="model">占位模型</param>
        /// <param name="respCode">响应吗</param>
        /// <param name="respMsg">相应信息</param>
        /// <param name="errorMsg">错误信息</param>
        private AppResp(BaseModel<T> model, string respCode, string respMsg,string errorMsg)
        {
            model.Isdata = IsData(model);
            this.data = model;
            this.RespCode = respCode;
            this.RespMsg = respMsg;
            this.ErrorMsg = errorMsg;
        }

        ///响应吗       
        public string RespCode { get; set; } = "success";

        ///响应信息 响应码的说明     
        public string RespMsg { get; set; } = "成功";
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg { get; set; }

        /// <summary>
        /// 发送数据
        /// </summary>
        public BaseModel<T> data { get; set; }

        /// <summary>
        /// 判断是否data中是否有数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool IsData(BaseModel<T> model)
        {
            return model.Model != null ||
                  (model.List != null &&
                   model.List.Count != 0) ||
                   !string.IsNullOrEmpty(model.Result);
        }

        #region AppResp初始化
        /// <summary>
        /// AppResp初始化
        /// </summary>
        /// <param name="baseModel"></param>
        /// <returns></returns>
        public static AppResp<T> InitiAppResp(BaseModel<T> baseModel)
        {
            return new AppResp<T>(baseModel);
        }
        /// <summary>
        /// AppResp初始化
        /// </summary>
        /// <param name="baseModel"></param>
        /// <param name="respCode"></param>
        /// <param name="respMsg"></param>
        /// <returns></returns>
        public static AppResp<T> InitiAppResp(BaseModel<T> baseModel, string respCode, string respMsg)
        {
            return new AppResp<T>(baseModel, respCode, respMsg);
        }
        /// <summary>
        /// AppResp初始化
        /// </summary>
        /// <param name="baseModel"></param>
        /// <param name="respCode"></param>
        /// <param name="respMsg"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static AppResp<T> InitiAppResp(BaseModel<T> baseModel, string respCode, string respMsg,string errorMsg)
        {
            return new AppResp<T>(baseModel, respCode, respMsg,errorMsg);
        }
        #endregion
    }
}
