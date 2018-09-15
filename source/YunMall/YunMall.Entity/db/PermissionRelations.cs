using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YunMall.Entity.db
{
    /// <summary>
    /// 权限关系表
    /// </summary>
    public class PermissionRelations
    {
        public int RelationId { get; set; }

        public int Uid { get; set; }

        public string PermissionList { get; set; }
    }
}
