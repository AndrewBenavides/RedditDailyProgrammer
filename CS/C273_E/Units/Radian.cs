using System;

namespace C273_E.Units {
    public class Radian : Unit {
        public override char Code { get; } = 'r';

        public Radian(decimal value) : base(value) {
        }

        [ConversionMethod]
        public Degree ConvertToDegrees() {
            return Unit.Create<Degree>(Value * (180M / (decimal)Math.PI));
        }
    }
}
