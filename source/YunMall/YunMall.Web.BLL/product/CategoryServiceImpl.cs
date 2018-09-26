using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.Unity;
using YunMall.Entity.dbExt;
using YunMall.Entity.ModelView;
using YunMall.Web.IBLL.product;
using YunMall.Web.IDAL.product;

namespace YunMall.Web.BLL.product {
    /// <summary>
    /// 经营类目服务
    /// </summary>
    public class CategoryServiceImpl : ICategoryService {
        private readonly ICategoryRepository categoryRepository;

        [InjectionConstructor]
        public CategoryServiceImpl(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public IList<CategoryDetail> GetCategoryDetails()
        {
            IList<CategoryDetail> list = categoryRepository.QueryDetails();
            if (list == null || list.Count <= 0) return null;
            return list;
        }
    }
}