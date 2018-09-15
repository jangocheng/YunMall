/// <summary>
/// 程序说明：自定义事务类
/// 模拟TransactionScope
/// 建立者：于法兴
/// 建立日期：2016年07月28日。
/// 修改记录：
/// 修改者：：修改日期时间：
/// 修改内容：说明该次修改的程序内容。
/// </summary>
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace DF.DBUtility
{
    public class TransactionScope: IDisposable
    {
        private Transaction transaction = Transaction.Current;
        public bool Completed { get; private set; }

        public TransactionScope(string connectionStringName, IsolationLevel isolationLevel = IsolationLevel.Unspecified,
            Func<string, DbProviderFactory> getFactory = null)
        {
            if (null == transaction)
            {
                if (null == getFactory)
                {
                    getFactory = cnnstringName => DbHelper.GetFactory(cnnstringName);
                }
                DbProviderFactory factory = getFactory(connectionStringName);
                DbConnection connection = factory.CreateConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
                connection.Open();
                DbTransaction dbTransaction = connection.BeginTransaction(isolationLevel);
                Transaction.Current = new CommittableTransaction(dbTransaction);
            }
            else
            {
                Transaction.Current = transaction.DependentClone();
            }           
        }

        public void Complete()
        {
            this.Completed = true;
        }
        public void Dispose()
        {
            Transaction current = Transaction.Current;
            Transaction.Current = transaction;
            if (!this.Completed)
            {
                current.Rollback();
            }
            CommittableTransaction committableTransaction = current as CommittableTransaction;
            if (null != committableTransaction)
            {
                if (this.Completed)
                {
                    committableTransaction.Commit();
                }
                committableTransaction.Dispose();
            }
        }
    }
}
