using System.ComponentModel.DataAnnotations;

namespace DF.Tmall.Common.Attributes
{
    /// <summary>
    /// 验证必须为true特性类
    /// </summary>
    public class MustBeTrueAttribute : ValidationAttribute
    {
        /// <summary>
        /// 验证方法
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            return value is bool && (bool)value;
        }
    }
}
