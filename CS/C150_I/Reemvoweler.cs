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
            var phrase1 = new Phrase(new Stack<Word>(), new Stack<char>("bbsrshpdlkftbllsndhvmrbndblbnsthndlts"), new Stack<char>("aieaeaeieooaaaeoeeaeoeaau")); //babies
            var phrase2 = new Phrase(new Stack<Word>(), new Stack<char>("wwllfndffthstrds"), new Stack<char>("eieoeaeoi")); //asteroids
            var phrase3 = new Phrase(new Stack<Word>(), new Stack<char>("llfyrbsshvtsmpntbncnfrmdbyncdt"), new Stack<char>("aoouiaeaeaoeoieeoieaeoe")); //biases
            var a1 = Parser.GetMostRelevantPhrase(phrase1);
            var a2 = Parser.GetMostRelevantPhrase(phrase2);
            var a3 = Parser.GetMostRelevantPhrase(phrase3);
        }
    }
}
