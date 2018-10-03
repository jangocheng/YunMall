using System.ComponentModel.Design;
using System.Linq;
using Microsoft.Practices.Unity;
using YunMall.Entity.db;
using YunMall.Entity.ModelView;
using YunMall.Web.IBLL.order;
using YunMall.Web.IDAL.finance;

namespace YunMall.Web.BLL.order {
    public class DictionarysServiceImpl : IDictionarysService {
        private readonly IDictionarysRepository dictionarysRepository;

        [InjectionConstructor]
        public DictionarysServiceImpl(IDictionarysRepository dictionarysRepository) {
            this.dictionarysRepository = dictionarysRepository;
        }

        /// <summary>
        /// 根据id查询键值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dictionarys GetValueById(int id) {
            var dictionaryses = dictionarysRepository.Query<Dictionarys>(new QueryParam() {
                StrWhere = "dictionaryId = '"+ id +"'"
            });
            if (dictionaryses == null || dictionaryses.Count == 0) return null;
            return dictionaryses.First();
        }
    }
}