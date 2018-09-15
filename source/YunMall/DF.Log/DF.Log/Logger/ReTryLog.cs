/// <summary>
/// 建立者：王龙
/// 类名：MyLog
/// 功能：日志记录
/// 详细： 
/// 版本：2.0
/// 修改日期：2016-10-9 17:59:06 
/// 说明：
/// </summary>
using System;
using log4net;
using log4net.Core;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HS.Log
{
    /// <summary>
    /// <para>日志记录</para>
    /// <para>日志可以使用 DEBUG、INFO、WARN、ERROR和FATAL记录这五个级别是有顺序的，DEBUG小于INFO小于WARN小于ERROR小于FATAL</para>
    /// <para>1：DEBUG 这个级别最低的东东，一般的来说，在系统实际运行过程中，一般都是不输出的。因此这个级别的信息，可以随意的使用，任何觉得有利于在调试时更详细的了解系统运行状态的东东，比如变量的值等等，都输出来看看也无妨。</para>
    /// <para>2：INFO 这个应该用来反馈系统的当前状态给最终用户的，所以，在这里输出的信息，应该对最终用户具有实际意义，也就是最终用户要能够看得明白是什么意思才行。从某种角度上说，Info 输出的信息可以看作是软件商品的一部分（就像那些交互界面上的文字一样），所以需要谨慎对待，不可随便。</para>
    /// <para>3：WARN、ERROR和FATAL：警告、错误、严重错误，这三者应该都在系统运行时检测到了一个不正常的状态，他们之间的区别，要区分还真不是那么简单的事情。</para>
    /// <para>      所谓警告，应该是这个时候进行一些修复性的工作，应该还可以把系统恢复到正常状态中来，系统应该可以继续运行下去。</para>
    /// <para>      所谓错误，就是说可以进行一些修复性的工作，但无法确定系统会正常的工作下去，系统在以后的某个阶段，很可能会因为当前的这个问题，导致一个无法修复的错误（例如宕机），但也可能一直工作到停止也不出现严重问题。</para>
    /// <para>      所谓Fatal，那就是相当严重的了，可以肯定这种错误已经无法修复，并且如果系统继续运行下去的话，可以肯定必然会越来越乱。这时候采取的最好的措施不是试图将系统状态恢复到正常，而是尽可能地保留系统有效数据并停止运行。</para>
    /// <para>      也就是说，选择 Warn、Error、Fatal 中的具体哪一个，是根据当前的这个问题对以后可能产生的影响而定的，如果对以后基本没什么影响，则警告之，如果肯定是以后要出严重问题的了，则Fatal之，拿不准会怎么样，则 Error 之</para>
    /// </summary>
    public static class ReTryLog
    {
        /// <summary>
        /// 日志记录Logger
        /// </summary>
        static readonly ILog MyLogger = LogManager.GetLogger("ReTryLogger");

        /// <summary>
        /// 多线程记录日志
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="filePath">日志文件地址</param>
        /// <param name="source">日志记录位置</param>
        /// <param name="tranCode">交易码可以是一类日志</param>
        /// <param name="mark">日志标识例如订单类交易</param>
        /// <param name="msg">日志信息</param>
        /// <param name="ex">异常信息</param>
        static void LogAsync(Level level, string source, string msgNick, string abUrl, Object msg)
        {
            string filePath = string.Format("{0}/log.log", DateTime.Now.ToString("yyyyMMdd")), msg1 = string.Empty;

            Task.Run(() =>
            {
                if (msg is String)
                {
                    msg1 = msg as String;
                }
                else
                {
                    msg1 = JsonConvert.SerializeObject(msg);
                }

                using (ThreadContext.Stacks["source"].Push(source))
                {
                    MyLogger.Logger.Log(typeof(MyLog), level, new { FilePath = filePath, MessageNick = msgNick, AbsoluteUrl = abUrl, Message = msg1 }, null);
                }
            });
        }

        /// <summary>
        /// 多线程记录日志
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="filePath">日志文件地址</param>
        /// <param name="source">日志记录位置</param>
        /// <param name="tranCode">交易码可以是一类日志</param>
        /// <param name="mark">日志标识例如订单类交易</param>
        /// <param name="msg">日志信息</param>
        /// <param name="ex">异常信息</param>
        static void LogAsync(Level level, string source, string tranCode, string msgNick, string abUrl, Object msg)
        {
            string filePath = string.Format("{0}/log.log", DateTime.Now.ToString("yyyyMMdd")), msg1 = string.Empty;

            Task.Run(() =>
            {
                if (msg is String)
                {
                    msg1 = msg as String;
                }
                else
                {
                    msg1 = JsonConvert.SerializeObject(msg);
                }

                using (ThreadContext.Stacks["source"].Push(source))
                {
                    MyLogger.Logger.Log(typeof(MyLog), level, new { FilePath = filePath, TranCode = tranCode, MessageNick = msgNick, AbsoluteUrl = abUrl, Message = msg1 }, null);
                }
            });
        }

        /// <summary>
        /// 多线程记录日志
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="filePath">日志文件地址</param>
        /// <param name="source">日志记录位置</param>
        /// <param name="tranCode">交易码可以是一类日志</param>
        /// <param name="mark">日志标识例如订单类交易</param>
        /// <param name="msg">日志信息</param>
        /// <param name="ex">异常信息</param>
        static void LogAsync(Level level, string source, string tranCode, string mark, string msgNick, string abUrl, Object msg)
        {
            string filePath = string.Format("{0}/log.log", DateTime.Now.ToString("yyyyMMdd")), msg1 = string.Empty;

            Task.Run(() =>
            {
                if (msg is String)
                {
                    msg1 = msg as String;
                }
                else
                {
                    msg1 = JsonConvert.SerializeObject(msg);
                }

                using (ThreadContext.Stacks["source"].Push(source))
                {
                    MyLogger.Logger.Log(typeof(MyLog), level, new { FilePath = filePath, TranCode = tranCode, Mark = mark, MessageNick = msgNick, AbsoluteUrl = abUrl, Message = msg1 }, null);
                }
            });
        }

        /// <summary>
        /// Debug级别记录调试日志
        /// </summary>
        /// <param name="source">日志记录位置</param>
        /// <param name="msg">记录详细信息</param>
        public static void Debug(string source, string msgNick, string abUrl, Object msg)
        {
            LogAsync(Level.Debug, source, msgNick, abUrl, msg);
        }

        /// <summary>
        /// Debug级别记录调试日志
        /// </summary>
        /// <param name="source">日志记录位置</param>
        /// <param name="tranCode">交易码可以是一类日志</param>
        /// <param name="msg">记录详细信息</param>
        public static void Debug(string source, string tranCode, string msgNick, string abUrl, Object msg)
        {
            LogAsync(Level.Debug, source, tranCode, msgNick, abUrl, msg);
        }

        /// <summary>
        /// Debug级别记录调试日志
        /// </summary>
        /// <param name="source">日志记录位置</param>
        /// <param name="tranCode">交易码可以是一类日志</param>
        /// <param name="mark">标记</param>
        /// <param name="msg">记录详细信息</param>
        public static void Debug(string source, string tranCode, string mark, string msgNick, string abUrl, Object msg)
        {
            LogAsync(Level.Debug, source, tranCode, mark, msgNick, abUrl, msg);
        }

        /// <summary>
        /// Info级别记录对用户有实际意义的日志
        /// </summary>
        /// <param name="source">日志记录位置</param>
        /// <param name="msg">记录详细信息</param>
        public static void Info(string source, string msgNick, string abUrl, Object msg)
        {
            LogAsync(Level.Info, source, msgNick, abUrl, msg);
        }

        /// <summary>
        /// Info级别记录用户有实际意义的日志
        /// </summary>
        /// <param name="source">日志记录位置</param>
        /// <param name="tranCode">交易码可以是一类日志</param>
        /// <param name="msg">记录详细信息</param>
        public static void Info(string source, string tranCode, string msgNick, string abUrl, Object msg)
        {
            LogAsync(Level.Info, source, tranCode, msgNick, abUrl, msg);
        }

        /// <summary>
        /// Info级别记录用户有实际意义的日志
        /// </summary>
        /// <param name="source">日志记录位置</param>
        /// <param name="tranCode">交易码可以是一类日志</param>
        /// <param name="mark">标记</param>
        /// <param name="msg">记录详细信息</param>
        public static void Info(string source, string tranCode, string mark, string msgNick, string abUrl, Object msg)
        {
            LogAsync(Level.Info, source, tranCode, mark, msgNick, abUrl, msg);
        }

        /// <summary>
        /// Warn级别记录警告日志
        /// </summary>
        /// <param name="source">日志记录位置</param>
        /// <param name="msg">记录详细信息</param>
        public static void Warn(string source, string msgNick, string abUrl, Object msg)
        {
            LogAsync(Level.Warn, source, msgNick, abUrl, msg);
        }

        /// <summary>
        /// Warn级别记录警告日志
        /// </summary>
        /// <param name="source">日志记录位置</param>
        /// <param name="msg">记录详细信息</param>
        public static void Warn(string source, string tranCode, string msgNick, string abUrl, Object msg)
        {
            LogAsync(Level.Warn, source, tranCode, msgNick, abUrl, msg);
        }

        /// <summary>
        /// Warn级别记录警告日志
        /// </summary>
        /// <param name="source">日志记录位置</param>
        /// <param name="mark">标记</param>
        /// <param name="msg">记录详细信息</param>
        public static void Warn(string source, string tranCode, string mark, string msgNick, string abUrl, Object msg)
        {
            LogAsync(Level.Warn, source, tranCode, mark, msgNick, abUrl, msg);
        }

        /// <summary>
        /// Error级别记录错误日志
        /// </summary>
        /// <param name="source">日志记录位置</param>
        /// <param name="msg">记录详细信息</param>
        [Obsolete("ReTryLog记录器不能记录错误消息")]
        public static void Error(string source, string msgNick, string abUrl, Exception ex)
        {
            LogAsync(Level.Error, source, msgNick, abUrl, ex);
        }

        /// <summary>
        /// Error级别记录错误日志
        /// </summary>
        /// <param name="source">日志记录位置</param>
        /// <param name="tranCode">交易码可以是一类日志</param>
        /// <param name="ex">记录详细信息</param>
        [Obsolete("ReTryLog记录器不能记录错误消息")]
        public static void Error(string source, string tranCode, string msgNick, string abUrl, Exception ex)
        {
            LogAsync(Level.Error, source, tranCode, msgNick, abUrl, ex);
        }

        /// <summary>
        /// Error级别记录错误日志
        /// </summary>
        /// <param name="source">日志记录位置</param>
        /// <param name="tranCode">交易码可以是一类日志</param>
        /// <param name="mark">标记</param>
        /// <param name="ex">记录详细信息</param>
        [Obsolete("ReTryLog记录器不能记录错误消息")]
        public static void Error(string source, string tranCode, string mark, string msgNick, string abUrl, Exception ex)
        {
            LogAsync(Level.Error, source, tranCode, mark, msgNick, abUrl, ex);
        }

        /// <summary>
        /// Fatal级别记录致命性错误日志
        /// </summary>
        /// <param name="source">日志记录位置</param>
        /// <param name="msg">记录详细信息</param>
        public static void Fatal(string source, string msgNick, string abUrl, Object msg)
        {
            LogAsync(Level.Fatal, source, msgNick, abUrl, msg);
        }

        /// <summary>
        /// Fatal级别记录致命性错误日志
        /// </summary>
        /// <param name="source">日志记录位置</param>
        /// <param name="tranCode">交易码可以是一类日志</param>
        /// <param name="msg">记录详细信息</param>
        public static void Fatal(string source, string tranCode, string msgNick, string abUrl, Object msg)
        {
            LogAsync(Level.Fatal, source, tranCode, msgNick, abUrl, msg);
        }

        /// <summary>
        /// Fatal级别记录致命性错误日志
        /// </summary>
        /// <param name="source">日志记录位置</param>
        /// <param name="tranCode">交易码可以是一类日志</param>
        /// <param name="mark">标记</param>
        /// <param name="msg">记录详细信息</param>
        public static void Fatal(string source, string tranCode, string mark, string msgNick, string abUrl, Object msg)
        {
            LogAsync(Level.Fatal, source, tranCode, mark, msgNick, abUrl, msg);
        }
    }
}