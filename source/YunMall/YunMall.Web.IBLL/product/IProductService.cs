using YunMall.Entity.db;

namespace YunMall.Web.IBLL.product {
    public interface IProductService {
        /// <summary>
        /// 创建商品 韦德 2018年9月20日19:01:39
        /// </summary>
        /// <param name="product"></param>
        /// <param name="cause"></param>
        /// <returns></returns>
        bool CreateProduct(Product product, ref string cause);
    }
}