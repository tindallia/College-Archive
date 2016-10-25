using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace RSA
{
    public class func
    {
        public bool isPrime(int p)
        {
            if (p <= 1)
                return false;
            else if (p <= 3)
                return true;
            else if (p % 2 == 0 || p % 3 == 0)
                return false;
            int i = 5;
            while (i * i <= p)
            {
                if (p % i == 0 || p % (i + 2) == 0)
                    return false;
                i += 6;
            }
            return true;
        }
        public bool coprime(long u, long v)
        {
            if (((u | v) & 1) == 0)
                return false;
            while ((u & 1) == 0) u >>= 1;
            if (u == 1)
                return true;
            do
            {
                while ((v & 1) == 0) v >>= 1;
                if (v == 1)
                    return true;
                if (u > v)
                {
                    long t = v;
                    v = u;
                    u = t;
                }
                v -= u;
            } while (v != 0);
            return false;
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
        public void init(out int p, out int q, out int e, out int d, out int n, out string plaintext)
        {
            Random r = new Random();
            p = r.Next(255);
            q = r.Next(255);
            while (!isPrime(p))
                p = r.Next(255);
            while (!isPrime(q))
                q = r.Next(255);
            n = p * q;
            int totient = n - (p + q - 1);
            e = r.Next(1, totient);
            while (!coprime(e, totient))
                e = r.Next(1, totient);
            d = invMod(e, totient);
            int[] publicKey = new int[] { n, e };
            int privateKey = d;
            do
            {
                Console.Clear();
                Console.WriteLine("RSA Cryptosystem program");
                Console.WriteLine("Value of p: " + p);
                Console.WriteLine("Value of q: " + q);
                Console.WriteLine("Value of e: " + e);
                Console.WriteLine("Value of d: " + d);
                Console.WriteLine("Value of n: " + n);
                Console.Write("Enter plaintext: "); plaintext = Console.ReadLine();
            } while (plaintext == "");
            plaintext = plaintext.Replace(" ", "");
        }
        public int[] encrypt(string plaintext,int e,int n)
        {
            int[] arr = new int[plaintext.Length];
            char[] buf = plaintext.ToCharArray();
            BigInteger BI, temp;
            int y = 0;
            for (int i = 0; i < plaintext.Length; i++)
            {
                y = (int)buf[i];
                BI = BigInteger.Pow(y, e);
                temp = BI % n;
                arr[i] = (int)temp;
                Console.WriteLine(buf[i] + " -> " + y);
                Console.WriteLine(y + " ^ " + e + " % " + n + " = " + temp + "\n");
            }
            return arr;
        }
        public string decrypt(int[] ciphertext, int d, int n)
        {
            string s = "";
            BigInteger BI, temp;
            double temp2;
            foreach(int i in ciphertext)
            {
                BI = BigInteger.Pow(i, d);
                temp = BI % n;
                temp2 = (int)temp;
                char t = (char)temp;
                Console.WriteLine(i + " ^ " + d + " % " + n + " = " + temp2 );
                Console.WriteLine(temp2 + " -> " + t + "\n");
                s += t;
            }
            return s;
        }
    }
}
