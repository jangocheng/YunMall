using  System;
using  System.Collections.Generic;
using  System.Linq;
using  System.Text;
using  System.Threading.Tasks;

namespace DF.Tmall.Common.UnityAop
{
    /// <summary>
    /// 拦截器接口
    /// </summary>
    public interface IInteceptor
    {
        /// <summary>
        /// 排序
        /// </summary>
        int Order { get; }
    }
}
