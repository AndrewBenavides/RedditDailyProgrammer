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
            //var rev = new Reemvoweler("wwllfndffthstrds", "eieoeaeoi");
            Console.WriteLine(EnabledWords.Matches("asteroids").ToString());
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

    public class Word {
        private string _word;
        public bool IsPartiallyFound { get; private set; }
        public bool IsFound { get; private set; }
        public Stack<char> RemainingConsonants { get; private set; }
        public Stack<char> RemainingVowels { get; set; }

        public Word NextConsonant { get; private set; }
        public Word NextVowel { get; private set; }

        public Word(string word, Stack<char> remainingConsonants, Stack<char> remainingVowels) {
            _word = word;
            this.RemainingConsonants = remainingConsonants;
            this.RemainingVowels = remainingVowels;
            this.IsPartiallyFound = EnabledWords.Contains(_word);
            this.IsFound = this.IsPartiallyFound ? EnabledWords.Matches(_word) : false;
            this.NextConsonant = GetNextConsonant(this.RemainingConsonants);
            this.NextVowel = GetNextVowel(this.RemainingVowels);
        }

        private Word GetNextConsonant(Stack<char> consonants) {
            if (this.IsPartiallyFound) {
                var word = consonants.Any() ? consonants.Pop() + _word : _word;
                var next = new Word(word, consonants, this.RemainingVowels);
                return next;
            } else {
                return null;
            }
        }

        private Word GetNextVowel(Stack<char> vowels) {
            if (this.IsPartiallyFound) {
                var word = vowels.Any() ? vowels.Pop() + _word : _word;
                var next = new Word(word, this.RemainingConsonants, vowels);
                return next;
            } else {
                return null;
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

    //class Reemvoweler {
    //    private string con;
    //    private string vow;
    //    Stack conStack;
    //    Stack vowStack;

    //    public Reemvoweler(string consonants, string vowels) {
    //        con = consonants;
    //        vow = vowels;
    //        conStack = new Stack(con.ToList());
    //        vowStack = new Stack(vow.ToList());
    //        FindWords();
    //    }

    //    public bool IsInEnabledWords(string str) {
    //        var found = false;
    //        //while (!found) { }
    //        foreach (var line in ReadLines()) {
    //            if (str.Length <= line.Length) {
    //                if (line.Substring(line.Length - str.Length) == str) {
    //                    found = true;
    //                    break;
    //                }
    //            }
    //        }
    //        return found;
    //    }

    //    public bool IsEntireWord(string str) {
    //        foreach (var line in ReadLines()) {
    //            if (line == str) {
    //                return true;
    //            }
    //        }
    //        return false;
    //    }

    //    public bool IsVowel(char letter) {
    //        return "aeiouAEIOU".Contains(letter);
    //    }

    //    public IEnumerable<string> ReadLines() {
    //        var reader = new System.IO.StreamReader(".\\enable1.txt");
    //        while (!reader.EndOfStream) {
    //            yield return reader.ReadLine();
    //        }
    //    }

    //    public string FindWords() {
    //        var word = string.Empty;
    //        var phrase = string.Empty;
    //        var takeVowel = false;
    //        char currentLetter;
    //        while (conStack.Count > 0 || vowStack.Count > 0) {
    //            currentLetter = (char)(!takeVowel ? conStack.Pop() : vowStack.Pop());
    //            word = currentLetter + word;
    //            if (IsInEnabledWords(word)) {
    //                if (IsEntireWord(word)) {
    //                    phrase = word + " " + phrase;
    //                    word = "";
    //                    takeVowel = false;
    //                } else {
    //                    takeVowel = false;
    //                }
    //            } else {
    //                word = word.Substring(1);
    //                if (IsVowel(currentLetter)) {
    //                    vowStack.Push(currentLetter);
    //                    takeVowel = false;
    //                } else {
    //                    conStack.Push(currentLetter);
    //                    takeVowel = true;
    //                }
    //            }
    //        }
    //        return phrase;
    //    }
    //}
}
