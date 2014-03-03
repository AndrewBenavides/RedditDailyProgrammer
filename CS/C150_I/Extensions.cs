﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C151_I {
    public static class Extensions {
        public static Stack<T> Clone<T>(this Stack<T> source) {
            return new Stack<T>(source.Reverse());
        }

        public static string Reverse2(this string str) {
            var builder = new StringBuilder();
            var chars = str.ToCharArray();
            Array.Reverse(chars);
            return builder.Append(chars).ToString();
        }

        public static string Stringify(this Stack<Word> stack) {
            var clone = stack.Clone();
            var output = new StringBuilder();
            foreach (var word in clone) {
                output.Append(" " + word.ToString());
            }
            return output.ToString().Trim();
        }
    }
}