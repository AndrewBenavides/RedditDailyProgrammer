using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C151_I {
    public class Word {
        private string _word;

        public double Frequency { get { return Math.Log(EnabledWords.Frequency(_word)) * this.Length * 2; } }
        public bool IsPartialMatch { get { return EnabledWords.Contains(_word); } }
        public bool IsMatch { get { return EnabledWords.Matches(_word); } }
        public int Length { get; private set; }
        public Word NextConsonant { get { return GetNextWordWith(this.RemainingConsonants.Clone()); } }
        public Word NextVowel { get { return GetNextWordWith(this.RemainingVowels.Clone()); } }
        public Stack<char> RemainingConsonants { get; private set; }
        public Stack<char> RemainingVowels { get; set; }
        public IEnumerable<Word> SubMatches { get { return GetSubMatches(); } }
        public IEnumerable<Word> SubPartialMatches { get { return GetSubPartialMatches(); } }

        public Word(string word, Stack<char> remainingConsonants, Stack<char> remainingVowels) {
            _word = word;
            this.Length = word.Length;
            this.RemainingConsonants = remainingConsonants;
            this.RemainingVowels = remainingVowels;
        }

        private Word GetNextWordWith(Stack<char> charStack) {
            if (charStack.Count > 0 && this.IsPartialMatch) {
                var c = charStack.Pop();
                var word = c + _word;
                Word next;
               if (EnabledWords.IsVowel(c)) {
                    next = new Word(word, this.RemainingConsonants.Clone(), charStack);
                } else {
                    next = new Word(word, charStack, this.RemainingVowels.Clone());
                }
                return next;
            } else {
                return null;
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

        private IEnumerable<Word> GetSubPartialMatches() {
            yield return this;
            if (this.NextConsonant != null) {
                foreach (var match in this.NextConsonant.SubPartialMatches) { yield return match; }
            }
            if (this.NextVowel != null) {
                foreach (var match in this.NextVowel.SubPartialMatches) { yield return match; }
            }
        }

        public override string ToString() {
            return _word;
        }
    }
}
