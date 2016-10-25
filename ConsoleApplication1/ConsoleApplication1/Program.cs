using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            int a, b;
            string repeat;
            Boolean run = true;
            do
            {
                Console.Clear();
                Console.WriteLine("Inverse Modulo Program");
                Console.WriteLine("==========================================");
                Program p = new Program();
                Console.Write("Enter the dividend\t: 1/");
                a = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter the divider\t: ");
                b = Convert.ToInt32(Console.ReadLine());
                if (p.GCD(a, b) != 1)
                {
                    Console.WriteLine("GCD is not equal to 1");
                }
                else
                {
                    Console.WriteLine("Result\t\t: " + p.invMod(a, b));
                }
                Console.Write("Repeat the program?(Y/N) ");
                repeat = Console.ReadLine();
                repeat.ToLower();
                if (repeat != "y")
                    run = false;
            } while (run);
            Console.ReadKey();
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
            int q, c, d;
            int y = 1;
            int j = 0;
            c = a;
            d = b;
            int[] t = new int[20];
            while (y != 0)
            {
                if (j <= 1)
                {
                    t[j] = j;
                    j++;
                }
                else
                {
                    q = d / c;
                    y = d - q * c;
                    d = c;
                    t[j] = t[j - 2] + (q * t[j - 1]);
                    j++;
                }
            }
            return (Mod(t[j - 1], b));
        }
    }
}
