using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YunMall.Entity.ModelView
{
    /// <summary>
    /// 查询公共模板类，非公共字段不允许添加在此类中
    /// </summary>
    public class QueryParam
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 查询列
        /// </summary>
        public string Fields { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public string StrWhere { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderBy { get; set; }
    }

    public class BatchParam
    {
        public Type Type { get; set; }

        public string strSql { get; set; }

    }
}
