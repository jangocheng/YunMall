using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DF.Tmall.Common.Attributes
{
    /// <summary>
    /// 版本号验证类
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class VersionAttribute : ValidationAttribute
    {

        /// <summary>
        /// 构造函数
        /// </summary>       
        /// <param name="errorMessage">错误提示信息</param>
        public VersionAttribute(string errorMessage) : base(errorMessage)
        {

        }
        /// <summary>
        /// 验证方法
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            var versionKey = HttpContext.Current.Request.Url.AbsolutePath.Replace("/", "").Trim().ToUpper();
            var val = value as string;
            return ((IList) ConstBaseData.VersionList[versionKey]).Contains(val.ToUpper());
        }
    }
}
