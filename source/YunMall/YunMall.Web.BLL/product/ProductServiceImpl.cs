using System;
using System.Collections;
using DF.Common.StringHelper;
using Microsoft.Practices.Unity;
using YunMall.Entity;
using YunMall.Entity.db;
using YunMall.Web.IBLL.product;
using YunMall.Web.IDAL.product;

namespace YunMall.Web.BLL.product {
    public class ProductServiceImpl : IProductService {
        private readonly IProductRepository productRepository;

        [InjectionConstructor]
        public ProductServiceImpl(IProductRepository productRepository) {
            this.productRepository = productRepository;
        }

        /// <summary>
        /// 创建商品 韦德 2018年9月20日19:01:39
        /// </summary>
        /// <param name="product"></param>
        /// <param name="cause"></param>
        /// <returns></returns>
        public bool CreateProduct(Product product, ref string cause) {
            // 条件验证补充
            if (product.Sid <= 0) {
                cause = "请使用商家认证账号发布商品";
                return false;
            }
            if (product.CategoryId <= 0) {
                cause = "请选择一个经营类目";
                return false;
            }
            product.AddTime = DateTime.Now;
            product.EditTime = DateTime.Now;
            product.Status = 0;
            if (product.MainImage == null || product.MainImage.IsEmpty()) product.MainImage = Constants.DefaultProductImage;
            if (product.Type < 0 || product.Type > 3) product.Type = 0;

            // 添加商品
            Hashtable hash = new Hashtable();
            productRepository.Insert(product, ref hash);

            // 事务提交
            productRepository.CommitTransaction(hash);
            return true;
        }
    }
}