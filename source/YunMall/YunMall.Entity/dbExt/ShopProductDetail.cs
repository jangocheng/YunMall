using System;

namespace YunMall.Entity.dbExt {
    public class ShopProductDetail {
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

        public string Remark { get; set; }

        /*categorys*/
        public int Cid { get; set; }

        public int ParentId { get; set; }

        public string CategoryName { get; set; }

        public bool IsLeaf { get; set; }

        public bool IsEnable { get; set; }

        public bool IsDelete { get; set; }

        public string ParentName { get; set; }

    }
}