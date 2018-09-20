using System.Collections.Generic;
using MySql.Data.MySqlClient;
using YunMall.Entity.db;
using YunMall.Web.IDAL;
using YunMall.Web.IDAL.user;

namespace YunMall.Web.DAL.user {
    public class PermissionRelationRepositoryImpl : AbsBaseRepository, IPermissionRelationRepository {
        public PermissionRelationRepositoryImpl() : base("permission_relations")
        {
            PermissionRelation permissionRelations = new PermissionRelation();
            base.Fields = "(" + base.JoinFieldStrings(permissionRelations) + ")";
        }

        protected override string GetInsertFields()
        {
            return base.Fields;
        }

        protected override string GetInsertValues<T>(T item, out MySqlParameter[] param)
        {
            var model = item as PermissionRelation;
            return base.JoinFieldValues(model, out param);
        }

        protected override string GetInsertValues<T>(T item, int index, ref IList<MySqlParameter> param)
        {
            var model = item as PermissionRelation;
            return base.JoinFields(model, index);
        }
    }
}