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

        /// <summary>
        /// 获取分类详细信息 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        CategoryDetail GetCategoryDetail(int value);

        /// <summary>
        /// 添加经营类目
        /// </summary>
        /// <param name="value"></param>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        bool Add(int value, string categoryName);

        /// <summary>
        /// 编辑经营类目
        /// </summary>
        /// <param name="value"></param>
        /// <param name="categoryId"></param>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        bool Edit(int value, int categoryId, string categoryName);
    }
}