using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C150_I {
    public static class Extensions {
        public static Stack<T> Clone<T>(this Stack<T> source) {
            return new Stack<T>(source.Reverse());
        }

        public static string Stringify(this Stack<Word> stack) {
            var clone = stack.Clone();
            var output = new StringBuilder();
            foreach (var word in clone) {
                output.Append(" ");
                output.Append(word.ToString());
            }
            return output.ToString().Trim();
        }
    }
}
