using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF.Common;
using DF.DBUtility.MySql;
using MySql.Data.MySqlClient;
using YunMall.Entity.db;
using YunMall.Entity.dbExt;
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

        public List<ProductDetail> SelectLimit(int page, string limit, int state, string beginTime, string endTime, string where)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT t1.*, t2.*, (SELECT permissionId FROM permissions WHERE permissionId IN(t3.permissionList)), (SELECT roleName FROM permissions WHERE permissionId IN(t3.permissionList)) FROM products t1 ");
            builder.Append("LEFT JOIN users t2 ON t1.sid = t2.uid ");
            builder.Append("LEFT JOIN permission_relations t3 ON t1.sid = t3.uid ");
            builder.AppendFormat($"WHERE {where} GROUP BY t1.pid ORDER BY t1.addTime DESC LIMIT {page},{limit}");
            var dataSet = DBHelperMySql.Query(builder.ToString());
            if (dataSet == null || dataSet.Tables.Count == 0) return null;
            return dataSet.Tables[0].ToList<ProductDetail>();
        }

        public int SelectLimitCount(int state, string beginTime, string endTime, string where)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(t1.pid) FROM products t1 ");
            builder.Append("LEFT JOIN users t2 ON t1.sid = t2.uid ");
            builder.AppendFormat($"WHERE {where}");
            var result = DBHelperMySql.GetSingle(builder.ToString());
            return Convert.ToInt32(result);
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