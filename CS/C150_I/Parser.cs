using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C151_I {
    public static class Parser {
        private static int LastWordLength(Phrase phrase) {
            var words = phrase.Words.Clone().Reverse();
            return words.First().ToString().Length;
        }

        public static string GetMostRelevantPhrase(Phrase phrase) {
            //var maxLen = phrase.SubPhrases.Max(p => LastWordLength(p));
            var phrases = phrase.SubPhrases.OrderByDescending(sp => sp.Words.Sum(w => w.Frequency)).ToList();
            return phrases.First().ToString();
        }
    }
}
