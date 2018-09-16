using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DF.Common;
using DF.DBUtility.MySql;
using YunMall.Entity.db;
using YunMall.Web.IDAL.user;

namespace YunMall.Web.DAL.user
{
    public class PermissionRepositoryImpl : AbsBaseRepository, IPermissionRepository
    {
        public PermissionRepositoryImpl() : base("permissions")
        {
            base.Fields = @"*";
        }

        protected override string GetInsertFields()
        {
            return base.Fields;
        }

        protected override string GetInsertValues<T>(T item, out MySqlParameter[] param)
        {
            var model = item as Permission;
            return base.JoinFieldValues(model, out param);
        }

        protected override string GetInsertValues<T>(T item, int index, ref IList<MySqlParameter> param)
        {
            var model = item as Permission;
            return base.JoinFields(model, index);
        }


        #region 根据uid查询权限列表 韦德 2018年9月16日14:35:31

        /// <summary>
        /// 根据uid查询权限列表 韦德 2018年9月16日14:35:31
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public IList<Permission> SelectList(int uid) {
            string sql = $"SELECT * FROM permissions WHERE permissionId IN( SELECT permissionList FROM `permission_relations` WHERE uid = {uid} )";
            return DBHelperMySql.Query(sql).Tables[0].ToList<Permission>();
        }

        #endregion
    }
}
