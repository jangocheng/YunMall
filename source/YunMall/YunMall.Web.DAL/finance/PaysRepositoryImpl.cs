using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using DF.Common;
using DF.DBUtility.MySql;
using MySql.Data.MySqlClient;
using YunMall.Entity.db;
using YunMall.Entity.dbExt;
using YunMall.Web.IDAL.finance;

namespace YunMall.Web.DAL.finance {
    public class PaysRepositoryImpl : AbsBaseRepository, IPaysRepository {
        public PaysRepositoryImpl() : base("pays")
        {
            Pays model = new Pays();
            base.Fields = "(" + base.JoinFieldStrings(model) + ")";
        }

        protected override string GetInsertFields()
        {
            return base.Fields;
        }

        protected override string GetInsertValues<T>(T item, out MySqlParameter[] param)
        {
            var model = item as Pays;
            return base.JoinFieldValues(model, out param);
        }

        protected override string GetInsertValues<T>(T item, int index, out MySqlParameter[] param)
        {
            var model = item as Pays;
            return base.JoinFields(model, index, out param);
        }


        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="type"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public IList<PaysDetail> SelectLimit(int page, string limit, int tradeType, int type, string beginTime, string endTime, string where)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT t1.*,t2.username,t3.* FROM pays t1 LEFT JOIN users t2 ON t1.fromUid = t2.uid ");
            builder.Append("LEFT JOIN pays t3 ON t1.toUid = t2.uid ");
            builder.AppendFormat($"WHERE {where} GROUP BY t1.payId ORDER BY t1.addTime DESC LIMIT {page},{limit}");
            var dataSet = DBHelperMySql.Query(builder.ToString());
            if (dataSet == null || dataSet.Tables.Count == 0) return null;
            return dataSet.Tables[0].ToList<PaysDetail>();
        }

        /// <summary>
        /// 插入交易流水
        /// </summary>
        /// <param name="payAccounts"></param>
        /// <param name="dictionary"></param>
        public void InsertAccounts(Pays payAccounts, ref IDictionary<string, DbParameter[]> dictionary) {
            var random = Guid.NewGuid().ToString().Replace("-", "");
            var paras = new List<MySqlParameter>();
            StringBuilder builder = new StringBuilder();
            builder.Append(
                "INSERT INTO `pays` (`payId`, `fromUid`, `fromName`, `toUid`, `toName`, `channelType`, `channelName`, `productType`, `productName`, `tradeType`, `tradeName`, `addTime`, `amount`, `systemRecordId`, `remark`, `status`) VALUES(");
            builder.Append("?"+ random + "PayId,");
            builder.Append("?" + random + "FromUid,");
            builder.Append("(SELECT username FROM users WHERE uid = ?" + random + "FromUid),");
            builder.Append("?" + random + "ToUid,");
            builder.Append("(SELECT username FROM users WHERE uid = ?" + random + "ToUid),");
            builder.Append("?" + random + "ChannelType,");
            builder.Append("(SELECT `value` FROM dictionarys WHERE dictionaryId = ?" + random + "ChannelType),");
            builder.Append("?" + random + "ProductType,");
            builder.Append("(SELECT `value` FROM dictionarys WHERE dictionaryId = ?" + random + "ProductType),");
            builder.Append("?" + random + "TradeType,");
            builder.Append("(SELECT `value` FROM dictionarys WHERE dictionaryId = ?" + random + "TradeType),");
            builder.Append("NOW(),");
            builder.Append("?" + random + "Amount,");
            builder.Append("?" + random + "SystemRecordId,");
            builder.Append("?" + random + "Remark,");
            builder.Append("?" + random + "Status");
            builder.Append(")");

            paras.Add(new MySqlParameter("?" + random + "PayId", payAccounts.PayId));
            paras.Add(new MySqlParameter("?" + random + "FromUid", payAccounts.FromUid));
            paras.Add(new MySqlParameter("?" + random + "ToUid", payAccounts.ToUid));
            paras.Add(new MySqlParameter("?" + random + "ChannelType", payAccounts.ChannelType));
            paras.Add(new MySqlParameter("?" + random + "ProductType", payAccounts.ProductType));
            paras.Add(new MySqlParameter("?" + random + "TradeType", payAccounts.TradeType));
            paras.Add(new MySqlParameter("?" + random + "Amount", payAccounts.Amount));
            paras.Add(new MySqlParameter("?" + random + "SystemRecordId", payAccounts.SystemRecordId));
            paras.Add(new MySqlParameter("?" + random + "Remark", payAccounts.Remark));
            paras.Add(new MySqlParameter("?" + random + "Status", payAccounts.Status));

            dictionary.Add(builder.ToString(), paras.ToArray());
        }

        /// <summary>
        /// 查询分页总数
        /// </summary>
        /// <param name="type"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public int SelectLimitCount(int type, string beginTime, string endTime, string where)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(t1.payId) FROM pays t1 ");
            builder.AppendFormat($"WHERE {where}");
            var result = DBHelperMySql.GetSingle(builder.ToString());
            return Convert.ToInt32(result);
        }
    }
}