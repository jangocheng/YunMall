using System;
using System.Collections.Generic;
using System.Web.Http;

namespace DF.Tmall.Common.Attributes
{
    /// <summary>
    /// WebApi请求版本控制特性类
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class RequestMappingAttribute : Attribute
    {
        /// <summary>
        /// 接口所支持的版本字符串
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 接口路径
        /// </summary>
        public string AbslutePath { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="version"></param>
        /// <param name="abslutePath"></param>
        public RequestMappingAttribute(string version, string abslutePath)
        {
            this.Version = version.Trim();
            this.AbslutePath = abslutePath.Replace("/", "").Trim();
        }
    }

    /// <summary>
    /// 加载版本类
    /// </summary>
    public class SetVersion
    {
        /// <summary>
        /// 注册接口版本信息
        /// </summary>
        /// <param name="assemblayName"></param>
        public static void RegisterVersion(string assemblayName)
        {
            IDictionary<string, string[]> versionList = new Dictionary<string, string[]>();
            var assembly = System.Reflection.Assembly.Load(assemblayName);
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.IsClass && type.IsSubclassOf(typeof(ApiController)))
                {
                    var methods = type.GetMethods();
                    foreach (var item in methods)
                    {
                        var customers = item.GetCustomAttributes(typeof(RequestMappingAttribute), true) as RequestMappingAttribute[];
                        foreach (var customer in customers)
                        {
                            // 初始化特性，并进行验证。
                            if (!versionList.ContainsKey(customer.AbslutePath.ToUpper()))
                                versionList.Add(customer.AbslutePath.ToUpper(), customer.Version.ToUpper().Split('|'));
                        }
                    }
                }
            }
            ConstBaseData.VersionList = versionList;
        }
    }
}
