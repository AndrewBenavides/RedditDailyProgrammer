using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace C273_E {
    public static class ConversionMethodFinder {
        public static Dictionary<Type, Func<IUnit>> GetConversionMethods(this IUnit instance) {
            var dict = new Dictionary<Type, Func<IUnit>>();
            var methods = instance.GetType().GetMethods()
                .Where(m => m.GetCustomAttribute<ConversionMethodAttribute>() != null);
            foreach (var methodInfo in methods) {
                var key = methodInfo.ReturnType;
                var value = BuildFunc<IUnit>(methodInfo, instance);
                dict.Add(key, value, instance);
            }
            return dict;
        }

        private static void Add(this Dictionary<Type, Func<IUnit>> dictionary, Type key, Func<IUnit> value, IUnit instance) {
            if (!dictionary.ContainsKey(key)) {
                dictionary.Add(key, value);
            } else {
                var message = string.Format("{0} already has a method for converting to {1}. This method will be ignored.", instance.GetType().Name, key.Name);
                throw new DuplicateKeyException(message);
            }
        }

        private static Func<T> BuildFunc<T>(MethodInfo method, object instance) {
            //http://stackoverflow.com/a/2933227
            var instanceExpression = Expression.Constant(instance);
            var call = Expression.Call(instanceExpression, method);
            var func = Expression.Lambda<Func<T>>(call).Compile();
            return func;
        }
    }
}
