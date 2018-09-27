using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using YunMall.Entity.db;
using YunMall.Entity.dbExt;
using YunMall.Web.BLL.util;
using YunMall.Web.IBLL.finance;
using YunMall.Web.IDAL.finance;

namespace YunMall.Web.BLL.finance {
    /// <summary>
    /// 财务交易流水表
    /// </summary>
    public class PaysServiceImpl : BasePageQuery<Pays>, IFinanceService
    {
        private readonly IPaysRepository paysRepository;

        [InjectionConstructor]
        public PaysServiceImpl(IPaysRepository paysRepository) {
            this.paysRepository = paysRepository;
        }

        public int GetPaysPageLimitCount(string condition, int tradeType, int type, string beginTime, string endTime) {
            String where = ExtractLimitWhere(condition, type, beginTime, endTime);
            return paysRepository.SelectLimitCount(type, beginTime, endTime, where);
        }

        public override int GetCount()
        {
            return paysRepository.Count();
        }

        public override IList<PaysDetail> GetPayPageLimit(int page, string limit, string condition, int tradeType, int type, string beginTime, string endTime)
        {
            page = ConditionUtil.ExtractPageIndex(page, limit);
            String where = ExtractLimitWhere(condition, tradeType, type, beginTime, endTime);
            IList<PaysDetail> list = paysRepository.SelectLimit(page, limit, tradeType, type, beginTime, endTime, where);
            return list;
        }
         

        protected  string ExtractLimitWhere(string condition, int tradeType, int type, string beginTime, string endTime)
        {
            // 查询模糊条件
            String where = " 1=1";

            if (type == 0) {
                if (condition != null && condition.Length > 1) {
                    condition = condition.Trim();
                    where += " AND (" + ConditionUtil.Like("payId", condition, true, "t1");
                    where += " OR " + ConditionUtil.Like("systemRecordId", condition, true, "t1");
                    where += " OR " + ConditionUtil.Like("fromName", condition, true, "t1");
                    where += " OR " + ConditionUtil.Like("toName", condition, true, "t1");
                    if (condition.Split('-').Length == 2)
                    {
                        where += " OR " + ConditionUtil.Like("addTime", condition, true, "t1");
                    }
                    where += " OR " + ConditionUtil.Like("tradeType", condition, true, "t1");
                    where += " OR " + ConditionUtil.Like("remark", condition, true, "t1") + ")";
                }

                // 查询全部数据或者只有一类数据
                if (tradeType != 0)
                {
                    where += $" AND t1.tradeType = {tradeType}";
                }
                // 取两个日期之间或查询指定日期
                where = ExtractBetweenTime(beginTime, endTime, where);

            }
            else {
                if (condition != null && condition.Length > 1)
                {
                    condition = condition.Trim();
                    where += " AND (" + ConditionUtil.Like("fromName", condition, true, "t1");
                    where += " OR " + ConditionUtil.Like("toName", condition, true, "t1");
                    where += " OR " + ConditionUtil.Like("amount", condition, true, "t1") + ")";
                }
            }
            return where.Trim();
        }
         
    }
}