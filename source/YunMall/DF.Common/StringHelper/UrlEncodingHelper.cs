using System.Text;
using System.Web;

namespace DF.Common.StringHelper {
    public static class UrlEncodingHelper {
        public static string Dencode(this string that) {
            return HttpUtility.UrlDecode(that, Encoding.UTF8);
        }

        public static string Encode(this string that) {
            return HttpUtility.UrlEncode(that, Encoding.UTF8);
        }
    }
}