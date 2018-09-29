using System.Collections;
using System.Collections.Generic;
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
        /// <param name="hash"></param>
        public void CreateOrder(Orders orders, ref Hashtable hash) {
            StringBuilder builder = new StringBuilder();
            builder.Append(
                "INSERT INTO `orders` (`orderId`, `pid`, `pname`, `sid`, `sname`, `uid`, `uname`, `tradeType`, `amount`,`addTime`, `editTime`, `remark`) VALUES (");
            builder.Append("?orderId,");
            builder.Append("?pid,");
            builder.Append("(SELECT productName FROM products WHERE pid = ?pid),");
            builder.Append("?sid,");
            builder.Append("(SELECT username FROM users WHERE uid = ?sid),");
            builder.Append("?uid,");
            builder.Append("(SELECT username FROM users WHERE uid = ?uid),");
            builder.Append("?amount,");
            builder.Append("NOW(),");
            builder.Append("NOW(),");
            builder.Append("?remark)");


            var paras = new List<MySqlParameter>();
            paras.Add(new MySqlParameter("?orderId", orders.OrderId));
            paras.Add(new MySqlParameter("?pid", orders.Pid));
            paras.Add(new MySqlParameter("?sid", orders.Sid));
            paras.Add(new MySqlParameter("?uid", orders.Uid));
            paras.Add(new MySqlParameter("?amount", orders.Amount));
            paras.Add(new MySqlParameter("?remark", orders.Remark));

            hash.Add(builder.ToString(), paras.ToArray());
        }
    }
}