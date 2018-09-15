using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YunMall.Entity.ModelView
{
    /// <summary>
    /// 分页公共模板类，非公共字段不允许添加在此类中
    /// </summary>
    public class PageParam : QueryParam
    {
        /// <summary>
        /// 起始页数
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }

    }
}
