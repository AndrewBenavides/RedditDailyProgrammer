using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C273_E {
    [AttributeUsage(AttributeTargets.Method)]
    public class ConversionMethodAttribute : Attribute {
        public Type Target { get; private set; }

        public ConversionMethodAttribute(Type target) {
            Target = target;
        }
    }
}
