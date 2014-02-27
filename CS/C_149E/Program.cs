using System;
using System.Linq;
using System.Text;

namespace C_149E {
    class Program {
        static void Main(string[] args) {
            Disemvowel("two drums and a cymbal fall off a cliff");
            Disemvowel("all those who believe in psychokinesis raise my hand");
            Disemvowel("did you hear about the excellent farmer who was outstanding in his field");
            //Console.ReadLine();
        }

        public static bool IsVowel(char c) {
            return "aeiou".Contains(c);
        }

        public static Tuple<string, string> Disemvoweler(string input) {
            var consonants = new StringBuilder();
            var vowels = new StringBuilder();

            foreach (char c in input.ToLower().Replace(" ", "")) {
                if (IsVowel(c)) {
                    vowels.Append(c);
                } else {
                    consonants.Append(c);
                }
            }

            return new Tuple<string, string>(consonants.ToString(), vowels.ToString());
        }

        public static void Disemvowel(string input) {
            var dis = Disemvoweler(input);
            Console.Write("{0}\n{1}\n{2}\n\n", input, dis.Item1, dis.Item2);
        }
    }
}
