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
    /// 财务会计账目接口实现类
    /// </summary>
    public class AccountsServiceImpl : BasePageQuery<Accounts>, IFinanceService
    {
        private readonly IAccountsRepository accountsRepository;

        [InjectionConstructor]
        public AccountsServiceImpl(IAccountsRepository accountsRepository) : base(accountsRepository){
            this.accountsRepository = accountsRepository;
        }

        public override int GetCount() {
            return accountsRepository.Count();
        }

        public override IList<Accounts> GetAccountPageLimit(int page, string limit, string condition, int type, string beginTime, string endTime) {
            page = ConditionUtil.ExtractPageIndex(page, limit);
            String where = ExtractLimitWhere(condition, type, beginTime, endTime);
            IList<Accounts> list = accountsRepository.SelectLimit(page, limit, type, beginTime, endTime, where);
            return list;
        }


        public override int GetPageLimitCount(string condition, int type, string beginTime, string endTime) {
            String where = ExtractLimitWhere(condition, type, beginTime, endTime);
            return accountsRepository.SelectLimitCount(type, beginTime, endTime, where);
        }

        protected override string ExtractLimitWhere(string condition, int type, string beginTime, string endTime) {
            // 查询模糊条件
            String where = " 1=1";


            if (type > 0)
            {
                where += " AND t1.accountsType = " + type + " ";
            }

            if (condition != null && condition.Length > 1)
            {
                condition = condition.Trim(); 

                if (condition.Contains("-"))
                {
                    where += " AND  " + ConditionUtil.Match("accountsType", "2", true, "t1");
                    where += " AND " + ConditionUtil.Match("amount", condition.Replace("-", "").Trim(), true, "t1") ;
                }
                else if (condition.Contains("+"))
                {
                    where += " AND  " + ConditionUtil.Match("accountsType", "1", true, "t1");
                    where += " AND " + ConditionUtil.Match("amount", condition.Replace("+", "").Trim(), true, "t1");
                }
                else
                {
                    where += " AND (" + ConditionUtil.Like("accountsId", condition, true, "t1");
                    where += " AND (" + ConditionUtil.Like("payId", condition, true, "t1");
                    if (condition.Split('-').Length >= 1)
                    {
                        where += " OR " + ConditionUtil.Like("addTime", condition, true, "t1");
                    }
                    where += " OR " + ConditionUtil.Like("tradeAccountName", condition, true, "t1");
                    where += " OR " + ConditionUtil.Like("remark", condition, true, "t1") + ")";

                    where += ")";
                }
            }

            // 取两个日期之间或查询指定日期
            where = ExtractBetweenTime(beginTime, endTime, where);
            return where.Trim();
        }
         
    }
}