using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using MySql.Data.MySqlClient;
using YunMall.Entity.db;
using YunMall.Web.IDAL.order;

namespace YunMall.Web.DAL.order {
    public class OrderRepositoryImpl : AbsBaseRepository, IOrderRepository {
        public OrderRepositoryImpl() : base("orders")
        {
            Orders model = new Orders();
            base.Fields = "(" + base.JoinFieldStrings(model) + ")";
        }

        protected override string GetInsertFields()
        {
            return base.Fields;
        }

        protected override string GetInsertValues<T>(T item, out MySqlParameter[] param)
        {
            var model = item as Orders;
            return base.JoinFieldValues(model, out param);
        }

        protected override string GetInsertValues<T>(T item, int index, out MySqlParameter[] param)
        {
            var model = item as Orders;
            return base.JoinFields(model, index, out param);

        }


        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="orders"></param>
        /// <param name="dictionary"></param>
        public void CreateOrder(Orders orders, ref IDictionary<string, DbParameter[]> dictionary) {
            var random = Guid.NewGuid().ToString().Replace("-","").Trim();

            StringBuilder builder = new StringBuilder();
            builder.Append(
                "INSERT INTO `orders` (`orderId`, `pid`, `pname`, `sid`, `sname`, `uid`, `uname`, `tradeType`, `amount`,`addTime`, `editTime`, `remark`) VALUES (");
            builder.Append("?" + random + "orderId,");
            builder.Append("?" + random + "pid,");
            builder.Append("(SELECT productName FROM products WHERE pid = ?" + random + "pid LIMIT 1),");
            builder.Append("(SELECT sid FROM products WHERE pid = ?" + random + "pid LIMIT 1),");
            builder.Append("(SELECT username FROM users WHERE uid = (SELECT sid FROM products WHERE pid = ?" + random + "pid LIMIT 1)),");
            builder.Append("?" + random + "uid,");
            builder.Append("(SELECT username FROM users WHERE uid = ?" + random + "uid LIMIT 1),");
            builder.Append("?" + random + "tradeType,");
            builder.Append("?" + random + "amount,");
            builder.Append("NOW(),");
            builder.Append("NOW(),");
            builder.Append("?" + random + "remark)");


            var paras = new List<MySqlParameter>();
            paras.Add(new MySqlParameter("?" + random + "orderId", orders.OrderId));
            paras.Add(new MySqlParameter("?" + random + "pid", orders.Pid));
            paras.Add(new MySqlParameter("?" + random + "uid", orders.Uid));
            paras.Add(new MySqlParameter("?" + random + "tradeType", orders.TradeType));
            paras.Add(new MySqlParameter("?" + random + "amount", orders.Amount));
            paras.Add(new MySqlParameter("?" + random + "remark", orders.Remark));

            dictionary.Add(builder.ToString(), paras.ToArray());
        }
    }
}