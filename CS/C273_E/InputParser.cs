using System.Linq;

namespace C273_E {
    public class InputParser {
        public string Input { get; }

        public InputParser(string input) {
            Input = input;
        }

        public string Process() {
            var validation = Validate();
            if (!string.IsNullOrWhiteSpace(validation)) return validation;

            var outputUnit = Input.Substring(Input.Length - 1, 1).SingleOrDefault();
            var inputUnit = Input.Substring(Input.Length - 2, 1).SingleOrDefault();
            var inputValue = decimal.Parse(Input.Substring(0, Input.Length - 2));

            try {
                var input = Unit.Create(inputUnit, inputValue);
                var output = input.ConvertTo(outputUnit);
                return output.Value.ToString();
            } catch (System.Exception exception) {
                return exception.Message;
            }
        }

        public string Validate() {
            if (string.IsNullOrWhiteSpace(Input)) return "Input is empty or white space.";
            if (Input
                .AsEnumerable()
                .Count(c => char.IsLetter(c)) < 2) return "Not enough conversion unit parameters";
            if (Input
                .AsEnumerable()
                .Count(c => char.IsLetter(c)) > 2) return "Too many conversion unit parameters.";
            if (Input
                .AsEnumerable()
                .Count(c => char.IsNumber(c)) < 1) return "No amount specified.";
            return string.Empty;
        }
    }
}
