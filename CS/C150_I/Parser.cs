using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C150_I {
    public static class Parser {
        public static string GetMostRelevantPhrase(Phrase phrase) {
            var phrases = phrase.SubPhrases.OrderByDescending(sp => sp.Words.Sum(w => w.Weight)).ToList();
            var p = phrases.FirstOrDefault();
            return p == null ? string.Empty : p.ToString();
        }
    }
}
