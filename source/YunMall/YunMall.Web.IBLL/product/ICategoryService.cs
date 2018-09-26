using System.Collections.Generic;
using YunMall.Entity.db;
using YunMall.Entity.dbExt;

namespace YunMall.Web.IBLL.product {
    public interface ICategoryService {
        /// <summary>
        /// 查询所有经营类目
        /// </summary>
        /// <returns></returns>
        IList<CategoryDetail> GetCategoryDetails();
    }
}