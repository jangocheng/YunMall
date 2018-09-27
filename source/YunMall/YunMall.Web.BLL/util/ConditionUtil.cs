using System;

namespace YunMall.Web.BLL.util {
    public class ConditionUtil {
        /**
     * 计算分页位置
     * @param page
     * @param limit
     * @return
     */
        public static int ExtractPageIndex(int page, string limit)
        {
            if (!limit.Equals("-1"))
            {
                page = page - 1;
                if (page != 0)
                {
                    page = page * int.Parse(limit);
                }
            }
            return page;
        }

        /**
         * 模糊查询某个字段
         * @param column
         * @param value
         * @param isJoin
         * @param alias
         * @return
         */
        public static String Like(String column, String value, bool isJoin, String alias)
        {
            String condition = "";
            if (!isJoin)
            {
                alias = "";
            }
            else
            {
                alias += ".";
            }
            String tb = alias + "`" + column + "`";
            condition += tb + " LIKE '%" + value + "' OR ";
            condition += tb + " LIKE '" + value + "%' OR ";
            condition += tb + " LIKE '%" + value + "%' ";
            return condition;
        }


        /**
         * 准确匹配
         * @param column
         * @param value
         * @param isJoin
         * @param alias
         * @return
         */
        public static String Match(String column, String value, bool isJoin, String alias)
        {
            String condition = "";
            if (!isJoin)
            {
                alias = "";
            }
            else
            {
                alias += ".";
            }
            String tb = alias + "`" + column + "`";
            condition += tb + " = " + value + "  ";
            return condition;
        }


        /**
         * 准确不匹配
         * @param column
         * @param value
         * @param isJoin
         * @param alias
         * @return
         */
        public static String NotMatch(String column, String value, bool isJoin, String alias)
        {
            String condition = "";
            if (!isJoin)
            {
                alias = "";
            }
            else
            {
                alias += ".";
            }
            String tb = alias + "`" + column + "`";
            condition += tb + " != " + value + "  ";
            return condition;
        }
    }
}