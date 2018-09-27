using System.Collections.Generic;
using MySql.Data.MySqlClient;
using YunMall.Entity.db;
using YunMall.Web.IDAL.finance;

namespace YunMall.Web.DAL.finance {
    public class PaysRepositoryImpl : AbsBaseRepository, IPaysRepository {
        public PaysRepositoryImpl() : base("pays")
        {
            Pays model = new Pays();
            base.Fields = "(" + base.JoinFieldStrings(model) + ")";
        }

        protected override string GetInsertFields()
        {
            return base.Fields;
        }

        protected override string GetInsertValues<T>(T item, out MySqlParameter[] param)
        {
            var model = item as Pays;
            return base.JoinFieldValues(model, out param);
        }

        protected override string GetInsertValues<T>(T item, int index, ref IList<MySqlParameter> param)
        {
            var model = item as Pays;
            return base.JoinFields(model, index);
        }
    }
}