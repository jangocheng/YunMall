using  MongoDB.Bson;
using  MongoDB.Driver;
using  MongodbRepositoryHelper;
using  MongoRepository;
using  System;
using  System.Collections.Generic;
using  System.Linq;
using  System.Text;
using  System.Threading.Tasks;

namespace  DF.DBUtility.MongoDb
{
    /// <summary>
    /// MongoDb访问帮助类
    /// </summary>
    public class MongoHelper
    {
        /// <summary>
        /// 数据库连接地址
        /// </summary>
        public static readonly string nosqlConnectionString = NosqlConfigHelper.GetConfig().ConnectionString;
        /// <summary>
        /// 数据库名词
        /// </summary>
        public static readonly string nosqlDbName = NosqlConfigHelper.GetConfig().DbName;

        #region 插入--法师
        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>    
        /// <param name="collectionName">集合名称</param>
        /// <param name="model">数据对象</param>
        public static bool Insert<T>(string collectionName, T model)
        {
            var result = MongoDbHepler.Insert(nosqlConnectionString, nosqlDbName, collectionName, new MongoModel<T>(model));
            return result.Ok;
        }

        /// <summary>
        /// 批量插入记录
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="collectionName">集合名称</param>
        /// <param name="model">数据对象</param>
        public static IEnumerable<bool> InsertBatch<T>(string collectionName, IEnumerable<T> model)
        {
            var result = MongoDbHepler.InsertBatch(nosqlConnectionString, nosqlDbName, collectionName, model.Select(u => new MongoModel<T>(u)));
            return result.Select(u => u.Ok);
        }
        #endregion

        #region 查询--法师

        /// <summary>
        /// 根据ID获取数据对象
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="collectionName">集合名称</param>
        /// <param name="id">主键ID</param>
        /// <returns>数据对象</returns>
        public static T GetById<T>(string collectionName, string id)
        {
            MongoModel<T> result = MongoDbHepler.GetById<MongoModel<T>>(nosqlConnectionString, nosqlDbName, collectionName, id);
            return result.Data;
        }

        /// <summary>
        /// 根据查询条件获取一条数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="collectionName">集合名称</param>
        /// <param name="query">查询条件,调用示例：Query.Matches("Title", "感冒") 或者 Query.EQ("Title", "感冒") 或者Query.And(Query.Matches("Title", "感冒"),Query.EQ("Author", "yanc"))</param>
        /// <returns>数据对象</returns>
        public static T GetOneByCondition<T>(string collectionName, IMongoQuery query)
        {
            return MongoDbHepler.GetOneByCondition<MongoModel<T>>(nosqlConnectionString, nosqlDbName, collectionName, query).Data;
        }


        /// <summary>
        /// 根据查询条件获取多条数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="collectionName">集合名称</param>
        /// <param name="query">查询条件,调用示例：Query.Matches("Title", "感冒") 或者 Query.EQ("Title", "感冒") 或者Query.And(Query.Matches("Title", "感冒"),Query.EQ("Author", "yanc"))</param>
        /// <returns>数据对象集合</returns>
        public static List<T> GetManyByCondition<T>(string collectionName, IMongoQuery query)
        {
            return MongoDbHepler.GetManyByCondition<MongoModel<T>>(nosqlConnectionString, nosqlDbName, collectionName, query).Select(u => u.Data).ToList();
        }

        /// <summary>
        /// 根据集合中的所有数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>     
        /// <param name="collectionName">集合名称</param>
        /// <returns>数据对象集合</returns>
        public static List<T> GetAll<T>(string collectionName)
        {
            return MongoDbHepler.GetAll<MongoModel<T>>(nosqlConnectionString, nosqlDbName, collectionName).Select(u => u.Data).ToList(); ;
        }

        #endregion
    }
}
