using System.Collections.Generic;
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

        /// <summary>
        /// 查询分页总数
        /// </summary>
        /// <param name="state"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        int SelectLimitCount(int state, string beginTime, string endTime, string where);

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="state"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        List<ProductDetail> SelectLimit(int page, string limit, int state, string beginTime, string endTime, string where);

        /// <summary>
        /// 查询店铺商品信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        IList<ShopProductDetail> GetShopProducts(int uid);

        /// <summary>
        /// 查询商品信息列表
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        IList<ProductDetail> QueryDetails(string pid);
    }
}