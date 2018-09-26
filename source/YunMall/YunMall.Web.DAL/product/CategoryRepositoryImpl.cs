using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF.Common;
using DF.DBUtility.MySql;
using MySql.Data.MySqlClient;
using YunMall.Entity.db;
using YunMall.Entity.dbExt;
using YunMall.Web.IDAL.product;

namespace YunMall.Web.DAL.product {
    /// <summary>
    /// 经营类目仓储接口实现类
    /// </summary>
    public class CategoryRepositoryImpl : AbsBaseRepository, ICategoryRepository {
        public CategoryRepositoryImpl() : base("categorys")
        {
            Category model = new Category();
            base.Fields = "(" + base.JoinFieldStrings(model) + ")";
        }

        protected override string GetInsertFields()
        {
            return base.Fields;
        }

        protected override string GetInsertValues<T>(T item, out MySqlParameter[] param)
        {
            var model = item as Category;
            return base.JoinFieldValues(model, out param);
        }

        protected override string GetInsertValues<T>(T item, int index, ref IList<MySqlParameter> param)
        {
            var model = item as Category;
            return base.JoinFields(model, index);
        }

        /// <summary>
        /// 查询经营类目详细信息列表
        /// </summary>
        /// <returns></returns>
        public IList<CategoryDetail> QueryDetails() {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT t1.*,t2.categoryName AS parentName FROM categorys t1 ");
            builder.Append("LEFT JOIN categorys t2 ON t1.parentId = t2.cid; ");
            var dataSet = DBHelperMySql.Query(builder.ToString());
            return dataSet.ToList<CategoryDetail>();
        }

        /// <summary>
        /// 查询经营类目详细信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CategoryDetail QueryDetail(int value) {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT t1.*,t2.categoryName AS parentName FROM categorys t1 ");
            builder.Append($"LEFT JOIN categorys t2 ON t1.parentId = t2.cid WHERE t1.cid = {value} ");
            var dataSet = DBHelperMySql.Query(builder.ToString());
            var list = dataSet.ToList<CategoryDetail>();
            if (list == null || list.Count == 0) return null;
            return list.First();
        }
    }
}