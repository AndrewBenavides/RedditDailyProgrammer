using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C273_E.Units {
    public class Degrees : Unit {
        public override char Code { get; } = 'd';

        public Degrees(decimal value) : base(value) {
        }

        [ConversionMethod(target: typeof(Radians))]
        public decimal ConvertToRadians() {
            return Value * ((decimal)Math.PI / 180M);
        }
    }
}
