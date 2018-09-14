using DF.Tmall.Common.UnityAop;
using Newtonsoft.Json;
using System;
using System.Text;

namespace DF.Tmall.Common
{
    /// <summary>
    /// 异常处理过滤器
    /// </summary>
    public class MyException : MethodAttribute
    {
        /// <summary>
        /// 自定义异常信息
        /// </summary>
        private string message;

        /// <summary>
        /// 自定义异常记录日志的级别
        /// </summary>
        public LogGrade LogGrade { get; set; }
        /// <summary>
        /// 日志记录码
        /// </summary>
        public string LogCode { get; set; }

        /// <summary>
        /// 是否处理掉异常不继续抛出
        /// </summary>
        public bool IsHandleException { get; set; } = false;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">自定义异常信息</param>
        /// <param name="isshowException">是否抛出异常信息</param>
        public MyException(string message, bool isshowException = true)
        {
            this.message = message;
        }
        #endregion


        #region 异常处理方法
        /// <summary>
        /// 异常处理方法
        /// </summary>
        /// <param name="exceptionContext"></param>
        public override void OnException(ExceptionContext exceptionContext)
        {
            //记录异常日志
            LogException();
            //处理异常！
            if (!IsHandleException)
            {
                exceptionContext.Result = null;
                return;
            }
            if (string.IsNullOrEmpty(message))
            {
                throw exceptionContext.Result.Exception;
            }
            throw new Exception(message + exceptionContext.Result.Exception);
        }
        #endregion

        #region 记录异常日志
        /// <summary>
        /// //记录异常日志
        /// </summary>
        private void LogException()
        {
            switch (LogGrade)
            {
                case LogGrade.Debug:
                    //记录Debug异常
                    break;
                case LogGrade.Error:
                    //记录Error异常
                    break;
                case LogGrade.Info:
                    //记录Info异常
                    break;
                case LogGrade.Warn:
                    //记录Warn异常
                    break;
                case LogGrade.Fatal:
                    //记录Fatal异常
                    break;
                default:
                    break;
            }
        }
        #endregion
    }

    #region 异常过滤器记录日志的等级
    /// <summary>
    /// 异常过滤器记录日志的等级
    /// </summary>
    public enum LogGrade
    {
        /// <summary>
        /// 不记录日志
        /// </summary>
        None = 0,
        /// <summary>
        /// Debug日志
        /// </summary>
        Debug = 1,
        /// <summary>
        /// Error日志
        /// </summary>
        Error = 2,
        /// <summary>
        /// Info日志
        /// </summary>
        Info = 3,
        /// <summary>
        /// 警告日志
        /// </summary>
        Warn = 4,
        /// <summary>
        /// 致命性错误日志
        /// </summary>
        Fatal = 5,
    }
    #endregion
}
