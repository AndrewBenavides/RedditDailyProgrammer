using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C151_I {
    class Program {
        static void Main(string[] args) {
            var con = "wwllfndffthstrds";
            var vow = "eieoeaeoi";

            var word = new Word("", new Stack<char>(con), new Stack<char>(vow));
            var matches = word.SubPartialMatches.ToList();
            var fullMatches = word.SubMatches.ToList();
        }


    }

    public static class EnabledWords {
        private static string _enabledWordsFilePath = ".\\enable1.txt";

        public static bool Contains(string str) {
            foreach (var line in ReadLines()) {
                if (str.Length <= line.Length) {
                    if (line.Substring(line.Length - str.Length) == str) {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool Matches(string str) {
            foreach (var line in ReadLines()) {
                if (line == str) {
                    return true;
                }
            }
            return false;
        }

        private static IEnumerable<string> ReadLines() {
            using (var reader = new System.IO.StreamReader(_enabledWordsFilePath)) {
                while (!reader.EndOfStream) {
                    yield return reader.ReadLine();
                }
            }
        }

    }

    public static class StackExtensions {
        public static Stack<T> Clone<T>(this Stack<T> source) {
            return new Stack<T>(source.Reverse());
        }
    }

    public class Word {
        private string _word;
        public bool IsPartialMatch { get; private set; }
        public bool IsMatch { get; private set; }
        public Stack<char> RemainingConsonants { get; private set; }
        public Stack<char> RemainingVowels { get; set; }
        public int MatchingLength { get { return this.IsMatch ? _word.Length : 0; } }

        public Word NextConsonant { get; private set; }
        public Word NextVowel { get; private set; }
        public IEnumerable<Word> SubPartialMatches { get { return this.GetSubPartialMatches(); } }
        public IEnumerable<Word> SubMatches { get { return this.GetSubMatches(); } }

        public Word(string word, Stack<char> remainingConsonants, Stack<char> remainingVowels) {
            _word = word;
            this.RemainingConsonants = remainingConsonants;
            this.RemainingVowels = remainingVowels;
            this.IsPartialMatch = EnabledWords.Contains(_word);
            this.IsMatch = this.IsPartialMatch ? EnabledWords.Matches(_word) : false;
            this.NextConsonant = GetNextWordWith(this.RemainingConsonants.Clone());
            this.NextVowel = GetNextWordWith(this.RemainingVowels.Clone());
            //this.SubMatches = GetSubMatches();
        }

        private Word GetNextWordWith(Stack<char> charStack) {
            if(this.IsPartialMatch) {
                var c = charStack.Any() ? charStack.Pop() : char.MinValue;
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


    public class WordFinder {
        public WordFinder() {

        }
    }
}
