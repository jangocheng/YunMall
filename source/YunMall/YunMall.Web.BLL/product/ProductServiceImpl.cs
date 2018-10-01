using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF.Common.StringHelper;
using Microsoft.Practices.Unity;
using YunMall.Entity;
using YunMall.Entity.db;
using YunMall.Entity.dbExt;
using YunMall.Entity.ModelView;
using YunMall.Web.BLL.util;
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
        /// 查询店铺商品信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public IList<ShopProductDetail> GetShopProducts(int uid) {
            return productRepository.GetShopProducts(uid);
        }

        /// <summary>
        /// 查询商品信息列表
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public IList<ProductDetail> GetShopProducts(string pid) {
            return productRepository.QueryDetails(pid);
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
            builder.Append($"description='{product.Description}',");
            builder.Append($"categoryId='{product.CategoryId}',");
            builder.Append($"status=0");
            return productRepository.Update(builder.ToString(), $"sid = {product.Sid} AND pid = {product.Pid}");
        }



        /// <summary>
        /// 查询总数 韦德 2018年9月22日16:12:46
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            return productRepository.Count();
        }

        /// <summary>
        /// 通用分页查询 韦德 2018年9月22日16:11:04
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="condition"></param>
        /// <param name="state"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public IList<ProductDetail> GetLimit(int page, string limit, string condition, int state, string beginTime, string endTime)
        {
            page = ConditionUtil.ExtractPageIndex(page, limit);
            String where = ExtractLimitWhere(condition, state, beginTime, endTime);
            List<ProductDetail> list = productRepository.SelectLimit(page, limit, state, beginTime, endTime, where);
            return list;
        }


        /// <summary>
        /// 通用分页查询总数 韦德 2018年9月22日16:11:16
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="state"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int GetLimitCount(string condition, int state, string beginTime, string endTime)
        {
            String where = ExtractLimitWhere(condition, state, beginTime, endTime);
            return productRepository.SelectLimitCount(state, beginTime, endTime, where);
        }


        /// <summary>
        /// 提取分页条件
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="isEnable"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        private String ExtractLimitWhere(String condition, int isEnable, String beginTime, String endTime)
        {
            // 查询模糊条件
            String where = " 1=1";
            if (condition != null)
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
        private String ExtractBetweenTime(String beginTime, String endTime, String where)
        {
            if ((beginTime != null && beginTime.Contains('-')) &&
                endTime != null && endTime.Contains('-'))
            {
                where += $" AND t1.addTime BETWEEN {beginTime} AND {endTime}";
            }
            else if (beginTime != null && beginTime.Contains('-'))
            {
                where += $" AND t1.addTime BETWEEN {beginTime} AND {endTime}";
            }
            else if (endTime != null && endTime.Contains('-'))
            {
                where += $" AND t1.addTime BETWEEN {beginTime} AND {endTime}";
            }
            return where;
        }


        /// <summary>
        /// 提取是否禁用的条件
        /// </summary>
        /// <param name="isEnable"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        private String ExtractQueryAllOrOne(int isEnable, String where)
        {
            if (isEnable != null && isEnable != 0)
            {
                where += $" AND t1.is_enable = {isEnable}";
            }
            return where;
        }
    }
}