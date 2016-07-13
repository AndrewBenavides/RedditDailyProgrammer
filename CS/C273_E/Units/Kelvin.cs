namespace C273_E.Units {
    public class Kelvin : Unit {
        public override char Code { get; } = 'k';

        public Kelvin(decimal value) : base(value) {
        }

        [ConversionMethod]
        public Celcius ConvertToCelcius() {
            return Unit.Create<Celcius>(Value - 273.15M);
        }

        [ConversionMethod]
        public Fahrenheit ConvertToFahrenheit() {
            return Unit.Create<Fahrenheit>((Value * (9M / 5M)) - 459.67M);
        }

        [ConversionMethod]
        public Kelvin ConvertToKelvin() {
            return this;
        }
    }
}
