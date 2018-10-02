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

        /*wallets*/
        public Wallet Wallet { get; set; }

        public IList<User> ParentUsers { get; set; }

        /*permissions*/
        public IList<Permission> Permissions { get; set; }

        /// <summary>
        /// 收入金额
        /// </summary>
        public double IncomeAmount { get; set; }

        /// <summary>
        /// 支出金额
        /// </summary>
        public double ExpendAmount { get; set; }

    }
}
