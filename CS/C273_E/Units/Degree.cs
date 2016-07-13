using System;

namespace C273_E.Units {
    public class Degree : Unit {
        public override char Code { get; } = 'd';

        public Degree(decimal value) : base(value) {
        }

        [ConversionMethod]
        public Degree ConvertToDegrees() {
            return this;
        }

        [ConversionMethod]
        public Radian ConvertToRadians() {
            return Unit.Create<Radian>(Value * ((decimal)Math.PI / 180M));
        }
    }
}
