using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DF.Shop.Common.InvokeInterface
{
    /// <summary>
    /// 移动接口返回BaseModel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseModel<T> where T : class
    {
        private BaseModel() { }
        /// <summary>
        /// 是否有数据
        /// </summary>
        public bool Isdata { get; internal set; }
        /// <summary>
        /// 实体类
        /// </summary>
        public T Model { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public IList<T> List { get; set; } = new List<T>();
        /// <summary>
        /// 操作结果
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// Model初始化
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static BaseModel<T> NoSet()
        {
            return new BaseModel<T>()
            {
            };
        }

        /// <summary>
        /// Model赋值
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static BaseModel<T> SetModel(T model)
        {
            return new BaseModel<T>()
            {
                Model = model
            };
        }
        /// <summary>
        /// List赋值
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static BaseModel<T> SetList(IList<T> list)
        {
            return new BaseModel<T>()
            {
                List = list
            };
        }
        /// <summary>
        /// Result赋值
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static BaseModel<T> SetResult(string result)
        {
            return new BaseModel<T>()
            {
                Result = result
            };
        }
    }
}
