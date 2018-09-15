using MongoDB.Bson;
using MongoDB.Driver;
using MongodbRepositoryHelper;
using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DF.DBUtility.MongoDb
{
    /// <summary>
    /// MongoDb访问帮助类
    /// </summary>
    public class MongoHelper
    {
        /// <summary>
        /// 数据库连接地址
        /// </summary>
        public static readonly string nosqlConnectionString = NosqlConfigHelper.GetConfig("MongoHelperConfig").ConnectionString;
        /// <summary>
        /// 数据库名词
        /// </summary>
        public static readonly string nosqlDbName = NosqlConfigHelper.GetConfig("MongoHelperConfig").DbName;

        #region 插入--
        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>    
        /// <param name="collectionName">集合名称</param>
        /// <param name="model">数据对象</param>
        public static bool Insert<T>(string collectionName, T model) where T : IEntity<string>
        {
            var result = MongoDbHepler.Insert(nosqlConnectionString, nosqlDbName, collectionName, model);
            return result.Ok;
        }

        /// <summary>
        /// 批量插入记录
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="collectionName">集合名称</param>
        /// <param name="model">数据对象</param>
        public static IEnumerable<bool> InsertBatch<T>(string collectionName, IEnumerable<T> model) where T : IEntity<string>
        {
            var result = MongoDbHepler.InsertBatch(nosqlConnectionString, nosqlDbName, collectionName, model);
            return result.Select(u => u.Ok);
        }
        #endregion


        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="query">查询条件条件查询,调用示例：Query.Matches("Title", "感冒") 或者 Query.EQ("Title", "感冒") 或者Query.And(Query.Matches("Title", "感冒"),Query.EQ("Author", "yanc")) 等等</param>
        /// <param name="update">更新字段,调用示例：Update.Set("Title", "yanc") 或者 Update.Set("Title", "yanc").Set("Author", "yanc2") 等等</param>
        public static bool Update(string collectionName, MongoDB.Driver.IMongoQuery query, MongoDB.Driver.IMongoUpdate update)
        {
            var result = MongoDbHepler.Update(nosqlConnectionString, nosqlDbName, collectionName, query, update);

            return result.Ok;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entitys"></param>
        public static void Update<T>(IEnumerable<T> entitys, string collectionName) where T : IEntity<string>
        {
            MongoDbHepler.Update1<T>(entitys, collectionName, nosqlConnectionString);
        }

        #region MongoDb原子操作FindAndModify--
        /// <summary>
        /// MongoDb原子操作FindAndModify--
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static FindAndModifyResult FindAndModify(string collectionName, FindAndModifyArgs args)
        {
            return MongoDbHepler.FindAndModify(nosqlConnectionString, nosqlDbName, collectionName, args);
        }
        #endregion

        #region 查询--

        /// <summary>
        /// 根据ID获取数据对象
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="collectionName">集合名称</param>
        /// <param name="id">主键ID</param>
        /// <returns>数据对象</returns>
        public static T GetById<T>(string collectionName, string id) where T : IEntity<string>
        {
            var result = MongoDbHepler.GetById<T>(nosqlConnectionString, nosqlDbName, collectionName, id);
            return result;
        }

        /// <summary>
        /// 根据查询条件获取一条数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="collectionName">集合名称</param>
        /// <param name="query">查询条件,调用示例：Query.Matches("Title", "感冒") 或者 Query.EQ("Title", "感冒") 或者Query.And(Query.Matches("Title", "感冒"),Query.EQ("Author", "yanc"))</param>
        /// <returns>数据对象</returns>
        public static T GetOneByCondition<T>(string collectionName, MongoDB.Driver.IMongoQuery query) where T : IEntity<string>
        {
            return MongoDbHepler.GetOneByCondition<T>(nosqlConnectionString, nosqlDbName, collectionName, query);
        }


        /// <summary>
        /// 根据查询条件获取多条数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="collectionName">集合名称</param>
        /// <param name="query">查询条件,调用示例：Query.Matches("Title", "感冒") 或者 Query.EQ("Title", "感冒") 或者Query.And(Query.Matches("Title", "感冒"),Query.EQ("Author", "yanc"))</param>
        /// <returns>数据对象集合</returns>
        public static List<T> GetManyByCondition<T>(string collectionName, IMongoQuery query) where T : IEntity<string>
        {
            return MongoDbHepler.GetManyByCondition<T>(nosqlConnectionString, nosqlDbName, collectionName, query).ToList();
        }

        /// <summary>
        /// 根据集合中的所有数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>     
        /// <param name="collectionName">集合名称</param>
        /// <returns>数据对象集合</returns>
        public static List<T> GetAll<T>(string collectionName) where T : IEntity<string>
        {
            return MongoDbHepler.GetAll<T>(nosqlConnectionString, nosqlDbName, collectionName).ToList();
        }

        #region 分页查询 
        /// <summary>
        /// 分页查询 swj
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName">文档名称</param>
        /// <param name="query">查询条件</param>
        /// <param name="pageInfo">分页条件</param>
        /// <param name="sortBy">排序条件</param>
        /// <param name="fields">关键字</param>
        /// <returns></returns>
        public static List<T> GetAll<T>(string collectionName, IMongoQuery query, PagerInfo pageInfo,IMongoSortBy sortBy, params string[] fields) where T : IEntity<string>
        {
            return MongoDbHepler.GetAll<T>(collectionName, query, pageInfo, sortBy, fields).ToList();
        }

        #endregion


        #region 删除 - yf
        /// <summary>
        /// 根据条件删除数据
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="query">条件</param>
        /// <returns></returns>
        public static bool Delete(IMongoQuery query, string collectionName)
        {
            var result = MongoDbHepler.Delete(nosqlConnectionString, nosqlDbName, collectionName, query);
            return result.Ok;
        }
        #endregion



        
        
        #endregion
    }
}
