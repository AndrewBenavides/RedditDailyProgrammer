using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C150_I {
    public class Phrase {
        public bool IsComplete { get; private set; }
        public bool IsDeadEnd { get; private set; }

        public IEnumerable<Phrase> NextPhrases { get { return GetSignificantPhrases(); } }
        public Stack<char> RemainingConsonants { get; private set; }
        public Stack<char> RemainingVowels { get; private set; }
        public IEnumerable<Phrase> SubPartialPhrases {
            get { return GetSubPhrases(includePartial: true); }
        }
        public IEnumerable<Phrase> SubPhrases {
            get { return GetSubPhrases(includePartial: false); }
        }
        public Stack<Word> Words { get; private set; }

        public Phrase(string consonants, string vowels) {
            Construct(new Stack<Word>(), new Stack<char>(consonants), new Stack<char>(vowels));
        }

        private Phrase(Stack<Word> words, Stack<char> consonants, Stack<char> vowels) {
            Construct(words, consonants, vowels);
        }

        private void Construct(Stack<Word> words, Stack<char> consonants, Stack<char> vowels) {
            this.RemainingConsonants = consonants;
            this.RemainingVowels = vowels;
            this.Words = words;
            this.IsComplete =
                (RemainingConsonants.Count == 0 && RemainingVowels.Count == 0) ? true : false;
        }

        public IEnumerable<Phrase> GetNextPhrases() {
            IEnumerable<Phrase> phrases = new List<Phrase>();
            if (!this.IsComplete) {
                var word = new Word("", this.RemainingConsonants, this.RemainingVowels);
                var submatches = word.SubMatches.ToList();
                if (submatches.Count == 0) {
                    this.IsDeadEnd = true;
                } else {
                    phrases = GetPhrases(submatches);
                }
            }
            return phrases;
        }

        public IEnumerable<Phrase> GetSignificantPhrases() {
            IEnumerable<Phrase> phrases = new List<Phrase>();
            if (!this.IsComplete) {
                var word = new Word("", this.RemainingConsonants, this.RemainingVowels);
                var submatches = word.SubMatches.ToList();
                if (submatches.Count == 0) {
                    this.IsDeadEnd = true;
                } else {
                    var mostSignificantWeight = submatches.Max(m => m.Weight);
                    var significantMatches = submatches
                        .Where(m => m.Weight == mostSignificantWeight)
                        .ToList();
                    var significantPhrases = GetPhrases(significantMatches);
                    var longestPhrases = GetLongestNonSignificantPhrases(submatches, significantMatches);
                    phrases = significantPhrases.Union(longestPhrases);
                }
            }
            return phrases;
        }

        private IEnumerable<Phrase> GetPhrases(List<Word> submatches) {
            foreach (var match in submatches) {
                var words = this.Words.Clone();
                words.Push(match);
                var phrase = new Phrase(
                    words
                    , match.RemainingConsonants.Clone()
                    , match.RemainingVowels.Clone());
                yield return phrase;
            }
        }

        private IEnumerable<Phrase> GetLongestNonSignificantPhrases(
            List<Word> submatches
            , List<Word> matchesToExclude) {
                var longest = submatches.Max(m => m.Length);
                var longestMatches = submatches
                    .Where(m => (m.Length >= longest - 1) && (!matchesToExclude.Contains(m)))
                    .OrderByDescending(m => m.Weight)
                    .Take(2)
                    .ToList();
                var phrases = GetPhrases(longestMatches);
                return phrases;
            }

        private IEnumerable<Phrase> GetSubPhrases(bool includePartial) {
            if (includePartial || this.IsComplete) yield return this;
            foreach (var phrase in this.NextPhrases) {
                foreach (var subphrase in phrase.SubPhrases) {
                    yield return subphrase;
                }
            }
        }

        public override string ToString() {
            return this.Words.Stringify();
        }
    }
}
