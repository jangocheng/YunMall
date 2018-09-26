namespace YunMall.Entity.dbExt {
    public class CategoryDetail {
        /*categorys*/
        public int Cid { get; set; }

        public int ParentId { get; set; }

        public string CategoryName { get; set; }

        public bool IsLeaf { get; set; }

        public bool IsEnable { get; set; }

        public bool IsDelete { get; set; }

        /*ext*/
        public string ParentName { get; set; }

    }
}