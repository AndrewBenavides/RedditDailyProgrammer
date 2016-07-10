using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C274_E {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine(BealeCipher.Decipher(@".\Resources\DoI.txt", @".\Resources\Cipher.txt"));
            Console.ReadLine();
        }
    }
}
