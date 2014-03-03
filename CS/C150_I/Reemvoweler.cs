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

            var phrase = new Phrase(new Stack<Word>(), new Stack<char>(con), new Stack<char>(vow));

        }
    }
}
