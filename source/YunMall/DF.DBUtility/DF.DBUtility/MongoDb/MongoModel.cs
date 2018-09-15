using  MongoRepository;
using  System;
using  System.Collections.Generic;
using  System.Linq;
using  System.Text;
using  System.Threading.Tasks;

namespace  DF.DBUtility.MongoDb
{
    /// <summary>
    /// MongoDb访问实体模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class MongoModel<T> : Entity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="model"></param>
        public MongoModel(T model)
        {
            this.Data = model;
        }
        /// <summary>
        /// 实体占位符
        /// </summary>
        public T Data { get; set; }

    }
}
