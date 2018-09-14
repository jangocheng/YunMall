// ========================================描述信息========================================
// 
// 方法拦截器上下文类
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
    /// <summary>
    /// 方法执行前后拦截器上下文类
    /// </summary>
    public class MethodExcuteContext
    {
        /// <summary>
        /// Input
        /// </summary>
        public IMethodInvocation Input { get; set; }

        /// <summary>
        /// 方法结果
        /// </summary>
        public IMethodReturn Result { get; set; }

        /// <summary>
        /// 方法异常信息
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// 方法执行前后拦截器上下文类构造函数
        /// </summary>
        /// <param name="input"></param>
        /// <param name="result"></param>
        /// <param name="exception"></param>
        public MethodExcuteContext(IMethodInvocation input, IMethodReturn result, Exception exception)
        {
            this.Input = input;
            this.Result = result;
            this.Exception = exception;
        }


    }
}
