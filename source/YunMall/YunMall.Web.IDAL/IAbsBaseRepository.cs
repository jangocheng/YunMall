using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using YunMall.Entity.ModelView;

namespace YunMall.Web.IDAL
{
    public interface IAbsBaseRepository
    {
        string Fields { get; set; }

        /// <summary>
        /// Hashtable提交事务
        /// </summary>
        /// <param name="table"></param>
        void CommitTransaction(Hashtable table);

        /// <summary>
        /// Hashtable提交事务
        /// </summary>
        /// <param name="table"></param>
        void CommitTransaction(IDictionary<string, DbParameter[]> table);


        bool CommitTransactionLock(IDictionary<string, DbParameter[]> table);

        bool CommitTransactionLock(IDictionary<StringBuilder, DbParameter[]> table);

        bool CommitTransactionLock(Hashtable table);

        #region 更新--Hashtable--
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="setValue">更新字段和值</param>
        /// <param name="strWhere"></param>
        /// <param name="arrayList"></param>
        void Update(string setValue, string strWhere, ref Hashtable table);
        #endregion

        #region 更新
        /// <summary>
        /// 更新方法模版
        /// </summary>
        /// <param name="setValue">更新字段和值</param>
        /// <param name="strWhere"></param>
        /// <param name="arrayList"></param>
        bool Update(string setValue, string strWhere);
        #endregion

        #region 更新--IDictionary--
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="setValue">更新字段和值</param>
        /// <param name="strWhere"></param>
        /// <param name="arrayList"></param>
        void Update(string setValue, string strWhere, ref IDictionary<string, DbParameter[]> table);
        #endregion

        #region 插入--Hashtable--
        /// <summary>
        /// 插入--
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="table"></param>
        void Insert<T>(T param, ref Hashtable table);
        #endregion


        #region 插入--Hashtable--
        /// <summary>
        /// 插入--
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="table"></param>
        void Insert<T>(T model, ref IDictionary<StringBuilder, DbParameter[]> table);
        #endregion

     

        #region 插入--IDictionary--
        /// <summary>
        /// 插入--
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="table"></param>
        void Insert<T>(T param, ref IDictionary<string, DbParameter[]> table);

        #endregion

        #region 批量插入--Hashtable--
        /// <summary>
        /// 批量插入--
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="table"></param>
        void BatchInsert<T>(IEnumerable<T> param, ref Hashtable table);

        #endregion

        #region 批量插入--IDictionary--
        /// <summary>
        /// 批量插入--
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="table"></param>
        void BatchInsert<T>(IEnumerable<T> param, ref IDictionary<string, DbParameter[]> table);

        #endregion



        #region 插入返回主键id--Hashtable--
        /// <summary>
        /// 插入返回主键id--
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="table"></param>
        bool InsertReturn<T>(T model, ref long pk);
        #endregion

        #region 分页查询--
        /// <summary>
        /// 根据条件查询分页订单列表--条件不含子表信息--只查询一张主表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>返回需要的订单列表(不含订单子表信息)</returns>
        PageModel<R> PageQuery<R>(PageParam conditions) where R : class, new();

        #endregion

        #region 查询--
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <returns></returns>
        IList<R> Query<R>(QueryParam model);


        #endregion

        #region 删除--Hashtable--
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="table"></param>
        void Delete(string strWhere, ref Hashtable table);
        #endregion

        #region 删除--IDictionary--
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="table"></param>
        void Delete(string strWhere, ref IDictionary<string, DbParameter[]> table);
        #endregion

        #region 删除--
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strWhere"></param  >
        /// <param name="table"></param>
        bool Delete(string strWhere);

        #endregion

        #region Replace--Hashtable--
        /// <summary>
        /// Replace
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="table"></param>
        void Replace<T>(T model, ref Hashtable table);



        #endregion

        #region Replace--IDictionary--
        /// <summary>
        /// Replace
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="table"></param>
        void Replace<T>(T model, ref IDictionary<string, DbParameter[]> table);


        #endregion

        #region 批量Replace--Hashtable--
        /// <summary>
        /// 批量Replace
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="table"></param>
        void BatchReplace<T>(IEnumerable<T> param, ref Hashtable table);

        #endregion

        #region 批量Replace--IDictionary--
        /// <summary>
        /// 批量Replace
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="table"></param>
        void BatchReplace<T>(IEnumerable<T> param, ref IDictionary<string,DbParameter[]> table);

        #endregion



        #region 反射获取属性名

        string JoinFields<T>(T model, int index);

        #endregion



        #region 反射获取属性值

        string JoinFieldValues<T>(T model, out MySqlParameter[] param);

        #endregion
    }
}
