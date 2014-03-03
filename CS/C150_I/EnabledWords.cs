using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C151_I {
    public static class EnabledWords {
        private static string _enabledWordsFilePath = ".\\enable1.txt";
        private static string[] _enabledWords = GetReversedArray();

        private static string[] GetReversedArray() {
            //http://stackoverflow.com/a/500930
            var list = new SortedList<string, string>();
            foreach (var line in ReadLines()) {
                list.Add(line.Reverse2(), line);
            }
            var array = list.Select(kvp => kvp.Key).ToArray();
            return array;
        }

        public static bool Contains(string str) {
            var rev = str.Reverse2();
            var nearest = Array.BinarySearch(_enabledWords, rev);
            if (nearest > -1 || _enabledWords[Math.Abs(nearest) - 1].Contains(rev)) {
                return true;
            } else {
                return false;
            }
        }

        public static bool Matches(string str) {
            return Array.BinarySearch(_enabledWords, str.Reverse2()) > -1 ? true : false;
        }

        private static IEnumerable<string> ReadLines() {
            using (var reader = new System.IO.StreamReader(_enabledWordsFilePath)) {
                while (!reader.EndOfStream) {
                    yield return reader.ReadLine();
                }
            }
        }

    }
}
