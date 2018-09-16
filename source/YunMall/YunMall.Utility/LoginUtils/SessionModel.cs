
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YunMall.Entity.dbExt;

namespace YunMall.Utility.LoginUtils
{
    public class SessionModel
    {
        public int Uid { get; set; }

        public UserDetail UserDetail { get; set; }
    }
}
