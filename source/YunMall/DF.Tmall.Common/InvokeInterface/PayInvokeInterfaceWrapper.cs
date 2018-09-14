using HS.Common;
using HS.Log;
using HS.PayModel;
using System.Threading.Tasks;
using DF.Common;
using DF.Log;
using DF.Tmall.Common;

namespace HS.Shop.Common.InvokeInterface
{
   public  class PayInvokeInterfaceWrapper: PayInvokeInterfaceBase
    {
        /// <summary>
        /// 调用支付模块
        /// </summary>
        /// <typeparam name="TPara">输入参数</typeparam>
        /// <typeparam name="TResult">输出参数</typeparam>
        /// <param name="model">实体类</param>
        /// <param name="url">调用地址</param>
        /// <returns>输出参数</returns>
        public async  Task<TResult> InvokeAsync<TPara, TResult>(TPara model, string url)
        {
            //封装实体类
            BaseReqModel<TPara> Model = new BaseReqModel<TPara>()
            {
                AppName = ConstBaseData.AppName,
                data = model,
                TranIP = "127.0.0.1",
                Version = "2.3"
            };

            //获取签名
            Model.Signa = string.Format("{0}appPwd=[{1}]", Model.ToString(), ConstBaseData.AppPwd).MD5();

            MyLog.Info("InvokeAsync.req", Model);

            //调用接口
            BaseRespModel<TResult> result = await HttpHelper.PostJsonAsync<BaseReqModel<TPara>, BaseRespModel<TResult>>(url, Model);

            //解签
            if (result.Signa == string.Format("{0}appPwd=[{1}]", result.ToString(), ConstBaseData.AppPwd).MD5())
            {
                return result.data;
            }
            else
            {
                return default(TResult);
            }
        }

        /// <summary>
        /// 调用支付模块
        /// </summary>
        /// <typeparam name="TPara">输入参数</typeparam>
        /// <typeparam name="TResult">输出参数</typeparam>
        /// <param name="model">实体类</param>
        /// <param name="url">调用地址</param>
        /// <returns>输出参数</returns>
        public async  Task<TResult> InvokeAsync1<TPara, TResult>(TPara model, string url) where TResult : HS.PayModel.Base.MyBaseResp
        {
            //封装实体类
            BaseReqModel<TPara> Model = new BaseReqModel<TPara>()
            {
                AppName = ConstBaseData.AppName,
                data = model,
                TranIP = "127.0.0.1",
                Version = "2.3"
            };

            //获取签名
            Model.Signa = string.Format("{0}appPwd=[{1}]", Model.ToString(), ConstBaseData.AppPwd).MD5();

            //调用接口
            TResult result = await HttpHelper.PostJsonAsync<BaseReqModel<TPara>, TResult>(url, Model);

            //解签
            if (result.Signa == string.Format("{0}appPwd=[{1}]", result.ToString(), ConstBaseData.AppPwd).MD5())
            {
                return result;
            }
            else
            {
                return default(TResult);
            }
        }

        /// <summary>
        /// 调用支付模块
        /// </summary>
        /// <typeparam name="TPara">输入参数</typeparam>
        /// <typeparam name="TResult">输出参数</typeparam>
        /// <param name="model">实体类</param>
        /// <param name="url">调用地址</param>
        /// <returns>输出参数</returns>
        public async  Task<TResult> InvokeAsync2<TPara, TResult>(TPara model, string url)
            where TPara : HS.PayModel.Base.MyBaseReq
            where TResult : HS.PayModel.Base.MyBaseResp
        {

            model.AppName = ConstBaseData.AppName;
            model.TranIP = "127.0.0.1";
            model.Version = "2.3";

            //获取签名
            model.Signa = string.Format("{0}appPwd=[{1}]", model.ToString(), ConstBaseData.AppPwd).MD5();

            //调用接口
            TResult result = await HttpHelper.PostJsonAsync<TPara, TResult>(url, model);

            //解签
            if (result.Signa == string.Format("{0}appPwd=[{1}]", result.ToString(), ConstBaseData.AppPwd).MD5())
            {
                return result;
            }
            else
            {
                return default(TResult);
            }
        }
    }
}
