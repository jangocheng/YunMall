using YunMall.Entity.dbExt;

namespace YunMall.Web.IDAL.product {
    public interface IProductRepository : IAbsBaseRepository
    {
        /// <summary>
        /// 查询商品详情 韦德 2018年9月25日15:39:40
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        ProductDetail QueryDetail(int value);
    }
}