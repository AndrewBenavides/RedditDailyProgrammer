using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C150_I {
    public class Phrase {
        public bool IsComplete { get; private set; }
        public bool IsDeadEnd { get; private set; }

        public IEnumerable<Phrase> NextPhrases { get { return GetNextPhrases(); } }
        public IEnumerable<Phrase> NextSignificantPhrases { get { return GetNextSignificantPhrases(); } }
        public Stack<char> RemainingConsonants { get; private set; }
        public Stack<char> RemainingVowels { get; private set; }
        public IEnumerable<Phrase> SubPartialPhrases {
            get { return GetSubPhrases(includePartial: true); }
        }
        public IEnumerable<Phrase> SubPhrases {
            get { return GetSubPhrases(includePartial: false); }
        }
        public IEnumerable<Phrase> SubSignificantPhrases {
            get { return GetSignificantSubPhrases(); }
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

        private IList<Word> GetNextWords() {
            var words = new List<Word>();
            if (!this.IsComplete) {
                var word = new Word("", this.RemainingConsonants, this.RemainingVowels);
                words = word.SubMatches.ToList();
                if (words.Count == 0) this.IsDeadEnd = true;
            }
            return words;
        }

        private IEnumerable<Phrase> GetNextPhrases() {
            IEnumerable<Phrase> phrases = new List<Phrase>();
            var words = GetNextWords();
            if (!this.IsDeadEnd && !this.IsComplete) {
                phrases = GeneratePhrases(words);
            }
            return phrases;
        }

        private IEnumerable<Phrase> GetNextSignificantPhrases() {
            IEnumerable<Phrase> phrases = new List<Phrase>();
            var words = GetNextWords();
            if (!this.IsDeadEnd && !this.IsComplete) {
                var targetWeight = words.Max(w => w.Weight);
                //weight floor can be adjusted here, but will increase processing time
                var significantWords = words
                    .Where(w => w.Weight >= targetWeight);
                var significantPhrases = GeneratePhrases(significantWords);

                var targetLength = words.Max(w => w.Length) - 1;
                //length floor can be adjusted here, but will greatly increase processing time
                var longestWords = words
                    .Where(w => (w.Length >= targetLength) && (w.Weight < targetWeight))
                    .OrderByDescending(w => w.Weight)
                    .Take(2);
                var longestPhrases = GeneratePhrases(longestWords);
                phrases = significantPhrases.Union(longestPhrases);
            }
            return phrases;
        }

        private IEnumerable<Phrase> GeneratePhrases(IEnumerable<Word> filteredMatches) {
            foreach (var match in filteredMatches) {
                var words = this.Words.Clone();
                words.Push(match);
                var phrase = new Phrase(
                    words
                    , match.RemainingConsonants.Clone()
                    , match.RemainingVowels.Clone());
                yield return phrase;
            }
        }

        private IEnumerable<Phrase> GetSubPhrases(bool includePartial) {
            if (includePartial || this.IsComplete) yield return this;
            foreach (var phrase in this.NextPhrases) {
                foreach (var subphrase in phrase.GetSubPhrases(includePartial)) {
                    yield return subphrase;
                }
            }
        }

        private IEnumerable<Phrase> GetSignificantSubPhrases() {
            if (this.IsComplete) yield return this;
            foreach (var phrase in this.NextSignificantPhrases) {
                foreach (var subphrase in phrase.SubSignificantPhrases) {
                    yield return subphrase;
                }
            }
        }

        public override string ToString() {
            return this.Words.Stringify();
        }
    }
}
