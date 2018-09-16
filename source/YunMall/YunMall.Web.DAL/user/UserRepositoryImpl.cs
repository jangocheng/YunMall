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
            base.Fields = @"*";
        }

        protected override string GetInsertFields()
        {
            return base.Fields;
        }

        protected override string GetInsertValues<T>(T item, out MySqlParameter[] param)
        {
            var model = item as User;
            var obj = new object();
            var fieldList = EntityUtil.GetFieldList(model, ref obj);
            var fieldNameList = EntityUtil.GetFieldNameList(model);
            var paras = new List<MySqlParameter>();

            foreach (var fieldInfo in fieldList) {
                paras.Add(new MySqlParameter("?" + fieldInfo.Name.GetFieldName(), fieldInfo.GetValue(obj))); 
            }

            param = paras.ToArray();

            StringBuilder builder = new StringBuilder();
            builder.Append("(");
            builder.Append("?" + string.Join(",?", fieldNameList));
            builder.Append(")");

            return builder.ToString();
        }

        protected override string GetInsertValues<T>(T item, int index, ref IList<MySqlParameter> param)
        {
            var model = item as User;
            var obj = new object();
            var fieldList = EntityUtil.GetFieldList(model, ref obj);
            var fieldNameList = EntityUtil.GetFieldNameList(model);
            var paras = new List<MySqlParameter>();

            foreach (var fieldInfo in fieldList)
            {
                paras.Add(new MySqlParameter("?" + index + fieldInfo.Name.GetFieldName(), fieldInfo.GetValue(obj))); 
            }

            StringBuilder builder = new StringBuilder();
            builder.Append("(");
            for (int i = 0; i < fieldNameList.Count; i++) {
                var name = fieldNameList[i];
                builder.Append("?" + index + name);
                if (i < fieldNameList.Count) builder.Append(",");
            }
            builder.Append("?" + string.Join(",?", fieldNameList));
            builder.Append(")");

            return builder.ToString();
        }
    }
}