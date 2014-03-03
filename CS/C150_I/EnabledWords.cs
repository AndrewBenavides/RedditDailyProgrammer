using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C151_I {
    public static class EnabledWords {
        private static string _reverseEnabledWordsFilePath = ".\\enable1_rev.txt";
        private static string[] _reverseEnabledWords = GetReversedArray();
        private static char[] _vowels = GetVowels();

        private static string[] GetReversedArray() {
            //http://stackoverflow.com/a/500930
            return ReadLines().ToArray();
        }

        private static IEnumerable<string> ReadLines() {
            using (var reader = new System.IO.StreamReader(_reverseEnabledWordsFilePath)) {
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

        public static bool Matches(string str) {
            return Array.BinarySearch(_reverseEnabledWords, str.Reverse2()) > -1 ? true : false;
        }

        public static bool IsVowel(char c) {
            return Array.BinarySearch(_vowels, c) > -1 ? true : false;
        }
    }
}
