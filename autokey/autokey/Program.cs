using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace autokey
{
    class Program
    {
        static void Main(string[] args)
        {
            string plainT, dec, rep, key;
            Boolean loop = true;
            Program p = new Program();
            do
            {
                Console.Clear();
                Console.WriteLine("Autokey Program");
                Console.WriteLine("==========================================");
                Console.Write("Enter plain text: ");
                plainT = Console.ReadLine();
                Console.Write("Enter key\t: ");
                key = Console.ReadLine();
                Console.WriteLine("\nEncryption in progress...");
                p.encrypt(plainT, key, out dec);
                Console.WriteLine("\nPress any key to continue...\n"); Console.ReadKey(true);
                Console.WriteLine("Decryption in progress...");
                p.decrypt(dec, key);
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
        private void decrypt(string myString, string key)
        {
            char[] buffer = myString.ToCharArray();
            char[] autokey = myString.ToCharArray();
            char[] temp = key.ToCharArray();
            int j = 0, t;
            char letter;
            Console.Write("n(CT) = ");
            for (int i = 0; i < autokey.Length; i++)
            {
                if (i < temp.Length)
                    autokey[i] = temp[i];
                else
                    autokey[i] = buffer[i - temp.Length];
            }
            for (int i = 0; i < buffer.Length; i++)
            {
                letter = buffer[i];
                int l = (int)letter;
                if (l >= 'A' && l <= 'Z')
                {
                    Console.Write(l - 65 + " ");
                }
                else if (l >= 'a' && l <= 'z')
                {
                    Console.Write(l - 97 + " ");
                }
            }
            string ak = new string(autokey);
            Console.WriteLine("\nKey : " + ak);
            for (int i = 0; i < buffer.Length; i++)
            {
                letter = buffer[i];
                Console.Write("D(" + letter + ") = ");
                int l = (int)letter;
                if (j >= key.Length)
                    autokey[i] = buffer[i - key.Length];
                int k = autokey[j];
                if (l >= 'A' && l <= 'Z')
                {
                    l = l - 65;
                    k = k - 65;
                    Console.Write(l + "-" + k + " mod 26 = ");
                    l = Mod((l - k), 26);
                    Console.Write(l + "\n");
                    l = l + 65;
                }
                else if (l >= 'a' && l <= 'z')
                {
                    l = l - 97;
                    k = k - 97;
                    Console.Write(l + "-" + k + " mod 26 = ");
                    l = Mod((l - k), 26);
                    Console.Write(l + "\n");
                    l = l + 97;
                }
                letter = (char)l;
                buffer[i] = letter;
                j++;
            }
            string s = new string(buffer);
            Console.WriteLine("Decrypted text: " + s);
        }

        private void encrypt(string myString, string key, out string dec)
        {
            char[] buffer = myString.ToCharArray();
            char[] autokey = myString.ToCharArray();
            char[] temp = key.ToCharArray();
            int j = 0, t;
            char letter;
            Console.Write("n(PT) = ");
            for (int i = 0; i < autokey.Length; i++)
            {
                if (i < temp.Length)
                    autokey[i] = temp[i];
                else
                    autokey[i] = buffer[i - temp.Length];
            }
            for (int i = 0; i < buffer.Length; i++)
            {
                letter = buffer[i];
                int l = (int)letter;
                if (l >= 'A' && l <= 'Z')
                {
                    Console.Write(l - 65 + " ");
                }
                else if (l >= 'a' && l <= 'z')
                {
                    Console.Write(l - 97 + " ");
                }
            }
            string ak = new string(autokey);
            Console.WriteLine("\nKey : " + ak);
            for (int i = 0; i < buffer.Length; i++)
            {
                letter = buffer[i];
                Console.Write("E(" + letter + ") = ");
                int l = (int)letter;
                if (j == autokey.Length)
                    j = 0;
                int k = autokey[j];
                if (l >= 'A' && l <= 'Z')
                {
                    l = l - 65;
                    k = k - 65;
                    Console.Write(l + "+" + k + " mod 26 = ");
                    l = Mod((l + k), 26);
                    Console.Write(l + "\n");
                    l = l + 65;
                }
                else if (l >= 'a' && l <= 'z')
                {
                    l = l - 97;
                    k = k - 97;
                    Console.Write(l + "+" + k + " mod 26 = ");
                    l = Mod((l + k), 26);
                    Console.Write(l + "\n");
                    l = l + 97;
                }
                j++;
                letter = (char)l;
                buffer[i] = letter;
            }
            string s = new string(buffer);
            dec = s;
            Console.WriteLine("Encrypted text: " + dec);
        }
    }
}
