namespace C273_E.Units {
    public class Celcius : Unit {
        public override char Code { get; } = 'c';

        public Celcius(decimal value) : base(value) {
        }

        [ConversionMethod]
        public Celcius ConvertToCelcius() {
            return this;
        }

        [ConversionMethod]
        public Fahrenheit ConvertToFahrenheit() {
            return Unit.Create<Fahrenheit>((Value * (9M / 5M)) + 32);
        }

        [ConversionMethod]
        public Kelvin ConvertToKelvin() {
            return Unit.Create<Kelvin>(Value + 273.15M);
        }
    }
}
