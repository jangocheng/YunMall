using System.Collections.Generic;
using YunMall.Entity.db;
using YunMall.Entity.dbExt;

namespace YunMall.Web.IBLL.finance {
    public interface IFinanceService {
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
        IList<Accounts> GetPageLimit(int page, string limit, string condition, int type, string beginTime, string endTime);


        /// <summary>
        /// 查询总数 韦德 2018年9月22日16:12:26
        /// </summary>
        /// <returns></returns>
        int GetCount();


        /// <summary>
        /// 查询分页总数 韦德 2018年9月22日16:12:21
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="state"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        int GetPageLimitCount(string condition, int state, string beginTime, string endTime);
    }
}