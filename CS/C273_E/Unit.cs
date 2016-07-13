using C273_E.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace C273_E {
    public abstract class Unit : IUnit {
        #region Static Members
        public static Dictionary<char, Type> UnitSubclasses { get; } = GetUnitSubClasses();

        public static IUnit Create(char code, decimal value) {
            Type type;
            if (UnitSubclasses.TryGetValue(code, out type)) {
                return (IUnit)Activator.CreateInstance(UnitSubclasses[code], value);
            } else {
                throw GetUnitTypeNotImplementedException(code);
            }
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

        public static UnitTypeNotImplementedException GetUnitTypeNotImplementedException(char code) {
            var message = string.Format(@"A unit type with code '{0}' is not implemented.", code);
            return new UnitTypeNotImplementedException(code, message);
        }
        #endregion

        public Dictionary<Type, Func<IUnit>> ConversionMethods { get; }

        public abstract char Code { get; }
        public decimal Value { get; set; }

        protected Unit(decimal value) {
            Value = value;
            ConversionMethods = this.GetConversionMethods();
        }

        private IUnit ConvertTo(Type target) {
            Func<IUnit> conversionMethod;
            if (ConversionMethods.TryGetValue(target, out conversionMethod)) {
                return conversionMethod();
            } else {
                var message = string.Format(@"No candidate for converting {0} to {1}.", GetType().Name, target.Name);
                throw new NoConversionCandidateException(message);
            }
        }

        public IUnit ConvertTo(char code) {
            Type type;
            if (UnitSubclasses.TryGetValue(code, out type)) {
                return ConvertTo(type);
            } else {
                throw GetUnitTypeNotImplementedException(code);
            }
        }

        public T ConvertTo<T>() where T : IUnit {
            return (T)ConvertTo(typeof(T));
        }
    }
}
