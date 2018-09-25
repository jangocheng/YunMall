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

        protected override string GetInsertValues<T>(T item, int index, ref IList<MySqlParameter> param)
        {
            var model = item as Product;
            return base.JoinFields(model, index);
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
    }
}