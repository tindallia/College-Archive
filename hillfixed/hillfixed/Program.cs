using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace route
{
    class Program
    {
        static void Main(string[] args)
        {
            int m = 2;
            string plainT = "", rep, ciph = "";
            int[,] matrix = new int[m, m];
            Boolean loop = true;
            Program p = new Program();
            do
            {
                p.menu(ref m, ref plainT, ref ciph, ref matrix);
                Console.WriteLine("\nPress any key to continue...\n"); Console.ReadKey(true);
                Console.Write("Would you like to restart the program?(y/N) ");
                rep = Console.ReadLine();
                rep.ToLower();
                if (rep != "y")
                    loop = false;
            } while (loop);
            Console.WriteLine("Fajar Satria (140810140015)");
            Console.WriteLine("M. Dean Aji Wibowo (140810140027)");
            Console.WriteLine("Ridwan Ibrahim (140810140033)");
            Console.WriteLine("Aditya Pratama (140810140039)");
            Console.WriteLine("William Rahman (140810140069)");
            Console.WriteLine("2016");
            Thread.Sleep(3000);
        }

        private void menu(ref int m, ref string plainT,ref string ciph, ref int[,] matrix)
        {
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("Hill Cipher Program (2x2)");
                Console.WriteLine("==========================================");
                Console.WriteLine("1. Encrypt & Decrypt");
                Console.WriteLine("2. Cryptanalysis");
                Console.WriteLine("0. Exit");
                Console.Write("Enter selection: ");
                choice = Convert.ToInt32(Console.ReadLine());
            } while (choice != 1 && choice != 2 && choice != 0);
            switch (choice)
            {
                case 1:
                    encDecInp(ref plainT, ref matrix, m);
                    Console.WriteLine("\nEncryption in progress...");
                    Console.WriteLine("Key:");
                    for (int i = 0; i < m; i++)
                    {
                        for (int j = 0; j < m; j++)
                        {
                            Console.Write(matrix[j, i] + "\t");
                        }
                        Console.WriteLine();
                    }
                    encrypt(matrix, plainT, out plainT);
                    Console.WriteLine("\nPress any key to continue...\n"); Console.ReadKey(true);
                    Console.WriteLine("Decryption in progress...");
                    decrypt(matrix, plainT, m);
                    break;
                case 2:
                    keyInp(ref plainT, ref ciph, matrix, m);
                    break;
            }
        }
        private void encDecInp(ref string plainT,ref int[,] matrix,int m)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Hill Cipher Program (2x2)");
                Console.WriteLine("==========================================");
                Console.Write("Enter plain text: ");
                plainT = Console.ReadLine();
                Console.WriteLine("Enter key: ");
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        Console.Write("Key [" + (i + 1) + "][" + (j + 1) + "] : ");
                        matrix[i, j] = Convert.ToInt32(Console.ReadLine());
                    }
                }
                if (plainT.Length % 2 != 0)
                    Console.WriteLine("Number of characters must be an even number");
                if (determinant(matrix) == 0)
                    Console.WriteLine("Determinant of the key must not be equal to 0\n");
                if (GCD(determinant(matrix), 26) != 1)
                    Console.WriteLine("GCD between the key and 26 must be equal to 1");
                if (plainT.Length % 2 != 0 || determinant(matrix) == 0 || GCD(determinant(matrix), 26) != 1)
                {
                    Console.Write("Press any key to continue..."); Console.ReadKey(true);
                }
            } while (plainT.Length % 2 != 0 || determinant(matrix) == 0 || GCD(determinant(matrix), 26) != 1);
        }
        private void keyInp(ref string plainT, ref string cipherT,int[,] matrix,int m)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Hill Cipher Program (2x2)");
                Console.WriteLine("==========================================");
                Console.Write("Enter plain text: ");
                plainT = Console.ReadLine();
                Console.Write("Enter cipher text: ");
                cipherT = Console.ReadLine();
                if (plainT.Length % 2 != 0 || cipherT.Length % 2 != 0)
                {
                    Console.WriteLine("Number of characters must be an even number");
                    Console.Write("Press any key to continue..."); Console.ReadKey(true);
                }
            } while (plainT.Length % 2 != 0 || cipherT.Length % 2 != 0);
            matrix = key(plainT, cipherT);
            Console.WriteLine("Finding key...");
            Console.WriteLine("Key: ");
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
        private int mod(int a, int n)
        {
            if (a < 0)
                return (mod((n + a), n));
            else
                return (a % n);
        }
        private int frMod(int a, int b)
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
            return (mod(t[j - 1], b));
        }
        private int GCD(int p, int q)
        {
            if (q == 0)
                return p;
            int r = p % q;
            return GCD(q, r);
        }
        private int determinant(int[,] matrix)
        {
            return ((matrix[0, 0] * matrix[1, 1]) - (matrix[0, 1] * matrix[1, 0]));
        }
        private int[,] adjoint(int[,] matrix)
        {
            int temp;
            temp = matrix[0, 0];
            matrix[0, 0] = matrix[1, 1];
            matrix[1, 1] = temp;
            matrix[0, 1] = -matrix[0, 1];
            matrix[1, 0] = -matrix[1, 0];
            return matrix;
        }
        private void encrypt(int[,] matrix,string plainT,out string dec)
        {
            char[] buf = new char[plainT.Length];
            int x1, x2, y1, y2;
            for (int i = 0; i < plainT.Length; i++)
            {
                x1 = (int)plainT[i];
                x2 = (int)plainT[i + 1];
                if (x1 >= 'A' && x1 <= 'Z' && x2 >= 'A' && x2 <= 'Z')
                {
                    x1 = x1 - 'A'; x2 = x2 + 'A';
                    y1 = x1 * matrix[0, 0] + x2 * matrix[0, 1];
                    y2 = x1 * matrix[1, 0] + x2 * matrix[1, 1];
                    y1 = mod(y1, 26) + 'A'; y2 = mod(y2, 26) + 'A';
                    buf[i] = (char)y1;
                    buf[i + 1] = (char)y2;
                }
                else if (x1 >= 'a' && x1 <= 'z' && x2 >= 'A' && x2 <= 'Z')
                {
                    x1 = x1 - 'a'; x2 = x2 - 'A';
                    y1 = x1 * matrix[0, 0] + x2 * matrix[0, 1];
                    y2 = x1 * matrix[1, 0] + x2 * matrix[1, 1];
                    y1 = mod(y1, 26) + 'a'; y2 = mod(y2, 26) + 'A';
                    buf[i] = (char)y1;
                    buf[i + 1] = (char)y2;
                }
                else if (x1 >= 'A' && x1 < 'Z' && x2 >= 'a' && x2 <= 'z')
                {
                    x1 = x1 - 'A'; x2 = x2 - 'a';
                    y1 = x1 * matrix[0, 0] + x2 * matrix[0, 1];
                    y2 = x1 * matrix[1, 0] + x2 * matrix[1, 1];
                    y1 = mod(y1, 26) + 'A'; y2 = mod(y2, 26) + 'a';
                    buf[i] = (char)y1;
                    buf[i + 1] = (char)y2;
                }
                else if (x1 >= 'a' && x1 < 'z' && x2 >= 'a' && x2 <= 'z')
                {
                    x1 = x1 - 'a'; x2 = x2 - 'a';
                    y1 = x1 * matrix[0, 0] + x2 * matrix[0, 1];
                    y2 = x1 * matrix[1, 0] + x2 * matrix[1, 1];
                    y1 = mod(y1, 26) + 'a'; y2 = mod(y2, 26) + 'a';
                    buf[i] = (char)y1;
                    buf[i + 1] = (char)y2;
                }
                i += 1;
            }
            string s = new string(buf);
            dec = s;
            Console.WriteLine("Encrypted text: " + dec);
        }
        private void decrypt(int[,] matrix, string plainT, int m)
        {
            int[,] inverse = new int[m, m];
            int[,] adj = adjoint(matrix);
            int detMod = frMod(determinant(matrix), 26);
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    inverse[i, j] = mod((detMod * adj[i,j]), 26);
                    Console.Write(inverse[i, j]+"\t");
                }
                Console.WriteLine();
            }
            char[] buf = new char[plainT.Length];
            int x1, x2, y1, y2;
            for (int i = 0; i < plainT.Length; i++)
            {
                x1 = (int)plainT[i];
                x2 = (int)plainT[i + 1];
                if (x1 >= 'A' && x1 <= 'Z' && x2 >= 'A' && x2 <= 'Z')
                {
                    x1 = x1 - 'A'; x2 = x2 + 'A';
                    y1 = x1 * inverse[0, 0] + x2 * inverse[0, 1];
                    y2 = x1 * inverse[1, 0] + x2 * inverse[1, 1];
                    y1 = mod(y1, 26) + 'A'; y2 = mod(y2, 26) + 'A';
                    buf[i] = (char)y1;
                    buf[i + 1] = (char)y2;
                }
                else if (x1 >= 'a' && x1 <= 'z' && x2 >= 'A' && x2 <= 'Z')
                {
                    x1 = x1 - 'a'; x2 = x2 - 'A';
                    y1 = x1 * inverse[0, 0] + x2 * inverse[0, 1];
                    y2 = x1 * inverse[1, 0] + x2 * inverse[1, 1];
                    y1 = mod(y1, 26) + 'a'; y2 = mod(y2, 26) + 'A';
                    buf[i] = (char)y1;
                    buf[i + 1] = (char)y2;
                }
                else if (x1 >= 'A' && x1 < 'Z' && x2 >= 'a' && x2 <= 'z')
                {
                    x1 = x1 - 'A'; x2 = x2 - 'a';
                    y1 = x1 * inverse[0, 0] + x2 * inverse[0, 1];
                    y2 = x1 * inverse[1, 0] + x2 * inverse[1, 1];
                    y1 = mod(y1, 26) + 'A'; y2 = mod(y2, 26) + 'a';
                    buf[i] = (char)y1;
                    buf[i + 1] = (char)y2;
                }
                else if (x1 >= 'a' && x1 < 'z' && x2 >= 'a' && x2 <= 'z')
                {
                    x1 = x1 - 'a'; x2 = x2 - 'a';
                    y1 = x1 * inverse[0, 0] + x2 * inverse[0, 1];
                    y2 = x1 * inverse[1, 0] + x2 * inverse[1, 1];
                    y1 = mod(y1, 26) + 'a'; y2 = mod(y2, 26) + 'a';
                    buf[i] = (char)y1;
                    buf[i + 1] = (char)y2;
                }
                i += 1;
            }
            string s = new string(buf);
            Console.WriteLine("Decrypted text: " + s);
        }
        private int[,] matrixMul(int[,] pt, int[,] ct)
        {
            int[,] k = new int[2, 2];
            k[0, 0] = (pt[0, 0] * ct[0, 0]) + (pt[0, 1] * ct[1, 0]);
            k[0, 1] = (pt[0, 0] * ct[0, 1]) + (pt[0, 1] * ct[1, 1]);
            k[1, 0] = (pt[1, 0] * ct[0, 0]) + (pt[1, 1] * ct[1, 0]);
            k[1, 1] = (pt[1, 0] * ct[0, 1]) + (pt[1, 1] * ct[1, 1]);
            return k;
        }
        private int[,] key(string pt, string ct)
        {
            int[,] k = new int[2, 2];
            int[,] inv = new int[2, 2];

            int[,] plain = new int[2, 2];
            int n = 0, l = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    plain[i, j] = (int)pt[l];
                    plain[i, j] = plain[i, j] - 'a';
                    l++;
                }
            }

            int[,] cipr = new int[2, 2];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    cipr[i, j] = (int)ct[n];
                    cipr[i, j] = cipr[i, j] - 'a';
                    n++;
                }
            }
            int x = 0;
            int[,] y = new int[2, 2];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    x = mod(determinant(plain), 26);
                    y[i, j] = mod(adjoint(plain)[i, j], 26);
                    if (mod(adjoint(plain)[i, j], x) != 0)
                        inv[i, j] = mod((frMod(x, 26) * y[i, j]), 26);
                    else
                        inv[i, j] = mod(y[i, j] / x, 26);
                }
            }
            k = matrixMul(inv, cipr);
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                    k[i, j] = mod(k[i, j], 26);
            }
            return k;
        }
    }
}