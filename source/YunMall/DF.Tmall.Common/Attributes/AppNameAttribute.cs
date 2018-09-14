using System;
using System.ComponentModel.DataAnnotations;

namespace DF.Tmall.Common.Attributes
{
    /// <summary>
    /// 验证AppName特性类
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class AppNameAttribute : ValidationAttribute
    {
        /// <summary>
        /// 验证方法
        /// </summary>
        /// <param name="value">属性值</param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            //return ConstBaseData.AppSecretKey.Contains(value);
            return true;
        }
    }
}
