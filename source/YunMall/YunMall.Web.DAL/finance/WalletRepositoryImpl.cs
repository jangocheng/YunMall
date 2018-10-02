using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using DF.Common;
using DF.DBUtility.MySql;
using MySql.Data.MySqlClient;
using YunMall.Entity.db;
using YunMall.Web.IDAL.finance;

namespace YunMall.Web.DAL.finance {
    public class WalletRepositoryImpl : AbsBaseRepository, IWalletRepository
    {
        public WalletRepositoryImpl() : base("wallets")
        {
            Wallet model = new Wallet();
            base.Fields = "(" + base.JoinFieldStrings(model) + ")";
        }

        /// <summary>
        /// 刷新钱包账目-出账
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="amount"></param>
        /// <param name="version"></param>
        /// <param name="dictionary"></param>
        public void OutAccounts(int uid, double amount, int version, ref IDictionary<string, DbParameter[]> dictionary) {
            var random = Guid.NewGuid().ToString().Replace("-", "").Trim();
            StringBuilder builder = new StringBuilder();
            builder.Append(
                "UPDATE wallets SET balance = balance - ?" + random + "amount, updateTime = NOW(), version = version + 1 ");
            builder.Append(" WHERE `userId`= ?" + random +  "uid AND balance > 0 AND (balance - ?" + random + "amount) > 0 AND version = ?" + random + "version ");
            var paras = new List<MySqlParameter>
            {
                new MySqlParameter("?" + random + "amount", amount),
                new MySqlParameter("?" + random + "uid", uid),
                new MySqlParameter("?" + random + "version", version)
            };
            dictionary.Add(builder.ToString(), paras.ToArray());
        }

        /// <summary>
        /// 刷新钱包账目-进账
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="amount"></param>
        /// <param name="version"></param>
        /// <param name="dictionary"></param>
        public void PutAccounts(int uid, double amount, int version, ref IDictionary<string, DbParameter[]> dictionary)
        {
            var random = Guid.NewGuid().ToString().Replace("-", "").Trim();
            StringBuilder builder = new StringBuilder();
            builder.Append(
                "UPDATE wallets SET balance = balance + ?" + random + "amount, updateTime = NOW(), version = version + 1 ");
            builder.Append(" WHERE `userId`= ?" + random + "uid AND version = ?" + random  + "version ");
            var paras = new List<MySqlParameter>
            {
                new MySqlParameter("?" + random + "amount", amount),
                new MySqlParameter("?" + random + "uid", uid),
                new MySqlParameter("?" + random + "version", version)
            };
            dictionary.Add(builder.ToString(), paras.ToArray());
        }

        /// <summary>
        /// 查询钱包信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public Wallet SelectById(int uid) {
            var dataSet = DBHelperMySql.Query("SELECT * FROM wallets WHERE userId = " + uid);
            var list = dataSet.ToList<Wallet>();
            if (list == null || list.Count == 0) return null;
            return list.First();
        }

        protected override string GetInsertFields()
        {
            return base.Fields;
        }

        protected override string GetInsertValues<T>(T item, out MySqlParameter[] param)
        {
            var model = item as Wallet;
            return base.JoinFieldValues(model, out param);
        }

        protected override string GetInsertValues<T>(T item, int index, out MySqlParameter[] param)
        {
            var model = item as Wallet;
            return base.JoinFields(model, index, out param);
        }
    }
}