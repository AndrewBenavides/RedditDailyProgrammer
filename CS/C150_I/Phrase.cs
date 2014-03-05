using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C150_I {
    public class Phrase {
        public bool IsComplete { get; private set; }
        public bool IsDeadEnd { get; private set; }

        public IEnumerable<Phrase> NextSignificantPhrases { get { return GetNextSignificantPhrases(); } }
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

        public IEnumerable<Phrase> GetNextPhrasesWith(Func<List<Word>, IEnumerable<Word>> filterFunc) {
            IEnumerable<Phrase> phrases = new List<Phrase>();
            if (!this.IsComplete) {
                var word = new Word("", this.RemainingConsonants, this.RemainingVowels);
                var submatches = word.SubMatches.ToList();
                if (submatches.Count == 0) {
                    this.IsDeadEnd = true;
                } else {
                    //var maxWeight = submatches.Max(w => w.Weight);
                    //var filteredMatches = word.SubMatches.Where(w => wherePredicate(w, maxWeight));
                    var filteredMatches = filterFunc(submatches);
                    phrases = GeneratePhrases(filteredMatches);
                }
            }
            return phrases;
        }

        public IEnumerable<Phrase> GetNextSignificantPhrases() {
            //Func<List<Word>, IEnumerable<Word>> weightFilter = words => {
            //    var significantWeight = words.Max(w => w.Weight);
            //    return words.Where(w => w.Weight >= significantWeight);
            //};
            //Func<List<Word>, IEnumerable<Word>> lengthFilter = words => {
            //    var longest = words.Max(w => w.Length);
            //    var significantWeight = words.Max(w => w.Weight);
            //    return words
            //        .Where(w => (w.Length >= longest - 1) && (w.Weight < significantWeight))
            //        .OrderByDescending(w => w.Weight)
            //        .Take(2)
            //        .ToList();
            //};
            //var phrases = GetNextPhrasesWith(weightFilter);
            //phrases = phrases.Union(GetNextPhrasesWith(lengthFilter));
            IEnumerable<Phrase> phrases = new List<Phrase>();
            if (!this.IsComplete) {
                var word = new Word("", this.RemainingConsonants, this.RemainingVowels);
                var words = word.SubMatches.ToList();
                if (words.Count == 0) {
                    this.IsDeadEnd = true;
                } else {
                    var targetWeight = words.Max(m => m.Weight); 
                    //weight floor can be adjusted here, but will increase processing time
                    var significantWords = words
                        .Where(m => m.Weight >= targetWeight);
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

        private IEnumerable<Phrase> GetLongestNonSignificantPhrases(List<Word> submatches, List<Word> matchesToExclude) {
                var longest = submatches.Max(m => m.Length);
                var longestMatches = submatches
                    .Where(m => (m.Length >= longest - 1) && (!matchesToExclude.Contains(m)))
                    .OrderByDescending(m => m.Weight)
                    .Take(2)
                    .ToList();
                var phrases = GeneratePhrases(longestMatches);
                return phrases;
            }

        private IEnumerable<Phrase> GetSubPhrases(bool includePartial) {
            if (includePartial || this.IsComplete) yield return this;
            foreach (var phrase in this.NextSignificantPhrases) {
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
