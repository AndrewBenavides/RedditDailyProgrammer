using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq.Expressions;

namespace C273_E {
    public abstract class Unit : IUnit {
        #region Static Members
        public static Dictionary<char, Type> UnitSubclasses { get; } = GetUnitSubClasses();

        public static IUnit Create(char code, decimal value) {
            return (IUnit)Activator.CreateInstance(UnitSubclasses[code], value);
        }

        public static T Create<T>(decimal value) where T : IUnit {
            return (T)Activator.CreateInstance(typeof(T), value);
        }

        public static Dictionary<char, Type> GetUnitSubClasses() {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(Unit)))
                .Select(type => (IUnit)Activator.CreateInstance(type, 0M))
                .ToDictionary(
                    subclass => subclass.Code,
                    subclass => subclass.GetType()
                );
        }
        #endregion

        public Dictionary<Type, Func<IUnit>> ConversionMethods { get; }

        public abstract char Code { get; }
        public decimal Value { get; set; }

        protected Unit(decimal value) {
            Value = value;
            ConversionMethods = GetConversionMethods();
        }

        public T ConvertTo<T>() where T : IUnit {
            Func<IUnit> conversionMethod;
            if (ConversionMethods.TryGetValue(typeof(T), out conversionMethod)) {
                return (T)conversionMethod();
            } else {
                throw new NotImplementedException();
            }
        }

        public Dictionary<Type, Func<IUnit>> GetConversionMethods() {
            var dict = new Dictionary<Type, Func<IUnit>>();
            var methods = this.GetType().GetMethods()
                .Where(m => m.GetCustomAttribute<ConversionMethodAttribute>() != null);
            foreach(var methodInfo in methods) {
                var key = methodInfo.GetCustomAttribute<ConversionMethodAttribute>().Target;
                var value = BuildFunc<Func<IUnit>>(methodInfo);
                AddToDictionary(dict, key, value);
            }
            return dict;
        }

        private void AddToDictionary(Dictionary<Type, Func<IUnit>> dictionary, Type key, Func<IUnit> value) {
            if (!dictionary.ContainsKey(key)) {
                dictionary.Add(key, value);
            } else {
                var message = string.Format("{0} already has a conversion methods for converting to {1}. This method will be ignored.", GetType().Name, key.Name);
                Console.WriteLine(message);
            }
        }

        private T BuildFunc<T>(MethodInfo method) {
            //http://stackoverflow.com/a/2933227
            var instance = Expression.Constant(this);
            var call = Expression.Call(instance, method);
            var func = Expression.Lambda<T>(call).Compile();
            return func;
        }
    }
}
