using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C273_E.Units {
    public class Radian : Unit {
        public override char Code { get; } = 'r';

        public Radian(decimal value) : base(value) {
        }

        [ConversionMethod(target: typeof(Degree))]
        public Degree ConvertToDegrees() {
            return Unit.Create<Degree>(Value * (180M / (decimal)Math.PI));
        }
    }
}
