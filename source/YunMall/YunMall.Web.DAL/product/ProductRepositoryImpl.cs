using System;
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
    public class ProductRepositoryImpl : AbsBaseRepository, IProductRepository{
        public ProductRepositoryImpl() : base("products")
        {
            Product product = new Product();
            base.Fields = "(" + base.JoinFieldStrings(product) + ")";
        }

        protected override string GetInsertFields()
        {
            return base.Fields;
        }

        protected override string GetInsertValues<T>(T item, out MySqlParameter[] param)
        {
            var model = item as Product;
            return base.JoinFieldValues(model, out param);
        }

        protected override string GetInsertValues<T>(T item, int index, out MySqlParameter[] param)
        {
            var model = item as Product;
            return base.JoinFields(model, index, out param);

        }

        /// <summary>
        /// 查询商品详情 韦德 2018年9月25日15:39:40
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ProductDetail QueryDetail(int value) {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT t1.*,t2.roleId,t2.level,t3.* FROM products t1 ");
            builder.Append("LEFT JOIN users t2 ON t1.sid = t2.uid ");
            builder.Append("LEFT JOIN permissions t3 ON t2.roleId = t3.permissionId ");
            builder.Append($"WHERE pid = {value}; ");
            var dateSet = DBHelperMySql.Query(builder.ToString());
            if (dateSet == null || dateSet.Tables.Count <= 0) return null;
            return dateSet.Tables[0].ToList<ProductDetail>().First();
        }


        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="state"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<ProductDetail> SelectLimit(int page, string limit, int state, string beginTime, string endTime, string where)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT t1.*, t2.*, t4.*, (SELECT permissionId FROM permissions WHERE permissionId IN(t3.permissionList)), (SELECT roleName FROM permissions WHERE permissionId IN(t3.permissionList)) FROM products t1 ");
            builder.Append("LEFT JOIN users t2 ON t1.sid = t2.uid ");
            builder.Append("LEFT JOIN permission_relations t3 ON t1.sid = t3.uid ");
            builder.Append("LEFT JOIN categorys t4 ON t1.categoryId = t4.cid ");
            builder.AppendFormat($"WHERE {where} GROUP BY t1.pid ORDER BY t1.addTime DESC LIMIT {page},{limit}");
            var dataSet = DBHelperMySql.Query(builder.ToString());
            if (dataSet == null || dataSet.Tables.Count == 0) return null;
            return dataSet.Tables[0].ToList<ProductDetail>();
        }

        /// <summary>
        /// 查询分页总数
        /// </summary>
        /// <param name="state"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public int SelectLimitCount(int state, string beginTime, string endTime, string where)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(t1.pid) FROM products t1 ");
            builder.Append("LEFT JOIN users t2 ON t1.sid = t2.uid ");
            builder.AppendFormat($"WHERE {where}");
            var result = DBHelperMySql.GetSingle(builder.ToString());
            return Convert.ToInt32(result);
        }
    }
}