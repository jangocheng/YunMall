using  System.IO;

namespace  DF.Log
{
    /// <summary>
    /// log4net配置文件管理类
    /// </summary>
    public class XmlConfigurator
    {
        /// <summary>
        /// 注册log4net的log4net.config文件用户自定义的方式方式
        /// </summary>
        public static void ConfigureAndWatch(FileInfo configFile)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(configFile);
        }

        /// <summary>
        /// 注册log4net的log4net.config文件默认方式
        /// </summary>
        public static void ConfigureAndWatch()
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(System.Threading.Thread.GetDomain().BaseDirectory + "bin\\log4net.config"));
        }
    }
}
