
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YunMall.Utility.LoginUtils
{
    public class SessionModel
    {
     
        public long Uid { get; set; }

        public int Level { get; set; }

        public string RoleId { get; set; }

        public string ParentId { get; set; }

        public int Depth { get; set; }

        public string RegIp { get; set; }

        public string LastIp { get; set; }

        public DateTime LastTime { get; set; }

        public string QQ { get; set; }

        public int State { get; set; }


        public string RealName { get; set; }

        public DateTime AddTime { get; set; }

    }
}
