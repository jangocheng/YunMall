// ========================================描述信息========================================
// 数据库公共类库----MySql
// 
// 
// 
// ========================================创建信息========================================
// 创建人：   
// 创建时间： 2017年02月06日
// ========================================变更信息========================================
// 日期          版本        修改人         描述
// 
// ========================================================================================
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Collections;
using DF.Log;
using System.Text;

namespace DF.DBUtility.MySql
{
    /// <summary>
    /// MySql数据库访问帮助类
    /// </summary>
    public class DBHelperMySql : DBHelperBase
    {
        /// <summary>
        /// 
        /// </summary>
        public static string connectionString { get { return DBHelperBase.connectionString; } }
        #region 是否存在

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static bool Exists(string strSql, params MySqlParameter[] cmdParms)
        {
            object obj = GetSingle(strSql, cmdParms);

            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region  执行一条计算查询结果语句，返回查询结果（object）

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(SQLString, connection))
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
                        connection.Close();
                        throw new Exception(e.Message);
                    }
                }
            }
        }

        #region 王亚囡
        public static DataSet GetList(string tableName, string columns, string strWhere, string orderBy, int pageIndex, int pageSize, out int totalCount, out double totalPage)
        {
            throw new NotImplementedException();
        }
        #endregion

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <param name="cmdParms">参数</param>      
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, params MySqlParameter[] cmdParms)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
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
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
        }

        #endregion

        #region 执行SQL语句，返回影响的记录数  不含参数--
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExcuteNonQuery(string SQLString)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlTransaction trans = connection.BeginTransaction())
                {
                    using (MySqlCommand cmd = new MySqlCommand(SQLString, connection))
                    {
                        try
                        {
                            cmd.CommandTimeout = 600;
                            int rows = cmd.ExecuteNonQuery();
                            trans.Commit();
                            return rows;
                        }
                        catch (Exception E)
                        {
                            trans.Rollback();
                            connection.Close();
                            throw new Exception(E.Message);
                        }
                    }
                }
            }
        }
        #endregion

        #region  执行Insert、Update、Delete操作--
        /// <summary>
        /// 执行Insert、Update、Delete操作
        /// </summary>
        /// <param name="sqlStr">sql语句</param>
        /// <param name="param">sql参数</param>
        /// <returns>影响数据库的行数</returns>
        public static int ExcuteNonQuery(string sqlStr, params DbParameter[] param)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlTransaction trans = connection.BeginTransaction())
                {
                    using (MySqlCommand cmd = new MySqlCommand(sqlStr, connection))
                    {
                        try
                        {
                            PrepareCommand(cmd, connection, null, sqlStr, param);
                            cmd.CommandType = CommandType.Text;
                            trans.Commit();
                            return cmd.ExecuteNonQuery();
                        }
                        catch (Exception E)
                        {
                            trans.Rollback();
                            connection.Close();
                            throw new Exception(E.Message);
                        }
                    }
                }
            }
        }
        #endregion

        #region 执行Insert语句，返回自增Id
        /// <summary>
        /// 执行Insert语句，返回自增Id
        /// </summary>
        /// <param name="sqlStr">sql</param>
        /// <param name="param">参数</param>
        /// <param name="pk">自增主键</param>
        /// <returns>影响数据库行数</returns>
        public static int ExcuteInsertReturnId(string sqlStr, out long pk, params DbParameter[] param)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlTransaction trans = connection.BeginTransaction())
                {
                    using (MySqlCommand cmd = new MySqlCommand(sqlStr, connection))
                    {
                        try
                        {
                            PrepareCommand(cmd, connection, null, sqlStr, param);
                            cmd.CommandType = CommandType.Text;
                            int result = cmd.ExecuteNonQuery();
                            pk = cmd.LastInsertedId;
                            trans.Commit();
                            return result;
                        }
                        catch (Exception e)
                        {
                            trans.Rollback();
                            throw e;
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
            }
        }

        #endregion

        #region  执行多条SQL语句，实现数据库事务--ArrayList--
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public static void ExecuteSqlTran(ArrayList SQLStringList)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                MySqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
                catch (System.Data.OleDb.OleDbException E)
                {
                    tx.Rollback();
                    throw new Exception(E.Message);
                }
            }
        }
        #endregion

        #region 执行多条SQL语句，实现数据库事务--HashTable--
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的MySqlParameter[]）</param>
        public static void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            MySqlParameter[] cmdParms = (MySqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        DF.Log.MyLog.Debug("ERROR", "DBUtility", "ERROR", ex);
                        throw;
                    }
                }
            }
        }
        #endregion


        #region 执行多条SQL语句，实现数据库事务--IList<SQLEntity>--
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="list">SQLEntity的IList</param>
        public static bool ExecuteSqlTran(IList<SQLEntity> list,out int index)
        {
            index = -1;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    try
                    {
                        //循环
                        foreach (var item in list)
                        {
                            string cmdText = item.Sql;
                            MySqlParameter[] cmdParms = (MySqlParameter[])item.Parameter;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            if (val <= 0)
                            {
                                index = list.IndexOf(item);
                                trans.Rollback();
                                return false;
                            }                    
                        }
                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
        #endregion

        #region 执行多条SQL语句，实现乐观锁--数据库事务--HashTable--
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的MySqlParameter[]）</param>
        public static bool ExecuteSqlTranLock(Hashtable SQLStringList)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            MySqlParameter[] cmdParms = (MySqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            #region 此段代码任何人勿动
                            if (val <= 0)
                            {
                                trans.Rollback();
                                return false;
                            }
                            #endregion
                            cmd.Parameters.Clear();
                        }

                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
        #endregion

        #region 执行多条SQL语句，实现乐观锁--数据库事务--Dictionary--
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的MySqlParameter[]）</param>
        public static bool ExecuteSqlTranLock(IDictionary<string, DbParameter[]> SQLStringList) {
            using (MySqlConnection conn = new MySqlConnection(connectionString)) {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction()) {
                    MySqlCommand cmd = new MySqlCommand();
                    try {
                        //循环
                        foreach (var myDE in SQLStringList) {
                            string cmdText = myDE.Key.ToString();
                            MySqlParameter[] cmdParms = (MySqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            #region 此段代码任何人勿动
                            if (val <= 0) {
                                trans.Rollback();
                                return false;
                            }
                            #endregion
                            cmd.Parameters.Clear();
                        }

                        trans.Commit();
                        return true;
                    } catch (Exception ex) {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的MySqlParameter[]）</param>
        /// <returns></returns>
        public static bool ExecuteSqlTranLock(IDictionary<StringBuilder, DbParameter[]> SQLStringList) {
            using (MySqlConnection conn = new MySqlConnection(connectionString)) {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction()) {
                    MySqlCommand cmd = new MySqlCommand();
                    try {
                        //循环
                        foreach (var myDE in SQLStringList) {
                            string cmdText = myDE.Key.ToString();
                            MySqlParameter[] cmdParms = (MySqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            #region 此段代码任何人勿动
                            if (val <= 0) {
                                trans.Rollback();
                                return false;
                            }
                            #endregion
                            cmd.Parameters.Clear();
                        }

                        trans.Commit();
                        return true;
                    } catch (Exception ex) {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        #endregion

        #region 执行多条SQL语句，实现数据库事务--Dictionary--
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。yfx
        /// </summary>
        /// <param name="SQLStringList">SQL语句的字典序集合（key为sql语句，value是该语句的MySqlParameter[]）</param>
        public static void ExecuteSqlTran(IDictionary<string, DbParameter[]> SQLStringList)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    try
                    {
                        //循环
                        foreach (var myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            MySqlParameter[] cmdParms = (MySqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            MyLog.Info("shangjia--UpdatCheckPass_测试影响行数", val);
                            cmd.Parameters.Clear();
                        }

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        MyLog.Info("shangjia--UpdatCheckPass_错误", ex);
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
        #endregion

        #region 执行多条SQL语句，实现数据库事务--Dictionary--
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。yfx
        /// </summary>
        /// <param name="SQLStringList">SQL语句的字典序集合（key为sql语句，value是该语句的MySqlParameter[]）</param>
        public static void ExecuteSqlTran(IDictionary<string, DbParameter[]> SQLStringList, IsolationLevel  isolationLevel)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction(isolationLevel)) {
                    MySqlCommand cmd = new MySqlCommand();
                    try
                    {
                        //循环
                        foreach (var myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            MySqlParameter[] cmdParms = (MySqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            MyLog.Info("shangjia--UpdatCheckPass_测试影响行数", val);
                            cmd.Parameters.Clear();
                        }

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        MyLog.Info("shangjia--UpdatCheckPass_错误", ex);
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
        #endregion

        #region 执行SELECT语句，返回查询结果中第一行第一列的数据--
        /// <summary>
        /// 执行SELECT语句，
        /// 返回查询结果中第一行第一列的数据，
        /// 如果第一行存在但是第一列的值为空，返回DBNull，
        /// 如果不存在第一行，返回null。
        /// </summary>
        /// <param name="sqlStr">sql语句</param>
        /// <param name="param">sql参数</param>
        /// <returns>查询结果</returns>
        public static object ExecuteScalar(string sqlStr, params DbParameter[] param)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(sqlStr, connection))
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, sqlStr, param);
                        cmd.CommandType = CommandType.Text;
                        return cmd.ExecuteScalar();
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        #endregion

        #region  执行查询语句，返回DataSet，不含参数--

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    MySqlDataAdapter command = new MySqlDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }
        #endregion

        #region 执行SELECT语句返回的DataSet不会为null--
        /// <summary>
        /// 执行SELECT语句
        /// 返回的DataSet不会为null
        /// </summary>
        /// <param name="sqlStr">sql语句</param>
        /// <param name="param">sql参数</param>
        /// <returns>查询结果</returns>
        public static DataSet Query(string sqlStr, params DbParameter[] param)
        {
            DataSet ds = new DataSet();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(sqlStr, connection))
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, sqlStr, param);
                        cmd.CommandType = CommandType.Text;
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        adapter.Fill(ds);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return ds;
        }
        #endregion

        #region 执行存储过程返回DataSet--
        /// <summary>
        /// 执行存储过程,返回DataSet
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="param">存储过程参数</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProceReturnDataSet(string storedProcName, params DbParameter[] param)
        {
            DataSet ds = new DataSet();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(storedProcName, connection))
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, storedProcName, param);
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        adapter.Fill(ds);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return ds;
        }
        #endregion

        #region 执行查询语句，返回MySqlDataReader--
        /// <summary>
        /// 执行查询语句，返回MySqlDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>MySqlDataReader</returns>
        public static MySqlDataReader ExecuteReader(string strSQL)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand(strSQL, connection);
            try
            {
                connection.Open();
                MySqlDataReader myReader = cmd.ExecuteReader();
                return myReader;
            }
            catch (System.Data.OleDb.OleDbException e)
            {
                throw new Exception(e.Message);
            }

        }
        #endregion

        /// <summary>
        /// 执行存储过程，返回影响的行数		
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns></returns>
        public static void RunProcedure(string storedProcName, out int rowsAffected, params MySqlParameter[] parameters)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
                rowsAffected = command.ExecuteNonQuery();
                //result = (int)command.Parameters["ReturnValue"].Value;
                //Connection.Close();
            }
        }

        /// <summary>
        /// 构建 MySqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>MySqlCommand</returns>
        private static MySqlCommand BuildQueryCommand(MySqlConnection connection, string storedProcName, params MySqlParameter[] parameters)
        {
            MySqlCommand command = new MySqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
            {
                foreach (MySqlParameter parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="sqlList">sql语句</param>
        /// <param name="param">参数</param>
        /// <returns>影响函数</returns>
        public static void AddBatch(List<string> sqlList, List<MySqlParameter> param)
        {
            //使用事务分批提交
            using (MySqlConnection conn = new MySqlConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.Parameters.AddRange(param.ToArray());
                MySqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    for (int n = 0; n < sqlList.Count; n++)
                    {
                        string strsql = sqlList[n];
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }

                        //500条提交一次事务
                        //if ((n > 0 && (n % 500 == 0 || n == sqlList.Count - 1)) || (n == 0 && sqlList.Count == 1))
                        //{
                        //    tx.Commit();
                        //    tx = conn.BeginTransaction();
                        //}
                    }
                    tx.Commit();
                }
                catch (System.Data.SqlClient.SqlException E)
                {
                    tx.Rollback();
                    throw new Exception(E.Message);
                }
            }
        }

        #region 执行多条SQL语句，实现数据库事务。yfx(顺序执行sql)
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。yfx
        /// </summary>
        /// <param name="SQLStringList">SQL语句的字典序集合（key为sql语句，value是该语句的MySqlParameter[]）</param>
        public static void ExecuteSqlTran(IDictionary<string, MySqlParameter[]> SQLStringList)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    try
                    {
                        //循环
                        foreach (var myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            MySqlParameter[] cmdParms = (MySqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
        #endregion

    }
}
