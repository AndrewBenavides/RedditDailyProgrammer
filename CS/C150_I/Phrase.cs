using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C151_I {
    public class Phrase {
        private Stack<char> _consonants;
        private Stack<char> _vowels;

        public Stack<Word> Words { get; private set; }

        public bool IsComplete { get; private set; }
        public bool IsDeadEnd { get; private set; }

        public IList<Phrase> NextPhrases { get; private set; }
        public IEnumerable<Phrase> SubPartialPhrases { get; private set; }
        public IEnumerable<Phrase> SubPhrases { get; private set; }

        public Phrase(Stack<Word> words, Stack<char> consonants, Stack<char> vowels) {
            _consonants = consonants;
            _vowels = vowels;
            this.Words = words;
            this.IsComplete = (!_consonants.Any() && !_vowels.Any()) ? true : false;

            this.NextPhrases = GetNextPhrases();
            this.SubPartialPhrases = GetSubPartialPhrases();
            this.SubPhrases = GetSubPhrases();
        }

        public IList<Phrase> GetNextPhrases() {
            var nextPhrases = new List<Phrase>();
            if (!this.IsComplete) {
                var word = new Word("", _consonants, _vowels);
                var submatches = word.SubMatches;
                if (!submatches.Any()) {
                    this.IsDeadEnd = true;
                } else {
                    foreach (var match in submatches) {
                        var words = this.Words.Clone();
                        words.Push(match);
                        var next = new Phrase(words, match.RemainingConsonants.Clone(), match.RemainingVowels.Clone());
                        nextPhrases.Add(next);
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
            var phrases = this.SubPartialPhrases.Where(p => p.IsComplete).AsEnumerable();
            return phrases;
        }

        public override string ToString() {
            return this.Words.Stringify();
        }
    }
}
