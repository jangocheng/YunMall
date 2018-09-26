using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.Unity;
using YunMall.Entity.db;
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


        /// <summary>
        /// 获取分类信息列表
        /// </summary>
        /// <returns></returns>
        public IList<CategoryDetail> GetCategoryDetails()
        {
            IList<CategoryDetail> list = categoryRepository.QueryDetails();
            if (list == null || list.Count <= 0) return null;
            return list;
        }

        /// <summary>
        /// 获取分类详细信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CategoryDetail GetCategoryDetail(int value) {
            return categoryRepository.QueryDetail(value);
        }

        /// <summary>
        /// 添加经营类目
        /// </summary>
        /// <param name="value"></param>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public bool Add(int value, string categoryName) {
            Hashtable hash = new Hashtable();
            categoryRepository.Insert(new Category() {
                ParentId = value,
                CategoryName = categoryName,
                IsEnable = true,
                IsDelete =  false,
                IsLeaf = value != 0
            }, ref hash);
            categoryRepository.CommitTransaction(hash);
            return true;
        }

        /// <summary>
        /// 编辑经营类目
        /// </summary>
        /// <param name="value"></param>
        /// <param name="categoryId"></param>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public bool Edit(int value, int categoryId, string categoryName) {
            StringBuilder builder = new StringBuilder();
            builder.Append($"parentId={value}, ");
            builder.Append($"categoryName='{categoryName}' ");
            return categoryRepository.Update(builder.ToString(), $"cid = {categoryId}");
        }
    }
}