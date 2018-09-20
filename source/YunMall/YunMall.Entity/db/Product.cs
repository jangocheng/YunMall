using System;

namespace YunMall.Entity.db {
    public class Product {
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
    }
}