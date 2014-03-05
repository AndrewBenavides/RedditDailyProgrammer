using System;
using System.Collections.Generic;
using System.Linq;

namespace C150_I {
    class Reemvoweler {
        static void Main(string[] args) {
            var pairings = new List<Pairing>() {
                new Pairing("wwllfndffthstrds", "eieoeaeoi")
                ,new Pairing("llfyrbsshvtsmpntbncnfrmdbyncdt","aoouiaeaeaoeoieeoieaeoe")
                ,new Pairing("bbsrshpdlkftbllsndhvmrbndblbnsthndlts", "aieaeaeieooaaaeoeeaeoeaau")
            };
            foreach (var pair in pairings) {
                var stopwatch = new System.Diagnostics.Stopwatch();
                stopwatch.Start();
                var results = GetMostSignificantPhrases(pair.GetPhrase(), take: 2);
                PrintResults(pair, results, stopwatch.Elapsed.TotalSeconds);
                stopwatch.Stop();
            }
            Console.ReadLine();
        }

        public static IEnumerable<string> GetMostSignificantPhrases(Phrase phrase, int take) {
            var phrases = phrase.SubSignificantPhrases
                .OrderByDescending(p => p.Words.Sum(w => w.Weight))
                .ToList();
            if (phrases != null) {
                return phrases.Take(take).Select(p => p.ToString());
            } else {
                return new List<string>();
            }
        }

        private static void PrintResults(Pairing pair, IEnumerable<string> results, double seconds) {
            Console.WriteLine("Consonants:\n  {0}",pair.Consonants);
            Console.WriteLine("Vowels:\n  {0}", pair.Vowels);
            Console.WriteLine("Result:");
            foreach (var result in results) { Console.WriteLine("  {0}", result); }
            Console.WriteLine("Time:\n  {0:N4} seconds", seconds);
            Console.WriteLine();
        }

        private class Pairing {
            public string Consonants { get; set; }
            public string Vowels { get; set; }

            public Pairing(string consonants, string vowels) {
                this.Consonants = consonants;
                this.Vowels = vowels;
            }

            public Phrase GetPhrase() {
                return new Phrase(this.Consonants, this.Vowels);
            }
        }
    }
}
