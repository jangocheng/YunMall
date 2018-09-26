namespace YunMall.Entity.db {
    /// <summary>
    /// 经营类目
    /// </summary>
    public class Category {
        public int Cid { get; set; }

        public int ParentId { get; set; }

        public string CategoryName { get; set; }

        public bool IsLeaf { get; set; }

        public bool IsEnable { get; set; }

        public bool IsDelete { get; set; }
    }
}