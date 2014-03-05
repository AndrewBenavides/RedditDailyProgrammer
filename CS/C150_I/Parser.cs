using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C150_I {
    public static class Parser {
        public static IEnumerable<string> GetMostRelevantPhrases(Phrase phrase, int take) {
            var phrases = phrase.SubSignificantPhrases.OrderByDescending(sp => sp.Words.Sum(w => w.Weight)).ToList();
            if (phrases != null) {
                return phrases.Take(take).Select(p => p.ToString());
            } else {
                return new List<string>();
            }
        }
    }
}
