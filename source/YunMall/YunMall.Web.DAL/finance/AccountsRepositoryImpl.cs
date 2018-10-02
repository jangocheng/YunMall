using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using DF.Common;
using DF.DBUtility.MySql;
using MySql.Data.MySqlClient;
using YunMall.Entity.db;
using YunMall.Entity.dbExt;
using YunMall.Web.IDAL.finance;

namespace YunMall.Web.DAL.finance {
    public class AccountsRepositoryImpl : AbsBaseRepository, IAccountsRepository {
        public AccountsRepositoryImpl() : base("accounts")
        {
            Accounts model = new Accounts();
            base.Fields = "(" + base.JoinFieldStrings(model) + ")";
        }

        protected override string GetInsertFields()
        {
            return base.Fields;
        }

        protected override string GetInsertValues<T>(T item, out MySqlParameter[] param)
        {
            var model = item as Accounts;
            return base.JoinFieldValues(model, out param);
        }

        protected override string GetInsertValues<T>(T item, int index, out MySqlParameter[] param)
        {
            var model = item as Accounts;
            return base.JoinFields(model, index, out param);
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
        public IList<Accounts> SelectLimit(int page, string limit, int state, string beginTime, string endTime, string where)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT t1.* FROM accounts t1 ");
            builder.AppendFormat($"WHERE {where} GROUP BY t1.accountsId ORDER BY t1.addTime DESC LIMIT {page},{limit}");
            var dataSet = DBHelperMySql.Query(builder.ToString());
            if (dataSet == null || dataSet.Tables.Count == 0) return null;
            return dataSet.Tables[0].ToList<Accounts>();
        }

        /// <summary>
        /// 查询收入支出金额
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public IDictionary<string, string> SelectAmount(int uid) {
            StringBuilder builder = new StringBuilder();
            builder.Append($"SELECT (SELECT SUM(amount) FROM pays WHERE fromUid != {uid}) AS incomeAmount, ");
            builder.AppendFormat($"(SELECT SUM(amount) FROM pays WHERE fromUid = {uid}) AS expendAmount FROM pays LIMIT 1");
            var dataSet = DBHelperMySql.Query(builder.ToString());
            if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0) return null;
            var convertDictionary = dataSet.Tables[0].Rows.Cast<DataRow>().ToDictionary(x => x[0].ToString(), x => x[1].ToString());
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("incomeAmount", Convert.ToString(convertDictionary.First().Key));
            dictionary.Add("expendAmount", Convert.ToString(convertDictionary.First().Value));
            return dictionary;
        }

        /// <summary>
        /// 批量插入流水
        /// </summary>
        /// <param name="currentAccounts"></param>
        /// <param name="dictionary"></param>
        public void BatchInsertAccounts(IList<Accounts> currentAccounts,
            ref IDictionary<string, DbParameter[]> dictionary) {
            var paras = new List<MySqlParameter>();
            StringBuilder builder = new StringBuilder();
            builder.Append(
                "INSERT INTO `accounts` (`payId`, `tradeAccountId`, `tradeAccountName`, `accountsType`, `currency`, `amount`, `beforeBalance`, `afterBalance`, `addTime`, `remark`) VALUES");
            currentAccounts.ToList().ForEach(item => {
                var random = Guid.NewGuid().ToString().Replace("-", "");

                builder.Append("(?" + random + "payId,");
                builder.Append("?" + random + "tradeAccountId,");
                builder.Append("(SELECT username FROM users WHERE uid = ?" + random + "tradeAccountId),");
                builder.Append("?" + random + "accountsType,");
                builder.Append("?" + random + "currency,");
                builder.Append("?" + random + "amount,");
                builder.Append("(SELECT balance FROM wallets WHERE userId = ?" + random + "tradeAccountId),");
                if (item.AccountsType == 1) {
                    builder.Append("(SELECT balance + ?" + random + "amount FROM wallets WHERE userId = ?" + random + "tradeAccountId),");
                }
                else if(item.AccountsType == 2) {
                    builder.Append("(SELECT balance - ?" + random + "amount FROM wallets WHERE userId = ?" + random + "tradeAccountId),");
                }
                builder.Append("NOW(),");
                builder.Append("?" + random + "remark),");
                paras.Add(new MySqlParameter("?" + random + "payId", item.PayId));
                paras.Add(new MySqlParameter("?" + random + "tradeAccountId", item.TradeAccountId));
                paras.Add(new MySqlParameter("?" + random + "accountsType", item.AccountsType));
                paras.Add(new MySqlParameter("?" + random + "currency", item.Currency));
                paras.Add(new MySqlParameter("?" + random + "amount", item.Amount));
                paras.Add(new MySqlParameter("?" + random + "remark", item.Remark));

            });
            var sql = builder.ToString().Substring(0, builder.ToString().Length - 1);
            dictionary.Add(sql, paras.ToArray()); 

        }

        public int SelectLimitCount(int state, string beginTime, string endTime, string where)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(t1.accountsId) FROM accounts t1 ");
            builder.AppendFormat($"WHERE {where}");
            var result = DBHelperMySql.GetSingle(builder.ToString());
            return Convert.ToInt32(result);
        }

         
    }
}