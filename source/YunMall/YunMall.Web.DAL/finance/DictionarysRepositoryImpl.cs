using System.Collections.Generic;
using MySql.Data.MySqlClient;
using YunMall.Entity.db;
using YunMall.Web.IDAL.finance;

namespace YunMall.Web.DAL.finance {
    public class DictionarysRepositoryImpl : AbsBaseRepository, IDictionarysRepository {
        public DictionarysRepositoryImpl() : base("dictionarys") {
            Dictionarys model = new Dictionarys();
            base.Fields = "(" + base.JoinFieldStrings(model) + ")";
        }

        protected override string GetInsertFields()
        {
            return base.Fields;
        }

        protected override string GetInsertValues<T>(T item, out MySqlParameter[] param)
        {
            var model = item as Dictionarys;
            return base.JoinFieldValues(model, out param);
        }

        protected override string GetInsertValues<T>(T item, int index, ref IList<MySqlParameter> param)
        {
            var model = item as Dictionarys;
            return base.JoinFields(model, index);
        }
    }
}