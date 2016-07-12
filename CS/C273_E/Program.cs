using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C273_E {
    class Program {
        static void Main(string[] args) {
            string input;
            do {
                Console.Write("Input conversion parameters: ");
                input = Console.ReadLine();
                var parser = new InputParser(input);
                Console.WriteLine(parser.Process());
            } while (input != "exit");
        }

        public void ConvertTo<TIn, TOut>(TIn input, TOut output) {
            throw new NotImplementedException();
        }
    }

    public class InputParser {
        public string Input { get; set; }

        public InputParser(string input) {
            Input = input;
        }

        public string Process() {
            var validation = Validate();
            if (!string.IsNullOrWhiteSpace(validation)) return validation;

            var outputUnit = Input.Substring(Input.Length - 1, 1).SingleOrDefault();
            var inputUnit = Input.Substring(Input.Length - 2, 1).SingleOrDefault();
            var inputValue = decimal.Parse(Input.Substring(0, Input.Length - 2));
            var input = Unit.Create(inputUnit, inputValue);
            var output = input.ConvertTo(outputUnit);
            return output.Value.ToString();
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
