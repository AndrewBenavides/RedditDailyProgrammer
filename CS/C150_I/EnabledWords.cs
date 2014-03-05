using System.Collections.Generic;

namespace C150_I {
    public static class EnabledWords {
        private static Dictionary<string, int> _frequency = GetFrequency();
        private static HashSet<string> _matches = new HashSet<string>(GetMatches());
        private static HashSet<string> _partialMatches = new HashSet<string>(GetPartialMatches());
        private static HashSet<char> _vowels = new HashSet<char>("aeiouAEIOU".ToCharArray());

        private static IEnumerable<string> ReadLines(string path) {
            using (var reader = new System.IO.StreamReader(path)) {
                while (!reader.EndOfStream) {
                    yield return reader.ReadLine();
                }
            }
        }

        private static Dictionary<string, int> GetFrequency() {
            var dict = new Dictionary<string, int>();
            foreach (var line in ReadLines(".\\enable1_freq.txt")) {
                var split = line.Split(' ');
                dict.Add(split[0], int.Parse(split[1]));
            }
            return dict;
        }

        private static IEnumerable<string> GetMatches() {
            return ReadLines(".\\enable1.txt");
        }

        private static IEnumerable<string> GetPartialMatches() {
            foreach (var line in GetMatches()) {
                for (int i = line.Length; i >= 0; --i) {
                    var partial = line.Substring(i);
                    yield return partial;
                }
            }
        }

        public static bool Contains(string str) {
            return _partialMatches.Contains(str);
        }

        public static int Frequency(string str) {
            int frequency = 0;
            var successful = _frequency.TryGetValue(str, out frequency);
            return frequency;
        }
        public static bool IsVowel(char c) {
            return _vowels.Contains(c);
        }

        public static bool Matches(string str) {
            return _matches.Contains(str);
        }
    }
}
