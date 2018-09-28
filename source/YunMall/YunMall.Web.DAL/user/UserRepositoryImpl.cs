using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF.Common;
using DF.DBUtility.MySql;
using MySql.Data.MySqlClient;
using YunMall.Entity.db;
using YunMall.Entity.dbExt;
using YunMall.Utility.joiner;
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

        /// <summary>
        /// 查询用户财务详情
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public UserFinanceDetail SelectFinanceDetail(int uid) {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT t1.*,t2.* FROM users t1 ");
            builder.Append("LEFT JOIN wallets t2 ON t1.uid = t2.userId ");
            builder.Append("WHERE uid = " + uid);
            var dataSet = DBHelperMySql.Query(builder.ToString());
            var userFinanceDetails = dataSet.ToList<UserFinanceDetail>();
            if (userFinanceDetails == null || userFinanceDetails.Count == 0) return null;
            return userFinanceDetails.First();
        }

        /// <summary>
        /// 查询多个用户的财务详情
        /// </summary>
        /// <param name="uids"></param>
        /// <returns></returns>
        public IList<UserFinanceDetail> SelectFinanceDetails(int[] uids) {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT t1.*,t2.* FROM users t1 ");
            builder.Append("LEFT JOIN wallets t2 ON t1.uid = t2.userId ");
            builder.Append($"WHERE uid IN ({uids.AsJoin()}) ");
            var dataSet = DBHelperMySql.Query(builder.ToString());
            var userFinanceDetails = dataSet.ToList<UserFinanceDetail>();
            if (userFinanceDetails == null || userFinanceDetails.Count == 0) return null;
            return userFinanceDetails;
        }
    }
}