﻿using System;

namespace C273_E {
    class Program {
        static void Main(string[] args) {
            string input;
            Console.WriteLine(@"Input ""quit"" to terminate the program.");
            do {
                Console.Write("Input conversion parameters: ");
                input = Console.ReadLine();
                if (input == "quit") break;
                Console.WriteLine(InputParser.Process(input));
            } while (true);
        }
    }
}
