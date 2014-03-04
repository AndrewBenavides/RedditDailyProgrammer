using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C151_I {
    public class Phrase {
        public bool IsComplete { get; private set; }
        public bool IsDeadEnd { get; private set; }

        public IEnumerable<Phrase> NextPhrases { get { return GetNextPhrases(); } }
        public Stack<char> RemainingConsonants { get; private set; }
        public Stack<char> RemainingVowels { get; private set; }
        public IEnumerable<Phrase> SubPartialPhrases { get { return GetSubPartialPhrases(); } }
        public IEnumerable<Phrase> SubPhrases { get { return GetSubPhrases(); } }
        public Stack<Word> Words { get; private set; }

        public Phrase(Stack<Word> words, Stack<char> consonants, Stack<char> vowels) {
            this.RemainingConsonants = consonants;
            this.RemainingVowels = vowels;
            this.Words = words;
            this.IsComplete = (RemainingConsonants.Count == 0 && RemainingVowels.Count == 0) ? true : false;
        }

        public IEnumerable<Phrase> GetNextPhrases() {
            var nextPhrases = new List<Phrase>();
            if (!this.IsComplete) {
                var word = new Word("", this.RemainingConsonants, this.RemainingVowels);
                var submatches = word.SubMatches.ToList();
                if (!submatches.Any()) {
                    this.IsDeadEnd = true;
                } else {
                    var mostSignificant = submatches.Max(m => m.Frequency);
                    var mostSignificantMatches = submatches.Where(m => m.Frequency == mostSignificant).ToList();
                        foreach (var match in mostSignificantMatches) {
                            var words = this.Words.Clone();
                            words.Push(match);
                            var next = new Phrase(words, match.RemainingConsonants.Clone(), match.RemainingVowels.Clone());
                            nextPhrases.Add(next);
                        }
                        
                    var longest = submatches.Max(m => m.Length);
                    var longestMatches = submatches.Where(m => m.Length == longest).OrderByDescending(m => m.Frequency).Take(2);
                    foreach (var match in longestMatches) {
                        if (!mostSignificantMatches.Contains(match)) {
                            var words = this.Words.Clone();
                            words.Push(match);
                            var next = new Phrase(words, match.RemainingConsonants.Clone(), match.RemainingVowels.Clone());
                            nextPhrases.Add(next);
                        }
                    }
                }
            }
            return nextPhrases;
        }

        public IEnumerable<Phrase> GetSubPartialPhrases() {
            yield return this;
            foreach (var phrase in this.NextPhrases) {
                foreach (var subphrase in phrase.SubPartialPhrases) {
                    yield return subphrase;
                }
            }
        }

        public IEnumerable<Phrase> GetSubPhrases() {
            if (this.IsComplete) yield return this;
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
