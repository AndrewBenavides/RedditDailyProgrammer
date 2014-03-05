using System;
using System.Collections.Generic;
using System.Linq;

namespace C150_I {
    public class Word {
        private string _word;

        public int Frequency {
            get { return EnabledWords.Frequency(_word); }
        }
        public bool IsPartial {
            get { return EnabledWords.Contains(_word); }
        }
        public bool IsComplete {
            get { return EnabledWords.Matches(_word); }
        }
        public int Length { get; private set; }
        public Word NextConsonant {
            get { return GetNextWordWith(this.RemainingConsonants); }
        }
        public Word NextVowel {
            get { return GetNextWordWith(this.RemainingVowels); }
        }
        public IList<char> RemainingConsonants { get; private set; }
        public IList<char> RemainingVowels { get; private set; }
        public IEnumerable<Word> SubPartialWords {
            get { return GetSubMatches(includePartial: true); }
        }
        public IEnumerable<Word> SubWords {
            get { return GetSubMatches(includePartial: false); }
        }
        public double Weight {
            get { return CalculateWeight(_word); }
        }

        public Word(string word, IList<char> remainingConsonants, IList<char> remainingVowels) {
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

        private Word GetNextWordWith(IList<char> chars) {
            if (chars.Count > 0 && this.IsPartial) {
                var c = chars[0];
                var word = c + _word;
                IList<char> consonants;
                IList<char> vowels;
                if (EnabledWords.IsVowel(c)) {
                    consonants = this.RemainingConsonants;
                    vowels = chars.Skip(1).ToList();
                } else {
                    consonants = chars.Skip(1).ToList();
                    vowels = this.RemainingVowels;
                }
                Word next = new Word(word, consonants, vowels);
                return next;
            } else {
                return null;
            }
        }

        private IEnumerable<Word> GetSubMatches(bool includePartial) {
            if (includePartial || this.IsComplete) yield return this;
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
