using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C151_I {
    public class Word {
        private string _word;
        public bool IsPartialMatch { get; private set; }
        public bool IsMatch { get; private set; }
        public Stack<char> RemainingConsonants { get; private set; }
        public Stack<char> RemainingVowels { get; set; }
        public int MatchingLength { get { return this.IsMatch ? _word.Length : 0; } }

        public Word NextConsonant { get; private set; }
        public Word NextVowel { get; private set; }
        public IEnumerable<Word> SubPartialMatches { get; private set; }
        public IEnumerable<Word> SubMatches { get; private set; }

        public Word(string word, Stack<char> remainingConsonants, Stack<char> remainingVowels) {
            _word = word;
            this.RemainingConsonants = remainingConsonants;
            this.RemainingVowels = remainingVowels;
            this.IsPartialMatch = EnabledWords.Contains(_word);
            this.IsMatch = this.IsPartialMatch ? EnabledWords.Matches(_word) : false;
            this.NextConsonant = GetNextWordWith(this.RemainingConsonants.Clone());
            this.NextVowel = GetNextWordWith(this.RemainingVowels.Clone());
            this.SubPartialMatches = GetSubPartialMatches();
            this.SubMatches = GetSubMatches();
        }

        private Word GetNextWordWith(Stack<char> charStack) {
            if (this.IsPartialMatch && charStack.Any()) {
                var c = charStack.Pop();
                var word = c + _word;
                Word next;
                if ("aeiouAEIOU".Contains(c)) {
                    next = new Word(word, this.RemainingConsonants.Clone(), charStack);
                } else {
                    next = new Word(word, charStack, this.RemainingVowels.Clone());
                }
                return next;
            } else {
                return null;
            }
        }

        private IEnumerable<Word> GetSubPartialMatches() {
            yield return this;
            if (this.NextConsonant != null) {
                foreach (var match in this.NextConsonant.SubPartialMatches) { yield return match; }
            }
            if (this.NextVowel != null) {
                foreach (var match in this.NextVowel.SubPartialMatches) { yield return match; }
            }
        }

        private IEnumerable<Word> GetSubMatches() {
            if (this.IsMatch) yield return this;
            if (this.NextConsonant != null) {
                foreach (var match in this.NextConsonant.SubMatches) { yield return match; }
            }
            if (this.NextVowel != null) {
                foreach (var match in this.NextVowel.SubMatches) { yield return match; }
            }
        }

        public override string ToString() {
            return _word;
        }
    }
}
