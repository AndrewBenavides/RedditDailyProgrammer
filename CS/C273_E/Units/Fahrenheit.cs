namespace C273_E.Units {
    public class Fahrenheit : Unit {
        public override char Code { get; } = 'f';

        public Fahrenheit(decimal value) : base(value) {
        }

        [ConversionMethod]
        public Celcius ConvertToCelcius() {
            return Unit.Create<Celcius>((Value - 32) * (5M / 9M));
        }

        [ConversionMethod]
        public Fahrenheit ConvertToFahrenheit() {
            return this;
        }

        [ConversionMethod]
        public Kelvin ConvertToKelvin() {
            return Unit.Create<Kelvin>((Value + 459.67M) * (5M / 9M));
        }
    }
}
