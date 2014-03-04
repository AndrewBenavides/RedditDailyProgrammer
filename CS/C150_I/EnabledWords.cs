using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C150_I {
    public static class EnabledWords {
        private static string[] _reverseEnabledWords = GetReversedArray();
        private static Dictionary<string, int> _frequency = GetFrequency();
        private static char[] _vowels = GetVowels();

        private static Dictionary<string, int> GetFrequency() {
            var dict = new Dictionary<string, int>();
            foreach (var line in ReadLines(".\\enable1_freq.txt")) {
                var split = line.Split(' ');
                dict.Add(split[0], int.Parse(split[1]));
            }
            return dict;
        }

        private static string[] GetReversedArray() {
            //http://stackoverflow.com/a/500930
            return ReadLines(".\\enable1_rev.txt").ToArray();
        }

        private static IEnumerable<string> ReadLines(string path) {
            using (var reader = new System.IO.StreamReader(path)) {
                while (!reader.EndOfStream) {
                    yield return reader.ReadLine();
                }
            }
        }

        private static char[] GetVowels() {
            var chars = "aeiouAEIOU".ToCharArray();
            Array.Sort(chars);
            return chars;
        }

        public static bool Contains(string str) {
            var rev = str.Reverse2();
            var nearest = Array.BinarySearch(_reverseEnabledWords, rev);
            if (nearest > -1 || _reverseEnabledWords[Math.Abs(nearest) - 1].Contains(rev)) {
                return true;
            } else {
                return false;
            }
        }

        public static int Frequency(string str) {
            int frequency = 0;
            var successful = _frequency.TryGetValue(str, out frequency);
            return frequency;
        }

        public static bool Matches(string str) {
            return Array.BinarySearch(_reverseEnabledWords, str.Reverse2()) > -1 ? true : false;
        }

        public static bool IsVowel(char c) {
            return Array.BinarySearch(_vowels, c) > -1 ? true : false;
        }
    }
}
