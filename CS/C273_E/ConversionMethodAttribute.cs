using System;

namespace C273_E {
    [AttributeUsage(AttributeTargets.Method)]
    public class ConversionMethodAttribute : Attribute {
        public ConversionMethodAttribute() {
        }
    }
}
