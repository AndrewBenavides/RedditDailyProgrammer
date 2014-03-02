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

        public static void Disemvowel(string input) {
            var consonants = new StringBuilder();
            var vowels = new StringBuilder();
            foreach (char c in input.ToLower().Replace(" ", "")) {
                var x = ("aeiou".Contains(c)) ? vowels.Append(c) : consonants.Append(c);
            } 
            Console.Write("{0}\n{1}\n{2}\n\n", input, consonants, vowels);
        }
    }
}
