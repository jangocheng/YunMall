// ========================================描述信息========================================
// 
// 数据访问层基类
// 
// ========================================创建信息========================================
// 创建人：   --
// 创建时间： 2017-02-04 17:55:55    	
// ========================================变更信息========================================
// 日期          版本        修改人         描述
// 
// ========================================================================================
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DF.DBUtility.MySql;
using System.Data.Common;
using MySql.Data.MySqlClient;
using DF.Common;
using YunMall.Entity.ModelView;
using YunMall.Web.DAL.utils;

namespace YunMall.Web.DAL
{
    public abstract class AbsBaseRepository
    {
        #region 初始化信息--
        public AbsBaseRepository()
        {

        }
        /// <summary>
        /// 表名
        /// </summary>
        protected string TableName { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public AbsBaseRepository(string tableName)
        {
            this.TableName = " " + tableName + " ";
        }

        public string Fields { get; set; }
        #endregion

        #region 查询返回List--
        /// <summary>
        /// 查询返回List
        /// </summary>
        /// <typeparam name="R">占位符</typeparam>
        /// <param name="strSql">Sql</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        protected IList<R> Query<R>(string strSql, MySqlParameter[] param)
        {
            return DBHelperMySql.Query(strSql, param).Tables[0].ToList<R>();
        }
        #endregion

        #region 查询返回List--
        /// <summary>
        /// 查询返回List
        /// </summary>
        /// <typeparam name="R">占位符</typeparam>
        /// <param name="strSql">Sql</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public IList<R> Query<R>(string strSql)
        {
            return DBHelperMySql.Query(strSql).Tables[0].ToList<R>();

        }
        #endregion

        #region Hashtable提交事务--
        /// <summary>
        /// Hashtable提交事务
        /// </summary>
        /// <param name="DFTable">sql 哈希集合</param>
        public void CommitTransaction(Hashtable DFTable)
        {
            DBHelperMySql.ExecuteSqlTran(DFTable);

        }
        #endregion

        #region  Hashtable提交事务--支持乐观锁--
        /// <summary>
        /// Hashtable提交事务--支持乐观锁
        /// </summary>
        /// <param name="DFTable"></param>
        /// <returns>返回false代表存在Sql受影响行数为0，更新失败！事务回滚</returns>
        public bool CommitTransactionLock(Hashtable DFTable)
        {
            return DBHelperMySql.ExecuteSqlTranLock(DFTable);
        }

        #endregion

        #region IDictionary提交事务--支持乐观锁--
        /// <summary>
        /// IDictionary
        /// </summary>
        /// <param name="DFTable">sql 哈希集合</param>
        public bool CommitTransactionLock(IDictionary<string, DbParameter[]> DFTable)
        {
            return DBHelperMySql.ExecuteSqlTranLock(DFTable);

        }
        #endregion 

        #region IDictionary提交事务--支持乐观锁--
        /// <summary>
        /// IDictionary
        /// </summary>
        /// <param name="DFTable">sql 哈希集合</param>
        public bool CommitTransactionLock(IDictionary<StringBuilder, DbParameter[]> DFTable)
        {
            return DBHelperMySql.ExecuteSqlTranLock(DFTable);

        }
        #endregion

        #region IDictionary提交事务--
        /// <summary>
        /// IDictionary
        /// </summary>
        /// <param name="DFTable">sql 哈希集合</param>
        public void CommitTransaction(IDictionary<string, DbParameter[]> DFTable)
        {
            DBHelperMySql.ExecuteSqlTran(DFTable);

        }
        #endregion

        #region 更新方法--
        /// <summary>
        /// 更新方法模版
        /// </summary>
        /// <param name="setValue">更新字段和值</param>
        /// <param name="strWhere"></param>
        /// <param name="arrayList"></param>
        public void Update(string setValue, string strWhere, ref Hashtable DFTable)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("update {0} set {1}", TableName, setValue);
            strSql.Append(GetWhere(strWhere));
            DFTable.Add(strSql, null);
        }
        #endregion

        #region 更新方法--
        /// <summary>
        /// 更新方法模版
        /// </summary>
        /// <param name="setValue">更新字段和值</param>
        /// <param name="strWhere"></param>
        /// <param name="arrayList"></param>
        public void Update(string setValue, string strWhere, ref IDictionary<StringBuilder, DbParameter[]> DFTable)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("update {0} set {1}", TableName, setValue);
            strSql.Append(GetWhere(strWhere));
            DFTable.Add(strSql, null);
        }

