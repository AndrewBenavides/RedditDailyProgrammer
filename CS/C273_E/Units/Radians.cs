using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C273_E.Units {
    public class Radians : Unit {
        public override char Code { get; } = 'r';

        public Radians(decimal value) : base(value) {
        }

        [ConversionMethod(target: typeof(Degrees))]
        public decimal ConvertToDegrees() {
            return (Value * (180M / (decimal)Math.PI));
        }
    }
}
