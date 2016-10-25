using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace affine
{
    class Program
    {
        static void Main(string[] args)
        {
            int a, b;
            string plainT, dec, rep;
            Boolean loop = true;
            Program p = new Program();
            do
            {
                Console.Clear();
                do
                {
                    Console.WriteLine("Affine Cipher Program");
                    Console.WriteLine("==========================================");
                    Console.Write("Enter plain text: ");
                    plainT = Console.ReadLine();
                    Console.Write("Enter key A: ");
                    a = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter key B: ");
                    b = Convert.ToInt32(Console.ReadLine());
                    if (p.GCD(a, b) != 1)
                    {
                        Console.WriteLine("GCD of key A & B must be equal to 1");
                        Console.WriteLine("Press any key to continue..."); Console.ReadKey(true);
                        Console.Clear();
                    }
                } while (p.GCD(a, b) != 1);
                Console.WriteLine("\nEncryption in progress...");
                Console.Write("Encrypted text: "); p.encrypt(plainT, a, b, out dec);
                Console.WriteLine("\nPress any key to continue...\n"); Console.ReadKey(true);
                Console.WriteLine("Decryption in progress...");
                Console.Write("Decrypted text: "); p.decrypt(dec, a, b);
                Console.WriteLine("\nPress any key to continue...\n"); Console.ReadKey(true);
                Console.Write("Would you like to restart the program?(y/N) ");
                rep = Console.ReadLine();
                rep.ToLower();
                if (rep != "y")
                    loop = false;
            } while (loop);
            Console.WriteLine("Mohamad Dean Aji Wibowo (140810140027), 2016");
            Thread.Sleep(3000);
        }
        public int GCD(int p, int q)
        {
            if (q == 0)
            {
                return p;
            }

            int r = p % q;

            return GCD(q, r);
        }
        public int Mod(int a, int n)
        {
            if (a < 0)
                return (Mod((n + a), n));
            else
                return (a % n);
        }
        public int invMod(int a, int b)
        {
            int i = b, v = 0, d = 1;
            while (a > 0)
            {
                int t = i / a, x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= b;
            if (v < 0) v = (v + b) % b;
            return v;
        }
        public void decrypt(string myString, int a, int b)
        {
            char[] buffer = myString.ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                char letter = buffer[i];
                int l = (int)letter;
                if (letter >= 'a' && letter <= 'z')
                {
                    l = l - 'a';
                    l = Mod(invMod(a, 26) * (l - b), 26);
                    l = l + 'a';
                }
                else if (letter >= 'A' && letter <= 'Z')
                {
                    l = l - 'A';
                    l = Mod(invMod(a, 26) * (l - b), 26);
                    l = l + 'A';
                }
                else
                    l = l;
                letter = (char)l;
                Console.Write(letter);
                buffer[i] = letter;
            }
        }
        private void encrypt(string myString, int a,int b, out string dec)
        {
            char[] buffer = myString.ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                char letter = buffer[i];
                int l = (int)letter;
                if (letter >= 'a' && letter <= 'z')
                {
                    l = l - 'a';
                    l = Mod((a * l) + b, 26);
                    l = l + 'a';
                }
                else if (letter >= 'A' && letter <= 'Z')
                {
                    l = l - 'A';
                    l = Mod((a * l) + b, 26);
                    l = l + 'A';
                }
                else
                    l = l;
                letter = (char)l;
                Console.Write(letter);
                buffer[i] = letter;
            }
            string s = new string(buffer);
            dec = s;
        }
    }
}
