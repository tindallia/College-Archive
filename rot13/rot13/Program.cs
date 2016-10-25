using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace rot13
{
    class Program
    {
        static void Main(string[] args)
        {
            int key = 13;
            string plainT, dec, rep;
            Boolean loop = true;
            Program p = new Program();
            do
            {
                Console.Clear();
                Console.WriteLine("ROT13 Cipher Program");
                Console.WriteLine("==========================================");
                Console.Write("Enter plain text: ");
                plainT = Console.ReadLine();
                Console.WriteLine("\nEncryption in progress...");
                Console.Write("Encrypted text: "); p.encrypt(plainT, key, out dec);
                Console.WriteLine("\nPress any key to continue...\n"); Console.ReadKey(true);
                Console.WriteLine("Decryption in progress...");
                Console.Write("Decrypted text: "); p.decrypt(dec, key);
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
        public int Mod(int a, int n)
        {
            if (a < 0)
                return (Mod((n + a), n));
            else
                return (a % n);
        }
        public void decrypt(string myString, int key)
        {
            char[] buffer = myString.ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                char letter = buffer[i];
                int l = (int)letter;
                if (letter >= 'a' && letter <= 'z')
                {
                    l = l - 'a';
                    l = Mod((l - key), 26);
                    l = l + 'a';
                }
                else if (letter >= 'A' && letter <= 'Z')
                {
                    l = l - 'A';
                    l = Mod((l - key), 26);
                    l = l + 'A';
                }
                letter = (char)l;
                Console.Write(letter);
                buffer[i] = letter;
            }
        }
        private void encrypt(string myString, int key, out string dec)
        {
            char[] buffer = myString.ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                char letter = buffer[i];
                int l = (int)letter;
                if (letter >= 'a' && letter <= 'z')
                {
                    l = l - 'a';
                    l = Mod((l + key), 26);
                    l = l + 'a';
                }
                else if (letter >= 'A' && letter <= 'Z')
                {
                    l = l - 'A';
                    l = Mod((l + key), 26);
                    l = l + 'A';
                }
                letter = (char)l;
                Console.Write(letter);
                buffer[i] = letter;
            }
            string s = new string(buffer);
            dec = s;
        }
    }
}
