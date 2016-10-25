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
            int w, h;
            string plainT, rep, cipher = "";
            char[,] matrix = { };
            Boolean loop = true;
            Program p = new Program();
            do
            {
                p.menu(out w, out h, out plainT);
                p.createMatrix(ref plainT, ref w, ref h, ref matrix);
                p.arrange(plainT, w, h, matrix, ref cipher);
                Console.WriteLine("\nPress any key to continue...\n"); Console.ReadKey(true);
                Console.WriteLine("Decryption in progress...");
                p.createMatrix(ref cipher, ref w, ref h, ref matrix);
                p.arrange(plainT, w, h, matrix, ref cipher);
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

        private void menu(out int w, out int h, out string plainT)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Route Cipher Program");
                Console.WriteLine("==========================================");
                Console.Write("Enter plain text: ");
                plainT = Console.ReadLine();
                Console.Write("Enter grid width\t: ");
                w = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter grid height\t: ");
                h = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                if (w * h < plainT.Length)
                    Console.WriteLine("Grid must be the same size or larger than plain text length.");
                if (w == 1 || h == 1)
                    Console.WriteLine("Grid's width and/or height must be larger than 1");
                if (w * h < plainT.Length || (w == 1 || h == 1))
                {
                    Console.WriteLine("\nPress any key to continue..."); Console.ReadKey(true);
                }
            } while (w * h < plainT.Length || (w == 1 || h == 1));
            plainT.ToUpper();
        }
        private void createMatrix(ref string plainT, ref int w, ref int h, ref char[,] dec)
        {
            int method = 1;
            Console.WriteLine("\nSelect grid route:");
            Console.WriteLine("1. Left to Right");
            Console.WriteLine("2. Right to Left");
            Console.WriteLine("3. Top to Bottom");
            Console.WriteLine("4. Bottom to Top");
            Console.Write("Select your choice (default=1) ");
            method = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Creating grid...");
            switch (method)
            {
                case 1:
                    createMatrixLtoR(plainT, w, h, out dec);
                    break;
                case 2:
                    createMatrixRtoL(plainT, w, h, out dec);
                    break;
                case 3:
                    createMatrixTtoB(plainT, w, h, out dec);
                    break;
                case 4:
                    createMatrixBtoT(plainT, w, h, out dec);
                    break;
                default:
                    break;
            }
        }
        private void createMatrixLtoR(string myString, int width, int height, out char[,] dec)
        {
            myString.ToUpper();
            char[] buffer = myString.ToCharArray();
            int count = 0, x = width, y = height;
            char[,] matrix = new char[x, y];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (count >= buffer.Length)
                    {
                        matrix[j,i] = 'X';
                    }
                    else
                    {
                        matrix[j,i] = buffer[count];
                        count++;
                    }
                }
            }
            Console.WriteLine("Created grid:");
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write(matrix[j, i]);
                }
                Console.WriteLine();
            }
            dec = matrix;
        }
        private void createMatrixRtoL(string myString, int width, int height, out char[,] dec)
        {
            myString.ToUpper();
            char[] buffer = myString.ToCharArray();
            int count = 0, x = width, y = height;
            char[,] matrix = new char[x, y];
            for (int i = 0; i < height; i++)
            {
                count = width * (i+1) - 1;
                for (int j = 0; j < width; j++)
                {
                    matrix[j, i] = buffer[count];
                    count--;
                }
            }
            Console.WriteLine("Created grid:");
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write(matrix[j, i]);
                }
                Console.WriteLine();
            }
            dec = matrix;
        }
        private void createMatrixTtoB(string myString, int width, int height, out char[,] dec)
        {
            char[] buffer = myString.ToCharArray();
            int count, x = width, y = height;
            char[,] matrix = new char[x, y];
            for (int i = 0; i < height; i++)
            {
                count = i;
                for (int j = 0; j < width; j++)
                {
                    if (count >= buffer.Length)
                    {
                        matrix[j, i] = 'X';
                    }
                    else
                    {
                        matrix[j, i] = buffer[count];
                        count = count + height;
                    }
                }
            }
            Console.WriteLine("Created grid:");
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write(matrix[j, i]);
                }
                Console.WriteLine();
            }
            dec = matrix;
        }
        private void createMatrixBtoT(string myString, int width, int height, out char[,] dec)
        {
            char[] buffer = myString.ToCharArray();
            int count, x = width, y = height;
            char[,] matrix = new char[x, y];
            for (int i = 0; i < height; i++)
            {
                count = height - 1 - i;
                for (int j = 0; j < width; j++)
                {
                    if (count >= buffer.Length)
                    {
                        matrix[j, i] = 'X';
                    }
                    else
                    {
                        matrix[j, i] = buffer[count];
                        count = count + height;
                    }
                }
            }
            Console.WriteLine("Created grid:");
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write(matrix[j, i]);
                }
                Console.WriteLine();
            }
            dec = matrix;
        }
        private void arrange(string plainT, int w, int h, char[,] matrix, ref string cipher)
        {
            int method = 1;
            Console.WriteLine("\nSelect encryption route:");
            Console.WriteLine("1. Left to Right");
            Console.WriteLine("2. Right to Left");
            Console.WriteLine("3. Top to Bottom");
            Console.WriteLine("4. Bottom to Top");
            Console.Write("Select your choice (default=1) ");
            method = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nArranging letters...");
            switch (method)
            {
                case 1:
                    encryptLtoR(plainT, matrix, w, h, out cipher);
                    break;
                case 2:
                    encryptRtoL(plainT, matrix, w, h, out cipher);
                    break;
                case 3:
                    encryptTtoB(plainT, matrix, w, h, out cipher);
                    break;
                case 4:
                    encryptBtoT(plainT, matrix, w, h, out cipher);
                    break;
            }
        }
        private void encryptLtoR(string myString, char[,] matrix,int w, int h, out string cipher)
        {
            char[] buffer = { };
            Array.Resize(ref buffer, w * h);
            int count=0;
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    buffer[count] = matrix[j, i];
                    count++;
                }
            }
            string s = new string(buffer);
            cipher = s;
            Console.WriteLine("Encrypted text: " + cipher);
        }
        private void encryptRtoL(string myString, char[,] matrix, int w, int h, out string cipher)
        {
            char[] buffer = { };
            Array.Resize(ref buffer, w * h);
            int count = 0;
            for (int i = 0; i < h; i++)
            {
                count = (w * (i + 1)) - 1;
                for (int j = 0; j < w; j++)
                {
                    buffer[count] = matrix[j, i];
                    count--;
                }
            }
            string s = new string(buffer);
            cipher = s;
            Console.WriteLine("Encrypted text: " + cipher);
        }
        private void encryptTtoB(string myString, char[,] matrix, int w, int h, out string cipher)
        {
            char[] buffer = { };
            Array.Resize(ref buffer, w * h);
            int count = 0;
            for (int i = 0; i < h; i++)
            {
                count = i;
                for (int j = 0; j < w; j++)
                {
                        buffer[count] = matrix[j, i];
                        count = count + h;
                }
            }
            string s = new string(buffer);
            cipher = s;
            Console.WriteLine("Encrypted text: " + cipher);
        }
        private void encryptBtoT(string myString, char[,] matrix, int w, int h, out string cipher)
        {
            char[] buffer = { };
            Array.Resize(ref buffer, w * h);
            int count = 0;
            for (int i = 0; i < h; i++)
            {
                count = h - 1 - i;
                for (int j = 0; j < w; j++)
                {
                    buffer[count] = matrix[j, i];
                    count = count + h;
                }
            }
            string s = new string(buffer);
            cipher = s;
            Console.WriteLine("Encrypted text: " + cipher);
        }
    }
}