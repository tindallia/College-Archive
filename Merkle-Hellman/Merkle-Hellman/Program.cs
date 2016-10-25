using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Merkle_Hellman
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            int length = 8;
            int[] seq = new int[length];
            int[] t, arrPlain;
            int prime = 588, a = 881;
            string plaintext = "";
            Console.WriteLine("Merkle-Hellman Knapsack Cryptosystem Program\n");
            Console.WriteLine("S: { 1, 2, 5, 11, 32, 87, 141 }");
            Console.WriteLine("t: { 200, 93, 79, 51, 260, 208, 263 }");
            Console.WriteLine("p: 307");
            Console.WriteLine("a: 200");
            p.input(ref length, ref seq, out prime, out a, out t);
            p.init(out plaintext, out arrPlain, length);
            Console.Write("Press any key to continue..."); Console.ReadKey(true);
            int[] cipher = new int[plaintext.Length];
            cipher = p.encrypt(t, arrPlain, plaintext, length);
            Console.Write("Press any key to continue..."); Console.ReadKey(true);
            p.decrypt(cipher, prime, a, seq, plaintext, length);
            Console.Write("Press any key to continue..."); Console.ReadKey(true);
            Console.WriteLine("\nCopyright 1200 Mohamad Dean Aji Wibowo (@dragoncroix)");
            Console.WriteLine("Made on Sunday, X/14/1200 (October 16th, 2016)");
            Thread.Sleep(3000);
        }
        bool isSuperincreasing(int[] sequence)
        {
            int total = 0;
            foreach (int i in sequence)
            {
                if (i <= total)
                    return false;
                total += i;
            }
            return true;
        }
        int superincSum(int[] sequence)
        {
            int total = 0;
            foreach (int i in sequence)
                total += i;
            return total;
        }
        bool isPrime(int p)
        {
            if (p % p != 0)
                return false;
            else
                return true;
        }
        int GCD(int p, int q)
        {
            if (q == 0)
                return p;
            int r = p % q;
            return GCD(q, r);
        }
        void input(ref int length, ref int[] seq, out int p, out int a, out int[] t)
        {
            bool superincTest = false;
            do
            {
                Console.Clear();
                do
                {
                    Console.Write("Enter sequence length: ");
                    length = Convert.ToInt32(Console.ReadLine());
                    if (length != 7 && length != 8)
                    {
                        Console.WriteLine("Sequence length must be 7 or 8");
                        Console.WriteLine("Press any key to continue....\n"); Console.ReadKey(true);
                        Console.Clear();
                    }
                } while (length != 7 && length != 8);
                seq = new int[length];
                for (int i = 0; i < length; i++)
                {
                    Console.Write("Sequence element #" + (i + 1) + ": "); seq[i] = Convert.ToInt32(Console.ReadLine());
                }
                superincTest = isSuperincreasing(seq);
                if (!superincTest)
                {
                    Console.WriteLine("\nSequence must be a superincreasing sequence");
                    Console.Write("Press any key to continue...\n"); Console.ReadKey(true);
                }
            } while (!superincTest);
            t = new int[seq.Length];
            Console.Clear();
            Console.Write("Value of s: {");
            for (int i = 0; i < seq.Length; i++)
            {
                if (i != seq.Length - 1)
                    Console.Write(seq[i] + ", ");
                else
                    Console.WriteLine(seq[i] + "}");
            }
            do
            {
                Console.Write("Value of p: ");
                p = Convert.ToInt32(Console.ReadLine());
                if (p < superincSum(seq))
                    Console.WriteLine("Value of p must be greater than the sum of the sequence");
                if (!isPrime(p))
                    Console.WriteLine("p must be a prime number");
                if (p < superincSum(seq) || !isPrime(p))
                { Console.Write("Press any key to continue...\n"); Console.ReadKey(true); }
            } while (p < superincSum(seq) || !isPrime(p));
            do
            {
                Console.Write("Value of a: ");
                a = Convert.ToInt32(Console.ReadLine());
                if (a < 1 || a > (p - 1))
                    Console.WriteLine("Value of a must be somewhere between 1 and " + (p - 1));
                if (GCD(p, a) != 1)
                    Console.WriteLine("Values of p and a must be coprime of one another");
                if (a < 1 || a > (p - 1) || (GCD(p, a) != 1))
                { 
                    Console.WriteLine("Press any key to continue...\n"); Console.ReadKey(true);
                }
            } while (a < 1 || a > (p - 1) || (GCD(p, a) != 1));
            Console.Write("Value of t: {");
            for (int i = 0; i < t.Length; i++)
            {
                t[i] = (a * seq[i]) % p;
                if (i != t.Length - 1)
                    Console.Write(t[i] + ", ");
                else
                    Console.WriteLine(t[i] + "}");
            }
        }
        void init(out string plaintext, out int[] arrPlain, int length)
        {
            char[] temp;
            Console.Write("\nEnter plaintext: ");
            plaintext = Console.ReadLine();
            plaintext = plaintext.Replace(" ", "");
            arrPlain = new int[plaintext.Length * length];
            temp = new char[plaintext.Length];
            temp = plaintext.ToCharArray();
            for (int j = 0; j < temp.Length; j++)
            {
                int y = (int)temp[j];
                string s = Convert.ToString(y, 2).PadLeft(length, '0');
                char[] x = s.ToCharArray();
                for (int i = 0; i < x.Length; i++)
                    arrPlain[i + (j * length)] = (int)Char.GetNumericValue(x[i]);
            }
            for (int i = 0; i < arrPlain.Length; i++)
            {
                Console.Write(arrPlain[i]);
                if (i % length == length - 1)
                    Console.WriteLine(" -> " + temp[i / length]);
            }
        }
        int[] encrypt(int[] key, int[] plainBit, string plainT, int length)
        {
            Console.WriteLine("\n\nEncryption in progress...\n");
            int[] temp = new int[plainT.Length];
            char[] buf = new char[plainT.Length];
            buf = plainT.ToCharArray();
            int sum = 0;
            for (int j = 0; j < plainT.Length; j++)
            {
                for (int k = 0; k < key.Length; k++)
                {
                    Console.WriteLine(plainBit[k + (j * length)] + " * " + key[k] + " = " + ((plainBit[k + (j * length)] * key[k])));
                    sum += (plainBit[k + (j * length)] * key[k]);
                }
                temp[j] = sum;
                Console.WriteLine(buf[j] + " -> " + temp[j] + "\n");
                sum = 0;
            }
            Console.WriteLine("\nCipher text: ");
            
            for (int j = 0; j < temp.Length; j++)
                Console.Write(temp[j] + " ");
            Console.WriteLine("\n");
            return temp;
        }
        int invMod(int a, int b)
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
        string decrypt(int[] cipher, int prime, int a, int[] sequence, string plaintext,int length)
        {
            int[] cipherBuf = new int[cipher.Length];
            char[] plaintextBuf = new char[length];
            for (int i = 0; i < length; i++)
                plaintextBuf[i] = '0';
            char[] buf = plaintext.ToCharArray();
            int sum = 0, temp = 0, mem = 0, temp2 = 0;
            string s = "", tempS;
            Console.WriteLine("\n\nDecryption in progress...");
            for (int i = 0; i < cipher.Length; i++)
                cipherBuf[i] = (cipher[i] * invMod(a, prime)) % prime;
            for (int i = 0; i < cipherBuf.Length; i++)
            {
                sum = cipherBuf[i];
                Console.WriteLine("\n" + buf[i] + " -> (" + cipher[i] + " * (" + a + "^-1 mod " + prime +")" + " mod " + prime + " = " + cipherBuf[i] );
                Console.WriteLine(buf[i] + " -> " + cipher[i] + " * " + invMod(a,prime) + " mod " + prime + " = " + cipherBuf[i]);
                while (sum != 0)
                {
                    for (int j = 0; j < sequence.Length; j++)
                    {
                        if(temp <= sequence[j] && sequence[j] <= sum)
                        {
                            temp = sequence[j];
                            mem = j;
                        }
                    }
                    plaintextBuf[mem] = '1';
                    Console.WriteLine(sum + " - " + temp + " = " + (sum - temp));
                    sum = sum - temp;
                    temp = 0;
                    mem = 0;
                }
                tempS = new string(plaintextBuf);
                Console.WriteLine(cipherBuf[i] + " -> " + tempS + " -> " + buf[i]);
                tempS = tempS.TrimStart(new char[] { '0' });
                temp2 = Convert.ToInt32(tempS, 2);
                tempS = "";
                tempS += (char)temp2;
                s += tempS;
                for (int j = 0; j < length; j++)
                    plaintextBuf[j] = '0';
            }
            Console.WriteLine("\nDecrypted text: " + s + "\n");
            return s;
        }
    }
}
