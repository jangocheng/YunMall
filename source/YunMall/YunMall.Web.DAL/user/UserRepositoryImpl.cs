using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using YunMall.Entity.db;
using YunMall.Web.DAL.utils;
using YunMall.Web.IDAL.user;

namespace YunMall.Web.DAL.user {
    /// <summary>
    /// 用户数据仓储接口实现类
    /// </summary>
    public class UserRepositoryImpl : AbsBaseRepository, IUserRepository {
        public UserRepositoryImpl() : base("users")
        {
            User user = new User();
            base.Fields = "(" + base.JoinFieldStrings(user) + ")";
        }

        protected override string GetInsertFields()
        {
            return base.Fields;
        }

        protected override string GetInsertValues<T>(T item, out MySqlParameter[] param)
        {
            var model = item as User;
            return base.JoinFieldValues(model, out param);
        }

        protected override string GetInsertValues<T>(T item, int index, ref IList<MySqlParameter> param)
        {
            var model = item as User;
            return base.JoinFields(model, index);
        }
    }
}