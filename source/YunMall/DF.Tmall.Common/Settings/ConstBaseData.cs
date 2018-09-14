// ========================================描述信息========================================
// 静态数据类
// ========================================创建信息========================================
// 创建人：  DF
// 创建时间： 2017年9月17日16:16:41
// ========================================变更信息========================================
// 日期          版本        修改人         描述
// 
// ========================================================================================

using System;
using System.Collections.Generic;
using DF.Common;
using System.Configuration;
using System.Xml.Linq;

namespace  DF.Tmall.Common {
    /// <summary>
    /// 静态数据类
    /// </summary>
    public static class ConstBaseData {

        static XElement Xel = null;

        static ConstBaseData() {
            string XmlPath = ConfigurationManager.AppSettings["xmlPath"].ToString();
            Xel = XDocument.Load(XmlPath).Element("root");

        }

        /// <summary>
        /// 静态文件地址
        /// </summary>
        public static string StaticFile { get { return Xel.Element("static").Value.ToString(); } }

        /// <summary>
        /// My项目
        /// </summary>
        public static string My { get { return Xel.Element("my").Value.ToString(); } }

        /// <summary>
        /// Order项目
        /// </summary>
        public static string Order { get { return Xel.Element("order").Value.ToString(); } }

        /// <summary>
        /// Pay项目
        /// </summary>
        public static string Pay { get { return Xel.Element("pay").Value.ToString(); } }

        /// <summary>
        /// 登录key
        /// </summary>
        public static string LoginCookieKey { get { return Xel.Element("loginKey").Value.ToString(); } }


        /// <summary>
        /// Manager项目
        /// </summary>
        public static string Manager { get { return Xel.Element("manager").Value.ToString(); } }

        /// <summary>
        /// 管理员等级
        /// </summary>
        public static int RootLevel { get { return Convert.ToInt32(Xel.Element("rootLevel").Value); } }

        /// <summary>
        /// 管理系统标题
        /// </summary>
        public static string AdminTitle { get { return Xel.Element("adminTitle").Value.ToString(); } }

        /// <summary>
        /// 版本键值集合  
        /// </summary>
        public static IDictionary<string, string[]> VersionList { get; set; }

    }
}
