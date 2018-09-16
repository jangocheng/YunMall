using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YunMall.Entity.db;

namespace YunMall.Web.IDAL.user
{
    /// <summary>
    /// 权限数据仓储接口
    /// </summary>
    public interface IPermissionRepository : IAbsBaseRepository {

        #region 根据uid查询权限列表 韦德 2018年9月16日14:35:31

        /// <summary>
        /// 根据uid查询权限列表 韦德 2018年9月16日14:35:31
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        IList<Permission> SelectList(int uid);

        #endregion
       
    }
}
