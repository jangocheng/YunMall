namespace DF.Common.StringHelper {
    public static class StringUtil {
        public static bool IsEmpty(this string that) {
            if(that != null) that = that.Trim();
            return that == null || that == string.Empty || that.Length <= 0;
        }
    }
}