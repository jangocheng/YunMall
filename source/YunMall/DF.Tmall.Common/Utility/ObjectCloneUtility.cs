using System.Collections.Generic;
using System.Reflection;
using System.ArrayExtensions;
using System.Linq.Expressions;

namespace System {

    /// <summary>
    /// 用于快速对象克隆的C＃扩展方法 
    /// github：https://github.com/Burtsev-Alexey/net-object-deep-copy
    /// 一百万次快速克隆一个包含字段数量达到二十个且包含复杂List的速度是：368ms，原始手动赋值是：118ms，序列化方式是：986ms，表达式树缓存方式是：780ms，反射是650ms
    /// </summary>
    public static class ObjectCloneUtility {
        private static readonly MethodInfo CloneMethod = typeof(Object).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);

        public static bool IsPrimitive(this Type type) {
            if (type == typeof(String)) return true;
            return (type.IsValueType & type.IsPrimitive);
        }

        public static Object Clone(this Object originalObject) {
            return InternalClone(originalObject, new Dictionary<Object, Object>(new ReferenceEqualityComparer()));
        }
        private static Object InternalClone(Object originalObject, IDictionary<Object, Object> visited) {
            if (originalObject == null) return null;
            var typeToReflect = originalObject.GetType();
            if (IsPrimitive(typeToReflect)) return originalObject;
            if (visited.ContainsKey(originalObject)) return visited[originalObject];
            if (typeof(Delegate).IsAssignableFrom(typeToReflect)) return null;
            var cloneObject = CloneMethod.Invoke(originalObject, null);
            if (typeToReflect.IsArray) {
                var arrayType = typeToReflect.GetElementType();
                if (IsPrimitive(arrayType) == false) {
                    Array clonedArray = (Array)cloneObject;
                    clonedArray.ForEach((array, indices) => array.SetValue(InternalClone(clonedArray.GetValue(indices), visited), indices));
                }

            }
            visited.Add(originalObject, cloneObject);
            CopyFields(originalObject, visited, cloneObject, typeToReflect);
            RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect);
            return cloneObject;
        }

        private static void RecursiveCopyBaseTypePrivateFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect) {
            if (typeToReflect.BaseType != null) {
                RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect.BaseType);
                CopyFields(originalObject, visited, cloneObject, typeToReflect.BaseType, BindingFlags.Instance | BindingFlags.NonPublic, info => info.IsPrivate);
            }
        }

        private static void CopyFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy, Func<FieldInfo, bool> filter = null) {
            foreach (FieldInfo fieldInfo in typeToReflect.GetFields(bindingFlags)) {
                if (filter != null && filter(fieldInfo) == false) continue;
                if (IsPrimitive(fieldInfo.FieldType)) continue;
                var originalFieldValue = fieldInfo.GetValue(originalObject);
                var clonedFieldValue = InternalClone(originalFieldValue, visited);
                fieldInfo.SetValue(cloneObject, clonedFieldValue);
            }
        }
        public static T Clone<T>(this T original) {
            return (T)Clone((Object)original);
        }
    }

    public class ReferenceEqualityComparer : EqualityComparer<Object> {
        public override bool Equals(object x, object y) {
            return ReferenceEquals(x, y);
        }
        public override int GetHashCode(object obj) {
            if (obj == null) return 0;
            return obj.GetHashCode();
        }
    }

    namespace ArrayExtensions {
        public static class ArrayExtensions {
            public static void ForEach(this Array array, Action<Array, int[]> action) {
                if (array.LongLength == 0) return;
                ArrayTraverse walker = new ArrayTraverse(array);
                do action(array, walker.Position);
                while (walker.Step());
            }
        }

        internal class ArrayTraverse {
            public int[] Position;
            private int[] maxLengths;

            public ArrayTraverse(Array array) {
                maxLengths = new int[array.Rank];
                for (int i = 0; i < array.Rank; ++i) {
                    maxLengths[i] = array.GetLength(i) - 1;
                }
                Position = new int[array.Rank];
            }

            public bool Step() {
                for (int i = 0; i < Position.Length; ++i) {
                    if (Position[i] < maxLengths[i]) {
                        Position[i]++;
                        for (int j = 0; j < i; j++) {
                            Position[j] = 0;
                        }
                        return true;
                    }
                }
                return false;
            }
        }
    }

    /// <summary>
    /// 用于快速对象克隆的C＃扩展方法 - 表达式缓存
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="R"></typeparam>
    public static class CloneExpression<T, R> {
        private static readonly Func<T, R> cache = GetFunc();
        private static Func<T, R> GetFunc() {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "p");
            List<MemberBinding> memberBindingList = new List<MemberBinding>();

            foreach (var item in typeof(R).GetProperties()) {
                if (!item.CanWrite)
                    continue;

                MemberExpression property = Expression.Property(parameterExpression, typeof(T).GetProperty(item.Name));
                MemberBinding memberBinding = Expression.Bind(item, property);
                memberBindingList.Add(memberBinding);
            }

            MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(R)), memberBindingList.ToArray());
            Expression<Func<T, R>> lambda = Expression.Lambda<Func<T, R>>(memberInitExpression, new ParameterExpression[] { parameterExpression });

            return lambda.Compile();
        }

        public static R Clone(T T) {
            return cache(T);
        }
    }

    /// <summary>
    /// 用于快速对象克隆的C＃扩展方法 - 反射
    /// </summary>

    public static class CloneReflection {

        public static PropertyInfo[] GetPropertyInfos(Type type) {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        /// <summary>
        /// 利用反射给不同类型对象同名属性赋值
        /// Blog:http://www.itdadao.com/articles/c15a429910p0.html
        /// </summary>
        /// <typeparam name="S">原类型</typeparam>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="s"></param>
        /// <param name="t"></param>
        public static void AutoMapping<S, T>(this S s, T t) {
            // get source PropertyInfos
            PropertyInfo[] pps = GetPropertyInfos(s.GetType());
            // get target type
            Type target = t.GetType();

            foreach (var pp in pps) {
                PropertyInfo targetPP = target.GetProperty(pp.Name);
                object value = pp.GetValue(s, null);

                if (targetPP != null && value != null) {
                    targetPP.SetValue(t, value, null);
                }
            }
        }
    }

}
