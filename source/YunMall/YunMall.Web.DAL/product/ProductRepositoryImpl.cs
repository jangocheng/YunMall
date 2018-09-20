using System.Collections.Generic;
using MySql.Data.MySqlClient;
using YunMall.Entity.db;
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
    }
}