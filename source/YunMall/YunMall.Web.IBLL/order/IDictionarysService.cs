using YunMall.Entity.db;

namespace YunMall.Web.IBLL.order {
    public interface IDictionarysService {
        /// <summary>
        /// 根据id查询键值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Dictionarys GetValueById(int id);
    }
}