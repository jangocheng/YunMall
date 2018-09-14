// ========================================描述信息========================================
// 
// 异常处理上下文类类
// 
// 
// ========================================创建信息========================================
// 创建人：   --
// 创建时间： 2017-02-08 13:55:55    	
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
    /// 异常上下文类
    /// </summary>
    public class ExceptionContext
    {
        /// <summary>
        /// 异常Input
        /// </summary>
        public IMethodInvocation Input { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public IMethodReturn Result { get; set; }

        /// <summary>
        /// 异常上下文类构造函数
        /// </summary>
        /// <param name="input"></param>
        /// <param name="result">异常信息</param>
        public ExceptionContext(IMethodInvocation input, IMethodReturn result)
        {
            this.Input = input;
            this.Result = result;
        }
    }
}
