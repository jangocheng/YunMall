using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YunMall.Web.DAL.utils
{
    public static class EntityUtil
    {
        /// <summary>
        /// 获取某个类的所有属性名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IList<string> GetFieldNameList<T>(T data) {
            Type t = typeof(T);
            Type[] types = new Type[0];//为构造函数准备参数类型  
            ConstructorInfo ci = t.GetConstructor(types); //获得构造函数  
            object[] objs = new object[0];//为构造函数准备参数值  
            object obj = ci.Invoke(objs);//调用构造函数创建对象  
            FieldInfo[] fis = t.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            IList<string> list = new List<string>();
            foreach (var fieldInfo in fis)
            {
                list.Add(fieldInfo.Name.Substring(1, fieldInfo.Name.IndexOf(">") - 1));
            }
            return list.Count == 0 ? null : list;
        }


        /// <summary>
        /// 获取某个类的所有属性名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static FieldInfo[] GetFieldList<T>(T data, ref object obj)
        {
            Type t = typeof(T);
            Type[] types = new Type[0];//为构造函数准备参数类型  
            ConstructorInfo ci = t.GetConstructor(types); //获得构造函数  
            object[] objs = new object[0];//为构造函数准备参数值  
            obj = ci.Invoke(objs);//调用构造函数创建对象  
            FieldInfo[] fis = t.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            return fis.Length == 0 ? null : fis;
        }


        public static string GetFieldName(this string data) {
            return data.Substring(1, data.IndexOf(">") - 1);
        }
    }
}
