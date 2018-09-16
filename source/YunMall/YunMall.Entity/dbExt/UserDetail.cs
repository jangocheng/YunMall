using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YunMall.Entity.db;

namespace YunMall.Entity.dbExt
{
    public class UserDetail 
    {
        /*users*/
        public User User { get; set; }

        public IList<User> ParentUsers { get; set; }

        /*permissions*/
        public IList<Permission> Permissions { get; set; }
    }
}
