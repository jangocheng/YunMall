using System;
using System.Collections;
using System.Linq;
using System.Text;
using DF.Common.StringHelper;
using Microsoft.Practices.Unity;
using YunMall.Entity;
using YunMall.Entity.db;
using YunMall.Entity.dbExt;
using YunMall.Entity.ModelView;
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
            if (product.Type < 0 || product.Type > 2) product.Type = 0;

            // 添加商品
            Hashtable hash = new Hashtable();
            productRepository.Insert(product, ref hash);

            // 事务提交
            productRepository.CommitTransaction(hash);
            return true;
        }

        /// <summary>
        /// 上架商品 韦德 2018年9月24日17:01:24
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <param name="cause"></param>
        /// <returns></returns>
        public bool Putaway(int userId, string productId, ref string cause) {
            var result = productRepository.Update("status = 1, remark='商家快捷上架'", $"sid={userId} AND pid IN({productId})");
            // TODO::重构时需注意，商品失败原因要返回给上游
            return result;
        }

        /// <summary>
        /// 查询商品信息 韦德 2018年9月25日14:08:10
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ProductDetail GetProduct(int value) {
            return productRepository.QueryDetail(value);
        }


        /// <summary>
        /// 下架商品 韦德 2018年9月24日17:07:43
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <param name="cause"></param>
        /// <returns></returns>
        public bool UnShelve(int userId, string productId, ref string cause) {
            var result = productRepository.Update("status = 2, remark='商家快捷下架'", $"sid={userId} AND pid IN({productId})");
            // TODO::重构时需注意，商品失败原因要返回给上游
            return result;
        }

        /// <summary>
        /// 编辑商品 韦德 2018年9月25日14:31:59
        /// </summary>
        /// <param name="product"></param>
        /// <param name="cause"></param>
        /// <returns></returns>
        public bool EditProduct(Product product, ref string cause)
        {
            // 条件验证补充
            if (product.Sid <= 0)
            {
                cause = "请使用商家认证账号发布商品";
                return false;
            }
            if (product.CategoryId <= 0)
            {
                cause = "请选择一个经营类目";
                return false;
            }
            product.EditTime = DateTime.Now;
            product.Status = 0;
            if (product.MainImage == null || product.MainImage.IsEmpty()) product.MainImage = Constants.DefaultProductImage;
            if (product.Type < 0 || product.Type > 2) product.Type = 0;

            // 编辑商品
            StringBuilder builder = new StringBuilder();
            builder.Append($"productName='{product.ProductName}',");
            builder.Append($"amount={product.Amount},");
            builder.Append($"mainImage='{product.MainImage}',");
            builder.Append($"type={product.Type},");
            builder.Append($"description='{product.Description}'");
            return productRepository.Update(builder.ToString(), $"sid = {product.Sid} AND pid = {product.Pid}");
        }
    }
}