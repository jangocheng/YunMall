/// <summary>
/// 程序说明：SQLSERVER 数据库操作公共类
/// 各部分之间的关系及本文件与其它文件关系等。   实现数据的逻辑操作
/// 建立者：
/// 建立日期：2014年12月17日。
/// 修改记录：
/// 修改者：
/// 修改内容：给成了抽象类的静态方法
/// 说明该次修改的程序内容。
/// </summary>

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Web;
using System.Text;
using System.Configuration;

namespace DF.DBUtility
{
    /// <summary>
    /// 数据访问抽象基础类
    /// </summary>
    public abstract class DbHelperMsSQL1
    {
        /* 
         * 数据库连接字符串。
         * 可以用任意的方法，比如web.config或者自定义xml来配置，或者写死在里面
        */
        //public static string connStr;

        //数据库连接字符串(web.config来配置)，多数据库可使用DbHelperSQLP来实现.
        public static string connStr =  PubConstant.GetConfigString("ConnectionString");

        public DbHelperMsSQL1() { }
        /// <summary>
        /// 获取不同的数据库连接字符串
        /// </summary>
        /// <param name="strConn">web.config中的Key</param>
        public DbHelperMsSQL1(string strConn)
        {
            connStr = ConfigurationManager.AppSettings[strConn].ToString();
        }

        #region SqlBulkCopy——批量导数据
        /// <summary>
        /// 批量将DataTable中的内容导入到数据库。dt的结构必须与数据库的表一致。
        /// </summary>
        /// <param name="dt">导入的DataTable</param>
        /// <param name="TableName">数据库中的表名</param>
        public static void SqlBulkCopy(DataTable dt, string TableName)
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    using (SqlBulkCopy bcp = new SqlBulkCopy(connection))
                    {
                        bcp.DestinationTableName = TableName;
                        bcp.WriteToServer(dt);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
        #endregion

        #region 返回查询结果是否有数据
        /// <summary>
        /// 返回一个查询结果，是否有数据
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        public static bool Exists(string sql)
        {
            bool rebool;
            try
            {
                using (SqlConnection myconn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, myconn))
                    {
                        myconn.Open();
                        using (SqlDataReader myReader = cmd.ExecuteReader())
                        {
                            if (myReader.Read())
                            {
                                rebool = true;
                            }
                            else
                            {
                                rebool = false;
                            }
                            return rebool;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "\r\n" + sql);
            }
        }
        /// <summary>
        /// 返回一个查询结果，是否有数据
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns></returns>
        public bool Exists(string sql, params SqlParameter[] cmdParms)
        {
            bool rebool;
            try
            {
                using (SqlConnection myconn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        PrepareCommand(cmd, myconn, null, sql, cmdParms);
                        SqlDataReader myReader = cmd.ExecuteReader();
                        if (myReader.Read())
                        {
                            rebool = true;
                        }
                        else
                        {
                            rebool = false;
                        }
                        return rebool;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region ExecuteNonQuery
        /// <summary>
        /// 执行无返回结果的SQL语句，返回执行是否成功
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static bool ExecuteNonQuery(string sql)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 执行无返回结果的SQL语句，返回执行是否成功
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns></returns>
        public static bool ExecuteNonQuery(string sql, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, sql, cmdParms);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }
        #endregion

        #region ExecuteScalar
        /// <summary>
        /// 执行一条计算查询结果语句，返回第一行第一列（object）。
        /// </summary>
        /// <param name="sql">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object ExecuteScalar(string sql)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回第一行第一列（object）。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, sql, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
        }
        #endregion

        #region ExecuteDataSet



        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataSet(string SQLString, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            strSql.Append("order by T." + orderby);
            strSql.Append(")AS Row, T.*  from (" + SQLString + ") T ");
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperMsSQL1.ExecuteDataSet(strSql.ToString());
        }



        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string sql, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, sql, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    return ds;
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataSet(string sql)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(sql, connection);
                    command.Fill(ds, "ds");
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message + sql);
                }
                return ds;
            }
        }

        #endregion

        #region ExecuteReader
        /// <summary>
        /// 执行查询语句，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string sql)
        {
            SqlConnection connection = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(sql, connection);
            try
            {
                connection.Open();
                SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return myReader;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 执行查询语句，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sql, params SqlParameter[] cmdParms)
        {
            SqlConnection connection = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, connection, null, sql, cmdParms);
                SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region 公共函数
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;
            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }
        #endregion

        #region 执行多条SQL语句，实现数据库事务。
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public static bool ExecuteSqlTran(List<String> SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    foreach (string sql in SQLStringList)
                    {
                        if (string.IsNullOrEmpty(sql) == false && sql != "")
                        {
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    return true;
                }
                catch
                {
                    tx.Rollback();
                    return false;
                }
            }
        }
        #endregion

        #region 执行多条SQL语句，实现数据库事务。
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public static bool ExecuteSqlTran(List<String> SQLStringList, params SqlParameter[] cmdParms)
        {
            string strSql = string.Empty;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    if (cmdParms != null)
                    {
                        foreach (SqlParameter parameter in cmdParms)
                        {
                            if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                                (parameter.Value == null))
                            {
                                parameter.Value = DBNull.Value;
                            }
                            cmd.Parameters.Add(parameter);
                        }
                    }
                    foreach (string sql in SQLStringList)
                    {
                        if (string.IsNullOrEmpty(sql) == false && sql != "")
                        {
                            strSql = strSql + sql;
                        }
                    }
                    cmd.CommandText = strSql;
                    cmd.ExecuteNonQuery();
                    tx.Commit();
                    return true;
                }
                catch
                {
                    tx.Rollback();
                    return false;
                }
            }
        }
        #endregion
    }
}