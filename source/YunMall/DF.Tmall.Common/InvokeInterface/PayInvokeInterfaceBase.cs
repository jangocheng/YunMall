using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS.Shop.Common.InvokeInterface
{
    public interface PayInvokeInterfaceBase
    {
        /// <summary>
        /// 调用支付模块
        /// </summary>
        /// <typeparam name="TPara">输入参数</typeparam>
        /// <typeparam name="TResult">输出参数</typeparam>
        /// <param name="model">实体类</param>
        /// <param name="url">调用地址</param>
        /// <returns>输出参数</returns>
          Task<TResult> InvokeAsync<TPara, TResult>(TPara model, string url);


        /// <summary>
        /// 调用支付模块
        /// </summary>
        /// <typeparam name="TPara">输入参数</typeparam>
        /// <typeparam name="TResult">输出参数</typeparam>
        /// <param name="model">实体类</param>
        /// <param name="url">调用地址</param>
        /// <returns>输出参数</returns>
         Task<TResult> InvokeAsync1<TPara, TResult>(TPara model, string url) where TResult : HS.PayModel.Base.MyBaseResp;


        /// <summary>
        /// 调用支付模块
        /// </summary>
        /// <typeparam name="TPara">输入参数</typeparam>
        /// <typeparam name="TResult">输出参数</typeparam>
        /// <param name="model">实体类</param>
        /// <param name="url">调用地址</param>
        /// <returns>输出参数</returns>
        Task<TResult> InvokeAsync2<TPara, TResult>(TPara model, string url)
            where TPara : HS.PayModel.Base.MyBaseReq
            where TResult : HS.PayModel.Base.MyBaseResp;
       
    }
}
