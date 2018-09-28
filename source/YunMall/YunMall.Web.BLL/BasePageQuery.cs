using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using YunMall.Entity;
using YunMall.Entity.db;
using YunMall.Entity.dbExt;
using YunMall.Entity.ModelView;
using YunMall.Web.BLL.util;
using YunMall.Web.IDAL.finance;
using YunMall.Web.IDAL.user;

namespace YunMall.Web.BLL {
    public class BasePageQuery<T> {
        private readonly IAccountsRepository accountsRepository;

        [Dependency]
        public IUserRepository userRepository { get; set; }

        [InjectionConstructor]
        public BasePageQuery(IAccountsRepository accountsRepository)
        {
            this.accountsRepository = accountsRepository;
        }

        public BasePageQuery()
        {
        } 


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
        public virtual IList<T> GetPageLimit<T>(int page, string limit, string condition, int type, string beginTime, string endTime)
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
        /// <param name="type"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public virtual int GetPageLimitCount(string condition, int type, string beginTime, string endTime)
        {
            String where = ExtractLimitWhere(condition, type, beginTime, endTime);
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



        /// <summary>
        /// 查询会计账目分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="condition"></param>
        /// <param name="type"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public virtual IList<Accounts> GetAccountPageLimit(int page, string limit, string condition, int type,
            string beginTime, string endTime) {
            return null;
        }


        /// <summary>
        /// 查询交易流水分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="condition"></param>
        /// <param name="type"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public virtual IList<PaysDetail> GetPayPageLimit(int page, string limit, string condition, int tradeType, int type, string beginTime,
            string endTime) {
            return null;
        }

        /// <summary>
        /// 查询用户收入与支出金额 韦德 2018年9月27日15:50:35
        /// </summary>
        /// <param name="userDetail"></param>
        /// <returns></returns>
        public virtual IDictionary<string, string> GetUserAmount(UserDetail userDetail) {
            // 查询收入与支出数据
            IDictionary<string, string> data = accountsRepository.SelectAmount(userDetail.User.Uid);
            // 查询系统公户信息
            var dataList = userRepository.Query<User>(new QueryParam() {
                StrWhere = $"uid={Constants.HotAccountID}"
            });
            if (dataList != null && dataList.Count > 0) {
                data.Add("account", dataList.FirstOrDefault().Username);
                data.Add("amount", "0");// TODO 等钱包表设计好之后，这里改为查询钱包表数据
            }
            else {
                data.Add("account",  "查询失败");
                data.Add("amount", "查询失败");
            }
            return data;
        }


        /// <summary>
        /// 查询分页总数 韦德 2018年9月22日16:12:21
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="type"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public virtual int GetPaysPageLimitCount(string condition, int tradeType, int type, string beginTime, string endTime) {
            return 0;
        }


        /// <summary>
        /// 人工充值转账
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public virtual bool Recharge(int uid, double amount) {
            return false;
        }

    }
}