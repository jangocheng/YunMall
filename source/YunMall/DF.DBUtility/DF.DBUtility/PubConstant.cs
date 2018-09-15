/// <summary>
/// 程序说明：数据库操作链接字符串加密处理
/// 各部分之间的关系及本文件与其它文件关系等。
/// 建立者：
/// 建立日期：2014年12月17日。
/// 修改记录：
/// 修改者：：修改日期时间：
/// 修改内容：说明该次修改的程序内容。
/// </summary>
using System;
using System.Configuration;
using DF.Common;
using System.Xml.Linq;

namespace DF.DBUtility
{
    public class PubConstant
    {
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                string _connectionString = ConfigurationManager.AppSettings["connectionString"];
                string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
                if (ConStringEncrypt == "true")
                {
                    _connectionString = DESEncrypt.Decrypt(_connectionString);
                }
                #region yfx增加读取xml文档的数据库链接字符串--yfx
                if (string.IsNullOrEmpty(_connectionString))
                {
                    //读取xml文档
                    //xml文档地址
                    string XmlPath = ConfigurationManager.AppSettings["xmlconfig"].ToString();
                    XElement Xel = XDocument.Load(XmlPath).Element("root");
                    //获取数据库链接字符串
                    _connectionString = Xel.Element("ConnectionString").Value.ToString();
                }
                #endregion
                return _connectionString;
            }
        }

        /// <summary>
        /// 得到web.config里配置项的数据库连接字符串。
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string configName)
        {
            string connectionString = ConfigurationManager.AppSettings[configName];
            string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
            if (ConStringEncrypt == "true")
            {
                connectionString = DESEncrypt.Decrypt(connectionString);
            }
            return connectionString;
        }


        /// <summary>
        /// 得到AppSettings中的配置字符串信息
        /// </summary>
        /// <param name="configName">配置文件的KEY值</param>
        /// <returns>返回数据库连接字符串</returns>
        public static string GetConfigString(string configName)
        {
            string CacheKey = "AppSettings-" + configName;
            object objEntities = DataCache.GetCache(CacheKey);
            if (objEntities == null)
            {
                try
                {
                    string _connectionString = ConfigurationManager.AppSettings[configName];
                    string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
                    if (ConStringEncrypt == "true")
                    {
                        _connectionString = DESEncrypt.Decrypt(_connectionString);
                    }

                    if (_connectionString != null)
                    {
                        DataCache.SetCache(CacheKey, _connectionString, DateTime.Now.AddMinutes(180), TimeSpan.Zero);
                    }
                    return _connectionString;
                }
                catch
                { }
            }
            return objEntities.ToString();
        }

    }
}
