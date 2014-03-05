using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C150_I {
    public class Word {
        private string _word;

        public int Frequency {
            get { return EnabledWords.Frequency(_word); }
        }
        public bool IsPartialMatch {
            get { return EnabledWords.Contains(_word); }
        }
        public bool IsMatch {
            get { return EnabledWords.Matches(_word); }
        }
        public int Length { get; private set; }
        public Word NextConsonant {
            get { return GetNextWordWith(this.RemainingConsonants.Clone()); }
        }
        public Word NextVowel {
            get { return GetNextWordWith(this.RemainingVowels.Clone()); }
        }
        public Stack<char> RemainingConsonants { get; private set; }
        public Stack<char> RemainingVowels { get; private set; }
        public IEnumerable<Word> SubMatches {
            get { return GetSubMatches(includePartial: false); }
        }
        public IEnumerable<Word> SubPartialMatches {
            get { return GetSubMatches(includePartial: true); }
        }
        public double Weight {
            get { return CalculateWeight(_word); }
        }

        public Word(string word, Stack<char> remainingConsonants, Stack<char> remainingVowels) {
            _word = word;
            this.Length = word.Length;
            this.RemainingConsonants = remainingConsonants;
            this.RemainingVowels = remainingVowels;
        }

        private static double CalculateWeight(string word) {
            var frequency = EnabledWords.Frequency(word);
            var weight = (frequency > 0) ? (Math.Pow(word.Length, 2) * Math.Log(frequency)) : 0;
            return weight;
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

        private IEnumerable<Word> GetSubMatches(bool includePartial) {
            if (includePartial || this.IsMatch) yield return this;
            IEnumerable<Word> words = new List<Word>();
            if (this.NextConsonant != null) words = 
                words.Union(this.NextConsonant.GetSubMatches(includePartial));
            if (this.NextVowel != null) words = 
                words.Union(this.NextVowel.GetSubMatches(includePartial));

            foreach (var word in words) { yield return word; }
        }

        public override string ToString() {
            return _word;
        }
    }
}
