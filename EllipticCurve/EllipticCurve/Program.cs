using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EllipticCurve
{
    class Program
    {
        static void Main(string[] args)
        {
            Program prog = new Program();
            int a = 1, b = 6, p = 11;
            //int a, b, p;
            char pT1 = ' ', pT2 = ' ';
            int[] alpha = { 9, 10 };//new int[2];
            //prog.input(out a, out b, out p);
            int p1 = (int)Char.GetNumericValue(pT1), p2 = (int)Char.GetNumericValue(pT2);
            int[] plaintext = new int[] { p1, p2 };
        }
        void input(out int a, out int b, out int p)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Elliptic Curve Cryptosystem Program\n");
                Console.WriteLine("a: "); a = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("b: "); b = Convert.ToInt32(Console.ReadLine());
                do
                {
                    Console.WriteLine("p: "); p = Convert.ToInt32(Console.ReadLine());
                    if (p < 3)
                    {
                        Console.WriteLine("Nilai p harus lebih dari 3");
                        Console.WriteLine("Press any key to continue...."); Console.ReadKey(true);
                    }
                } while (p < 3);
                
                if ((4 * (a ^ 3) + 27 * (b ^ 2)) % p == 0)
                {
                    Console.WriteLine("Nilai 4a^3 + 27b^2 mod p tidak boleh bernilai 0");
                    Console.WriteLine("Press any key to continue..."); Console.ReadKey(true);
                    Console.WriteLine();
                }
            } while ((4 * (a ^ 3) + 27 * (b ^ 2)) % p == 0);
        }
        bool isQuadraticResidue(int p, int r)
        {
            if ((r ^ ((p - 1) / 2) % p) == 1)
                return true;
            else
                return false;
        }
        int[] quadraticResidue(int p)
        {
            List<int> temp = new List<int>();
            for (int i = 1; i < p; i++)
            {
                if (isQuadraticResidue(i, p))
                    temp.Add(i);
            }
            return temp.ToArray();
        }
        int lambda(int[] x, int[] y, int p)
        {
            int x1, y1, x2, y2, a = 1;
            x1 = x[0];
            y1 = x[1];
            x2 = y[0];
            y2 = y[1];
            if (x != y)
                return invMod(((y2 - y1) / (x2 - x1)), p);
            else
                return invMod(((3 * (x1 ^ 2) + a) / 2 * y1), p);
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
        int[] PplusQ(int[] x, int[] y, int p)
        {
            int x1, y1, x2, y2, x3, y3;
            x1 = x[0];
            y1 = x[1];
            x2 = y[0];
            y2 = y[1];
            x3 = (lambda(x, y, p) ^ 2) - x1 - x2;
            y3 = (lambda(x, y, p) * (x1 - x3)) - y1;
            int[] temp = new int[] { x3, y3 };
            return temp;
        }
    }
}
