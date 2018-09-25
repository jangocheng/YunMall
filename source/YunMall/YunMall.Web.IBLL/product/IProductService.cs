using YunMall.Entity.db;
using YunMall.Entity.dbExt;

namespace YunMall.Web.IBLL.product {
    public interface IProductService {
        /// <summary>
        /// 创建商品 韦德 2018年9月20日19:01:39
        /// </summary>
        /// <param name="product"></param>
        /// <param name="cause"></param>
        /// <returns></returns>
        bool CreateProduct(Product product, ref string cause);

        /// <summary>
        /// 上架商品 韦德 2018年9月24日17:00:43
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <param name="cause"></param>
        /// <returns></returns>
        bool Putaway(int userId, string productId, ref string cause);


        /// <summary>
        /// 查询商品信息 韦德 2018年9月25日14:07:36
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        ProductDetail GetProduct(int value);

        /// <summary>
        /// 下架商品 韦德 2018年9月24日17:05:47 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <param name="cause"></param>
        /// <returns></returns>
        bool UnShelve(int userId, string productId, ref string cause);

        /// <summary>
        /// 编辑商品 韦德 2018年9月25日14:31:18
        /// </summary>
        /// <param name="product"></param>
        /// <param name="cause"></param>
        /// <returns></returns>
        bool EditProduct(Product product, ref string cause);
    }
}