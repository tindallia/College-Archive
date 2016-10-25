using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    class Program
    {
        static void Main(string[] args)
        {
            int prime1, prime2, e, d, n;
            string plaintext;
            func f = new func();
            f.init(out prime1, out prime2, out e, out d, out n, out plaintext);
            double[] ciphertext = f.encrypt(plaintext, e, n);
            Console.Write("Encrypted text: ");
            foreach (int i in ciphertext)
                Console.Write(i);
            Console.WriteLine();
            string decrypted = f.decrypt(ciphertext, d, n);
            Console.WriteLine("Decrypted text: " + decrypted);
            Console.ReadKey(true);
        }
    }
}
