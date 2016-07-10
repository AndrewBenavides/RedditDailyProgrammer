using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace C274_E {
    public class BealeCipher {
        public static void CorrectValues(IList<string> values) {
            values.Insert(0, string.Empty);
            values.Insert(155, "a");
            values.RemoveAt(242);
            for (int i = 0; i < 10; i++) values.RemoveAt(480);
            values.RemoveAt(620);
            values.RemoveAt(666);
            values[810] = "y";
            values[1004] = "x";
        }

        public static List<string> LoadValues(string dictionaryPath) {
            using (var file = new StreamReader(dictionaryPath))
                return file.ReadToEnd()
                    .Split(' ')
                    .ToList();
        }

        public static List<int> LoadCiphers(string cipherPath) {
            using (var file = new StreamReader(cipherPath)) {
                return file.ReadToEnd()
                    .Split(',')
                    .Select(c => int.Parse(c))
                    .ToList();
            }
        }

        public static string Decipher(string dictionaryPath, string cipherPath) {
            var values = LoadValues(dictionaryPath);
            CorrectValues(values);
            var ciphers = LoadCiphers(cipherPath);
            var deciphered = string.Join(string.Empty, ciphers.Select(c => {
                var v = values[c];
                return v.First();
            }));
            return deciphered;
        }
    }
}
