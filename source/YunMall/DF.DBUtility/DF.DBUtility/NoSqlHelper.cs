using MongoDB.Driver;
using MongodbRepositoryHelper;
using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS.DBUtility
{
    public class NoSqlHelper
    {
        #region 2017-01-21  MongoDb向指定集合插入一条记录--yfx
        /// <summary>
        /// MongoDb向指定集合插入一条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName">存储集合名</param>
        /// <param name="model">存储数据模型</param>
        /// <returns></returns>
        public static bool Insert<T>(string collectionName, T model) where T : IEntity<string>
        {
            var result = MongoDbHepler.Insert(collectionName, model);
            return result.Ok;
        }
        #endregion
    }
}
