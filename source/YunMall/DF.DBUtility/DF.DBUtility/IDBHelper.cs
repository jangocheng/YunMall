// ========================================描述信息========================================
// 数据库公共类库方法接口
// 
// 
// 
// ========================================创建信息========================================
// 创建人：   lupeng
// 创建时间： 2016年11月07日
// ========================================变更信息========================================
// 日期          版本        修改人         描述
// 
// ========================================================================================
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DF.DBUtility
{
    public interface IDBHelper
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        string connectionStr { get; set; }

        /// <summary>
        /// 执行Insert、Update、Delete操作
        /// </summary>
        /// <param name="sqlStr">sql语句</param>
        /// <param name="param">sql参数</param>
        /// <returns>影响数据库的行数</returns>
        int ExcuteNonQuery(string sqlStr, params DbParameter[] param);

        /// <summary>
        /// 执行SELECT语句
        /// </summary>
        /// <param name="sqlStr">sql语句</param>
        /// <param name="param">sql参数</param>
        /// <returns>查询结果</returns>
        DataSet Query(string sqlStr, params DbParameter[] param);

        /// <summary>
        /// 执行SELECT语句，
        /// 返回查询结果中第一行第一列的数据，
        /// 如果第一行存在但是第一列的值为空，返回DBNull，
        /// 如果不存在第一行，返回null。
        /// </summary>
        /// <param name="sqlStr">sql语句</param>
        /// <param name="param">sql参数</param>
        /// <returns>查询结果</returns>
        object ExecuteScalar(string sqlStr, params DbParameter[] param);

        /// <summary>
        /// 执行存储过程,返回DataSet
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="param">存储过程参数</param>
        /// <returns>DataSet</returns>
        DataSet RunProceReturnDataSet(string storedProcName, params DbParameter[] param);


    }
}
