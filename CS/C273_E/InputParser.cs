using System.Linq;

namespace C273_E {
    public static class InputParser {
        public static string Process(string input) {
            var validation = Validate(input);
            if (!string.IsNullOrWhiteSpace(validation)) return validation;

            var outputType = input[input.Length - 1];
            var inputType = input[input.Length - 2];
            var inputValue = decimal.Parse(input.Substring(0, input.Length - 2));

            try {
                var inputUnit = Unit.Create(inputType, inputValue);
                var outputUnit = inputUnit.ConvertTo(outputType);
                return outputUnit.Value.ToString("F2") + outputUnit.Code;
            } catch (System.Exception exception) {
                return exception.Message;
            }
        }

        public static string Validate(string input) {
            if (string.IsNullOrWhiteSpace(input))
                return "Input is empty or white space.";
            if (input.Count(c => char.IsLetter(c)) < 2)
                return "Not enough conversion unit parameters";
            if (input.Count(c => char.IsLetter(c)) > 2)
                return "Too many conversion unit parameters.";
            if (input.Count(c => char.IsNumber(c)) < 1)
                return "No amount specified.";
            return string.Empty;
        }
    }
}
