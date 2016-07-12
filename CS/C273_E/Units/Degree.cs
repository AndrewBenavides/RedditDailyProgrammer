using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C273_E.Units {
    public class Degree : Unit {
        public override char Code { get; } = 'd';

        public Degree(decimal value) : base(value) {
        }

        [ConversionMethod(target: typeof(Radian))]
        public Radian ConvertToRadians() {
            return Unit.Create<Radian>(Value * ((decimal)Math.PI / 180M));
        }
    }
}
