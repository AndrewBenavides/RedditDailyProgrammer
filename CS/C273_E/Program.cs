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
}