        /// <summary>
        /// 更新方法模版
        /// </summary>
        /// <param name="setValue">更新字段和值</param>
        /// <param name="strWhere"></param>
        /// <param name="arrayList"></param>
        public bool Update(string setValue, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("update {0} set {1}", TableName, setValue);
            strSql.Append(GetWhere(strWhere));
            return DBHelperMySql.ExcuteNonQuery(strSql.ToString()) > 0;
        }

        /// <summary>
        /// 更新方法模版
        /// </summary>
        /// <param name="setValue">更新字段和值</param>
        /// <param name="strWhere"></param>
        /// <param name="arrayList"></param>
        public void Update(string setValue, string strWhere, ref IDictionary<string, DbParameter[]> DFTable)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("update {0} set {1}", TableName, setValue);
            strSql.Append(GetWhere(strWhere));
            DFTable.Add(strSql.ToString(), null);
        }
        #endregion

        #region 获取查询条件--
        /// <summary>
        /// 获取查询条件 
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns>返回where语句，无返回空字符串</returns>
        protected string GetWhere(string strWhere)
        {
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                return string.Format(" WHERE {0} ", strWhere);
            }
            return string.Empty;
        }
        #endregion

        #region 分页查询--
        /// <summary>
        /// 根据条件查询分页订单列表--条件不含子表信息--只查询一张主表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>返回需要的订单列表(不含订单子表信息)</returns>
        public virtual PageModel<R> PageQuery<R>(PageParam model) where R : class, new()
        {
            StringBuilder strSql = new StringBuilder();
            MySqlParameter[] parameters = new MySqlParameter[7];
            parameters[0] = new MySqlParameter("_fields", model.Fields);
            parameters[1] = new MySqlParameter("_tableName", model.TableName ?? TableName);
            parameters[2] = new MySqlParameter("_where", model.StrWhere);
            parameters[3] = new MySqlParameter("_orderby", model.OrderBy);
            parameters[4] = new MySqlParameter("_pageindex", model.PageIndex);
            parameters[5] = new MySqlParameter("_pagesize", model.PageSize);
            parameters[6] = new MySqlParameter("_totalcount", MySqlDbType.Int32);
            parameters[6].Direction = ParameterDirection.Output;
            DataSet ds = DBHelperMySql.RunProceReturnDataSet("PageQuery", parameters);
            PageModel<R> respQuery = new PageModel<R>()
            {
                List = ds.Tables[0].ToList<R>(),
                TotalCount = int.Parse(parameters[6].Value.ToString()),
                PageIndex = model.PageIndex,
                PageSize = model.PageSize
            };
            return respQuery;
        }
        #endregion

        #region 查询--
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <returns></returns>
        public IList<R> Query<R>(QueryParam model)
        {
            MySqlParameter[] parameters = {
                new MySqlParameter("_fields",model.Fields),
                 new MySqlParameter("_tableName",model.TableName??TableName),
                new MySqlParameter("_where",model.StrWhere),
                new MySqlParameter("_orderby",model.OrderBy),
            };
            return DBHelperMySql.RunProceReturnDataSet("Query", parameters)
                .Tables[0].ToList<R>();
        }



        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <returns></returns>
        public IList<R> Query<R>(QueryParam model, string tableName)
        {
            MySqlParameter[] parameters = {
                new MySqlParameter("_fields",model.Fields),
                 new MySqlParameter("_tableName",tableName),
                new MySqlParameter("_where",model.StrWhere),
                new MySqlParameter("_orderby",model.OrderBy),
            };
            return DBHelperMySql.RunProceReturnDataSet("Query", parameters)
                .Tables[0].ToList<R>();
        }
        #endregion



        #region 插入
        /// <summary>
        /// 插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="DFTable"></param>
        public void Insert<T>(T model, ref Hashtable DFTable)
        {

            StringBuilder strSql = new StringBuilder();
            MySqlParameter[] parameters;
            strSql.AppendFormat("insert into {0}", TableName);
            strSql.Append(GetInsertFields());
            strSql.Append(" values ");
            strSql.Append(GetInsertValues<T>(model, out parameters));
            DFTable.Add(strSql, parameters);
        }
        #endregion


        #region 插入返回主键
        /// <summary>
        /// 插入返回主键
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="DFTable"></param>
        public bool InsertReturn<T>(T model, ref long pk)
        {

            StringBuilder strSql = new StringBuilder();
            MySqlParameter[] parameters;
            strSql.AppendFormat("insert into {0}", TableName);
            strSql.Append(GetInsertFields());
            strSql.Append(" values ");
            strSql.Append(GetInsertValues<T>(model, out parameters));
            int result = DBHelperMySql.ExcuteInsertReturnId(strSql.ToString(), out pk, parameters);
            return result > 0;
        }

        #region 插入
        /// <summary>
        /// 插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="DFTable"></param>
        public void Insert<T>(T model, ref IDictionary<StringBuilder, DbParameter[]> DFTable)
        {

            StringBuilder strSql = new StringBuilder();
            MySqlParameter[] parameters;
            strSql.AppendFormat("insert into {0}", TableName);
            strSql.Append(GetInsertFields());
            strSql.Append(" values ");
            strSql.Append(GetInsertValues<T>(model, out parameters));
            DFTable.Add(strSql, parameters);
        }
        #endregion

        /// <summary>
        /// 插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="DFTable"></param>
        public void Insert<T>(T model, ref IDictionary<string, DbParameter[]> DFTable)
        {

            StringBuilder strSql = new StringBuilder();
            MySqlParameter[] parameters;
            strSql.AppendFormat("insert into {0}", TableName);
            strSql.Append(GetInsertFields());
            strSql.Append(" values ");
            strSql.Append(GetInsertValues<T>(model, out parameters));
            DFTable.Add(strSql.ToString(), parameters);
        }

        protected virtual string GetInsertValues<T>(T item, out MySqlParameter[] param)
        {
            param = new MySqlParameter[0];
            return string.Empty;
        }

        #endregion

        #region 批量插入--Hashtable--
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="DFTable"></param>
        public void BatchInsert<T>(IEnumerable<T> param, ref Hashtable DFTable)
        {
            if (param == null || param.Count() <= 0)
            {
                return;
            }
            //var list = param as IEnumerable<InsertModel>;
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(" INSERT INTO {0} ", TableName);
            strSql.Append(GetInsertFields());
            strSql.AppendFormat(" VALUES ");
            int index = 0;
            IList<MySqlParameter> parameters = new List<MySqlParameter>();
            foreach (var item in param)
            {
                index++;
                strSql.Append(GetInsertValues(item, index, ref parameters));
            }
            strSql.Remove(strSql.Length - 1, 1);
            DFTable.Add(strSql, parameters.ToArray());
        }

        #endregion

        #region 获取插入的字段
        /// <summary>
        /// 获取插入的字段
        /// </summary>
        /// <returns></returns>
        protected virtual string GetInsertFields()
        {
            return string.Empty;
        }

        /// <summary>
        /// 获取插入的字段和值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected virtual string GetInsertValues<T>(T item, int index, ref IList<MySqlParameter> parameters)
        {
            return string.Empty;
        }
        #endregion

        #region 批量插入--IDictionary--
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="DFTable"></param>
        public void BatchInsert<T>(IEnumerable<T> param, ref IDictionary<string, DbParameter[]> DFTable)
        {
            if (param == null || param.Count() <= 0)
            {
                return;
            };
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(" INSERT INTO {0} ", TableName);
            strSql.Append(GetInsertFields());
            strSql.AppendFormat(" VALUES ");
            int index = 0;
            IList<MySqlParameter> parameters = new List<MySqlParameter>();
            foreach (var item in param)
            {
                index++;
                strSql.Append(GetInsertValues(item, index, ref parameters));
            }
            strSql.Remove(strSql.Length - 1, 1);
            DFTable.Add(strSql.ToString(), parameters.ToArray());
        }
        #endregion

        #region 删除--Hashtable--
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strWhere"></param  >
        /// <param name="DFTable"></param>
        public void Delete(string strWhere, ref Hashtable DFTable)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("delete from {0}", TableName);
            strSql.Append(GetWhere(strWhere));
            DFTable.Add(strSql, null);
        }
        #endregion

        #region 删除--IDictionary--
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strWhere"></param  >
        /// <param name="DFTable"></param>
        public void Delete(string strWhere, ref IDictionary<string, DbParameter[]> DFTable)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("delete from {0}", TableName);
            strSql.Append(GetWhere(strWhere));
            DFTable.Add(strSql.ToString(), null);
        }
        #endregion

        #region 删除--
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strWhere"></param  >
        /// <param name="DFTable"></param>
        public bool Delete(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("delete from {0}", TableName);
            strSql.Append(GetWhere(strWhere));
            return DBHelperMySql.ExcuteNonQuery(strSql.ToString()) > 0;
        }
        #endregion

        #region Replace--
        /// <summary>
        /// Replace
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="DFTable"></param>
        public void Replace<T>(T model, ref Hashtable DFTable)
        {

            StringBuilder strSql = new StringBuilder();
            MySqlParameter[] parameters;
            strSql.AppendFormat(" REPLACE INTO {0}", TableName);
            strSql.Append(GetInsertFields());
            strSql.Append(" VALUES ");
            strSql.Append(GetInsertValues<T>(model, out parameters));
            DFTable.Add(strSql, parameters);
        }


        #endregion

        #region Replace--IDictionary--
        /// <summary>
        /// Replace
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="DFTable"></param>
        public void Replace<T>(T model, ref IDictionary<string, DbParameter[]> DFTable)
        {

            StringBuilder strSql = new StringBuilder();
            MySqlParameter[] parameters;
            strSql.AppendFormat(" REPLACE INTO {0}", TableName);
            strSql.Append(GetInsertFields());
            strSql.Append(" VALUES ");
            strSql.Append(GetInsertValues<T>(model, out parameters));
            DFTable.Add(strSql.ToString(), parameters);
        }


        #endregion

        #region 批量Replace--Hashtable--
        /// <summary>
        /// 批量Replace
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="DFTable"></param>
        public void BatchReplace<T>(IEnumerable<T> param, ref Hashtable DFTable)
        {
            //var list = param as IEnumerable<InsertModel>;
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(" REPLACE INTO {0} ", TableName);
            strSql.Append(GetInsertFields());
            strSql.AppendFormat(" VALUES ");
            int index = 0;
            IList<MySqlParameter> parameters = new List<MySqlParameter>();
            foreach (var item in param)
            {
                index++;
                strSql.Append(GetInsertValues(item, index, ref parameters));
            }
            strSql.Remove(strSql.Length - 1, 1);
            DFTable.Add(strSql, parameters.ToArray());
        }

        #endregion

        #region 批量Replace--IDictionary--
        /// <summary>
        /// 批量Replace
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="DFTable"></param>
        public void BatchReplace<T>(IEnumerable<T> param, ref IDictionary<string, DbParameter[]> DFTable)
        {
            if (param == null || param.Count() <= 0)
            {
                return;
            }
            //var list = param as IEnumerable<InsertModel>;
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(" REPLACE INTO {0} ", TableName);
            strSql.Append(GetInsertFields());
            strSql.AppendFormat(" VALUES ");
            int index = 0;
            IList<MySqlParameter> parameters = new List<MySqlParameter>();
            foreach (var item in param)
            {
                index++;
                strSql.Append(GetInsertValues(item, index, ref parameters));
            }
            strSql.Remove(strSql.Length - 1, 1);
            DFTable.Add(strSql.ToString(), parameters.ToArray());
        }

        #endregion

        #region 反射获取属性名

        public virtual string JoinFields<T>(T model, int index) {
            var obj = new object();
            var fieldList = EntityUtil.GetFieldList(model, ref obj);
            var fieldNameList = EntityUtil.GetFieldNameList(model);
            var paras = new List<MySqlParameter>();

            foreach (var fieldInfo in fieldList)
            {
                paras.Add(new MySqlParameter("?" + index + fieldInfo.Name.GetFieldName(), fieldInfo.GetValue(obj)));
            }

            StringBuilder builder = new StringBuilder();
            builder.Append("(");
            for (int i = 0; i < fieldNameList.Count; i++)
            {
                var name = fieldNameList[i];
                builder.Append("?" + index + name);
                if (i < fieldNameList.Count) builder.Append(",");
            }
            builder.Append("?" + string.Join(",?", fieldNameList));
            builder.Append(")");

            return builder.ToString();
        }

        #endregion


        #region 反射获取属性值

        public virtual string JoinFieldValues<T>(T model, out MySqlParameter[] param)
        {
            var obj = new object();
            var fieldList = EntityUtil.GetFieldList(model, ref obj);
            var fieldNameList = EntityUtil.GetFieldNameList(model);
            var paras = new List<MySqlParameter>();

            foreach (var fieldInfo in fieldList)
            {
                paras.Add(new MySqlParameter("?" + fieldInfo.Name.GetFieldName(), fieldInfo.GetValue(obj)));
            }

            param = paras.ToArray();

            StringBuilder builder = new StringBuilder();
            builder.Append("(");
            builder.Append("?" + string.Join(",?", fieldNameList));
            builder.Append(")");

            return builder.ToString();
        }

        #endregion
    }
}
