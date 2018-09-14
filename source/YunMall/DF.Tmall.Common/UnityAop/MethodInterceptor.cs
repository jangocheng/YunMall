// ========================================描述信息========================================
// 
// aop方法核心拦截器（Unity）
// 
// 
// ========================================创建信息========================================
// 创建人：   --
// 创建时间： 2016-7-27 13:55:55    	
// ========================================变更信息========================================
// 日期          版本        修改人         描述
// 
// ========================================================================================
using  Microsoft.Practices.Unity.InterceptionExtension;
using  System;
using  System.Collections.Generic;
using  System.Linq;
using  System.Text;
using  System.Threading.Tasks;

namespace DF.Tmall.Common.UnityAop
{
    internal class MethodInterceptor : ICallHandler
    {
        #region 初始值设定
        private MethodAttribute model { get; set; }
        public int Order { get; set; }
        public MethodInterceptor(int Order, MethodAttribute model)
        {
            this.Order = Order;
            this.model = model;
        }
        #endregion

        #region aop处理--
        /// <summary>
        /// aop处理
        /// </summary>
        /// <param name="input"></param>
        /// <param name="getNext"></param>
        /// <returns></returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            //执行前方法
            model.OnMethodExcuting(new MethodExcuteContext(input, null, null));
            //执行方法
            var result = getNext()(input, getNext);
            if (result.Exception != null)
            {     //执行异常方法
                model.OnException(new ExceptionContext(input, result));           
            }
            //执行后方法
            model.OnMethodExcuted(new MethodExcuteContext(input, result, null));
            return result;
        }
        #endregion
    }
}
