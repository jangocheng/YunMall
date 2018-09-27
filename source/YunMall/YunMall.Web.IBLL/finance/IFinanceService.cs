using System;
using System.Collections;
using System.Collections.Generic;
using YunMall.Entity.db;
using YunMall.Entity.dbExt;

namespace YunMall.Web.IBLL.finance {
    public interface IFinanceService {

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
        IList<Accounts> GetAccountPageLimit(int page, string limit, string condition, int type, string beginTime, string endTime);


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
        IList<PaysDetail> GetPayPageLimit(int page, string limit, string condition, int tradeType, int type, string beginTime, string endTime);

        /// <summary>
        /// 查询总数 韦德 2018年9月22日16:12:26
        /// </summary>
        /// <returns></returns>
        int GetCount();


        /// <summary>
        /// 查询分页总数 韦德 2018年9月22日16:12:21
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="type"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        int GetPageLimitCount(string condition, int type, string beginTime, string endTime);

        /// <summary>
        /// 查询用户收入与支出金额 韦德 2018年9月27日15:50:35
        /// </summary>
        /// <param name="userDetail"></param>
        /// <returns></returns>
        IDictionary<string, string> GetUserAmount(UserDetail userDetail);


        /// <summary>
        /// 查询分页总数 韦德 2018年9月22日16:12:21
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="type"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        int GetPaysPageLimitCount(string condition, int tradeType, int type, string beginTime, string endTime);
    }
}