using System.Collections.Generic;

namespace DF.Tmall.Common {
    public static class JoinUtility {
        public static string AsJoin(this IList<int> that) {
            return string.Join(",", that);
        }

        public static string AsJoin(this IList<long> that)
        {
            return string.Join(",", that);
        }

        public static string AsJoin(this IEnumerable<int> that)
        {
            return string.Join(",", that);
        }

        public static string AsJoin(this IEnumerable<long> that)
        {
            return string.Join(",", that);
        }
    }
}