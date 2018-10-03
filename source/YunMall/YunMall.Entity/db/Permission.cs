using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YunMall.Entity.db
{
    /// <summary>
    /// 权限表
    /// </summary>
    public class Permission
    {
        public int PermissionId { get; set; }

        public string RoleName { get; set; }
    }
}
