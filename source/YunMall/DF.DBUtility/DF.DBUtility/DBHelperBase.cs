using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DF.DBUtility
{
    /// <summary>
    /// DBHelper基类
    /// </summary>
    public class DBHelperBase
    {

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        protected static readonly string connectionString = SqlConfigHelper.GetConfig("SqlHelperConfig").ConnectionString;

        /// <summary>
        /// 构建Command对象
        /// </summary>
        /// <param name="cmd">commond对象</param>
        /// <param name="conn">connection对象</param>
        /// <param name="trans">transaction对象</param>
        /// <param name="cmdText">sql字符串</param>
        /// <param name="cmdParms">参数</param>
        protected static void PrepareCommand(DbCommand cmd, DbConnection conn, DbTransaction trans, string cmdText, DbParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            if (cmdParms != null)
            {
                foreach (DbParameter parm in cmdParms)
                {
                    if (parm != null)
                    {
                        cmd.Parameters.Add(parm);
                    }
                }
            }
        }


    }
}
