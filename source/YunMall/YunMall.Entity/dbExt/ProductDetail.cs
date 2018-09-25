using System;

namespace YunMall.Entity.dbExt {
    public class ProductDetail {
        /*products*/
        public int Pid { get; set; }
        public int Sid { get; set; }

        public int CategoryId { get; set; }

        public int Type { get; set; }

        public string MainImage { get; set; }

        public string ProductName { get; set; }

        public double Amount { get; set; }

        public string Description { get; set; }

        public int Status { get; set; }

        public DateTime AddTime { get; set; }

        public DateTime EditTime { get; set; }

        /*users*/
        public int Uid { get; set; }

        public string Username { get; set; }

        public int Level { get; set; }

        public string RoleId { get; set; }

        public string ParentId { get; set; }

        public int Depth { get; set; }

        public int State { get; set; }

        public string CashAccount { get; set; }

        public string RealName { get; set; }

        public string Remark { get; set; }

        /*permissions*/
        public int PermissionId { get; set; }

        public string RoleName { get; set; }

        public double ReturnRate { get; set; }
    }
}