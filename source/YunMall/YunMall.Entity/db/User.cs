﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YunMall.Entity
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class User
    {
        public int Uid { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int Level { get; set; }

        public string RoleId { get; set; }

        public string ParentId { get; set; }

        public int Depth { get; set; }

        public string RegIp { get; set; }

        public string LastIp { get; set; }

        public DateTime LastTime { get; set; }

        public string QQ { get; set; }

        public int State { get; set; }

        public double ReturnRate { get; set; }

        public string CashAccount { get; set; }

        public string RealName { get; set; }

        public DateTime AddTime { get; set; }

        public string Remark { get; set; }
    }
}