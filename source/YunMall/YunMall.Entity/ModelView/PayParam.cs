namespace YunMall.Entity.ModelView {
    public class PayParam {
        public int FromUid { get; set; }
        public int ToUid { get; set; }
        public double Amount { get; set; }
        public string Remark { get; set; }
        public long SystemRecordId { get; set; }
        public int Currency { get; set; } = 0;


        public string FromUsername { get; set; }
        public string ToUsername { get; set; }
    }
}