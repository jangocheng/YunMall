using System;
using System.Collections.Generic;
using System.Linq;
using YunMall.Entity.dbExt;
using YunMall.Web.BLL.util;

namespace YunMall.Web.BLL {
    public class BasePageQuery<T> {
        /// <summary>
        /// 查询总数 韦德 2018年9月22日16:12:46
        /// </summary>
        /// <returns></returns>
        public virtual int GetCount()
        {
            //return productRepository.Count();
            return 0;
        }

        /// <summary>
        /// 通用分页查询 韦德 2018年9月22日16:11:04
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="condition"></param>
        /// <param name="type"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public virtual IList<T> GetPageLimit(int page, string limit, string condition, int type, string beginTime, string endTime)
        {
            page = ConditionUtil.ExtractPageIndex(page, limit);
            String where = ExtractLimitWhere(condition, type, beginTime, endTime);
            //List<T> list = productRepository.SelectLimit(page, limit, state, beginTime, endTime, where);
            //return list;
            return null;
        }


        /// <summary>
        /// 通用分页查询总数 韦德 2018年9月22日16:11:16
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="state"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public virtual int GetPageLimitCount(string condition, int state, string beginTime, string endTime)
        {
            String where = ExtractLimitWhere(condition, state, beginTime, endTime);
            //return productRepository.SelectLimitCount(state, beginTime, endTime, where);
            return 0;
        }


        /// <summary>
        /// 提取分页条件
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="type"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        protected virtual String ExtractLimitWhere(String condition, int type, String beginTime, String endTime)
        {
            // 查询模糊条件
            String where = " 1=1";
            if (condition != null && condition.Length > 1)
            {
                condition = condition.Trim();
                where += " AND (" + ConditionUtil.Like("sid", condition, true, "t1");
                if (condition.Split('-').Length == 2)
                {
                    where += " OR " + ConditionUtil.Like("addTime", condition, true, "t1");
                    where += " OR " + ConditionUtil.Like("editTime", condition, true, "t1");
                }
                where += " OR " + ConditionUtil.Like("productName", condition, true, "t1");
                where += " OR " + ConditionUtil.Like("categoryId", condition, true, "t1") + ")";
            }

            // 查询全部数据或者只有一类数据
            // where = extractQueryAllOrOne(isEnable, where);

            // 取两个日期之间或查询指定日期
            where = ExtractBetweenTime(beginTime, endTime, where);
            return where.Trim();
        }


        /// <summary>
        /// 提取两个日期之间的sql条件
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        protected virtual String ExtractBetweenTime(String beginTime, String endTime, String where)
        {
            if ((beginTime != null && beginTime.Contains('-')) &&
                endTime != null && endTime.Contains('-'))
            {
                where += $" AND t1.addTime BETWEEN '{beginTime}' AND '{endTime}'";
            }
            else if (beginTime != null && beginTime.Contains('-'))
            {
                where += $" AND t1.addTime BETWEEN '{beginTime}' AND '{endTime}'";
            }
            else if (endTime != null && endTime.Contains('-'))
            {
                where += $" AND t1.addTime BETWEEN '{beginTime}' AND '{endTime}'";
            }
            return where;
        }


        /// <summary>
        /// 提取是否禁用的条件
        /// </summary>
        /// <param name="isEnable"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        protected virtual  String ExtractQueryAllOrOne(int isEnable, String where)
        {
            if (isEnable != null && isEnable != 0)
            {
                where += $" AND t1.is_enable = {isEnable}";
            }
            return where;
        }
    }
}