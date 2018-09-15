using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YunMall.Entity.ModelView
{
    public class PageModel<T>
    {
        /// <summary>
        /// 列表
        /// </summary>
        public IList<T> List { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage { get { return int.Parse(Math.Ceiling(Convert.ToDouble(this.TotalCount) / this.PageSize).ToString()); } }
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }


    }
}
