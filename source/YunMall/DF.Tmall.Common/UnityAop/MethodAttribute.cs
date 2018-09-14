// ========================================描述信息========================================
// 
// aop方法拦截器特性类基类
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
using  Microsoft.Practices.Unity;

namespace DF.Tmall.Common.UnityAop
{
    /// <summary>
    /// 方法拦截器特性类
    /// </summary>
    public class MethodAttribute : HandlerAttribute, IInteceptor
    {
        #region 重写基类方法调用拦截器--
        /// <summary>
        /// 重写基类方法调用拦截器--
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new MethodInterceptor(Order, this);
        }
        #endregion

        #region 方法执行前拦截器--
        /// <summary>
        /// 方法执行前拦截器
        /// </summary>
        /// <param name="methodContext">MothodContext</param>
        public virtual void OnMethodExcuting(MethodExcuteContext methodContext) { }
        #endregion

        #region 方法执行后拦截器--
        /// <summary>
        /// 方法执行后拦截器
        /// </summary>
        /// <param name="methodContext">MothodContext</param>
        public virtual void OnMethodExcuted(MethodExcuteContext methodContext) { }
        #endregion

        #region 异常处理拦截器--
        /// <summary>
        /// 异常处理拦截器--
        /// </summary>
        /// <param name="exceptionContext">异常上下文</param>
        public virtual void OnException(ExceptionContext exceptionContext) { }
       
        #endregion
    }
}
