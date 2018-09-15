/// <summary>
/// 程序说明：通用DbHelper类 具有事务感知的ExecuteNonQuery
/// 依赖TransactionScope，transaction
/// 建立者：于法兴(初步可用，有待进一步测试，慎重选择使用)
/// 建立日期：2016年07月28日。
/// 修改记录：
/// 修改者：：修改日期时间：
/// 修改内容：说明该次修改的程序内容。
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Configuration;
using DF.DBUtility;
namespace DF.DBUtility
{
    public class DbHelper
    {
        public DbProviderFactory DbProviderFactory { get; private set; }
        public string ConnectionString { get; private set; }
        public virtual DbParameter BuildDbParameter(string name, object value)
        {
            DbParameter parameter = this.DbProviderFactory.CreateParameter();
            parameter.ParameterName = "@" + name;
            parameter.Value = value;
            return parameter;
        }

        public DbHelper(string cnnStringName)
        {
            var cnnStringSection = ConfigurationManager.ConnectionStrings[cnnStringName];
            this.DbProviderFactory = DbProviderFactories.GetFactory(cnnStringSection.ProviderName);
            this.ConnectionString = cnnStringSection.ConnectionString;
        }
        public static DbProviderFactory GetFactory(string cnnStringName)
        {
            var cnnStringSection = ConfigurationManager.ConnectionStrings[cnnStringName];
            return DbProviderFactories.GetFactory(cnnStringSection.ProviderName);
        }
        /// <summary>
        /// (初步可用，有待进一步测试，慎重选择使用)
        /// </summary>
        /// <param name="commandText">执行sql</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string commandText, IDictionary<string, object> parameters)
        {
            DbConnection connection = null;
            DbCommand command = this.DbProviderFactory.CreateCommand();
            DbTransaction dbTransaction = null;
            try
            {
                command.CommandText = commandText;
                parameters = parameters ?? new Dictionary<string, object>();
                foreach (var item in parameters)
                {
                    command.Parameters.Add(this.BuildDbParameter(item.Key, item.Value));
                }
                if (null != Transaction.Current)
                {
                    command.Connection = Transaction.Current.DbTransactionWrapper.DbTransaction.Connection;
                    command.Transaction = Transaction.Current.DbTransactionWrapper.DbTransaction;
                }
                else
                {
                    connection = this.DbProviderFactory.CreateConnection();
                    connection.ConnectionString = this.ConnectionString;
                    command.Connection = connection;
                    connection.Open();
                    if (Transaction.Current == null)
                    {
                        dbTransaction = connection.BeginTransaction();
                        command.Transaction = dbTransaction;
                    }
                }
                int result = command.ExecuteNonQuery();
                if (null != dbTransaction)
                {
                    dbTransaction.Commit();
                }
                return result;
            }
            catch
            {
                if (null != dbTransaction)
                {
                    dbTransaction.Rollback();
                }
                throw;
            }
            finally
            {
                if (null != connection)
                {
                    connection.Dispose();
                }
                if (null != dbTransaction)
                {
                    dbTransaction.Dispose();
                }
                command.Dispose();
            }
        }
    }
}