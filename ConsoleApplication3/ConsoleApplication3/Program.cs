using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Checkerboard
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Program p = new Program();
            int i1, i2;
            char[] tableContents = p.init();
            p.inputI(out i1, out i2);
            char[,] table = p.initCheckerboard(tableContents, i1, i2);
            Console.WriteLine("\nPress any key to continue..."); Console.ReadKey(true);
            string pT;
            Console.Write("\nPlain text to be encrypted: ");
            pT = Console.ReadLine();
            string cipher = p.encrypt(pT, table, i1, i2);
            p.decrypt(cipher, i1, i2, table);
            Console.WriteLine("\nUTS Praktikum Kriptografi 2016");
            Console.WriteLine("Mohamad Dean Aji Wibowo (140810140027)");
            Thread.Sleep(3000);
        }
        
        char[] init()
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyz #";
            string keystream = /*"for the king"*/" decrypt#", estoniar = "etaoinsr";
            Console.Clear();
            bool found = false;
            foreach (char c in keystream)
            {
                foreach (char d in estoniar)
                {
                    if (c == d)
                    {
                        found = false;
                        break;
                    }
                    if (c != d)
                        found = true;
                }
                if (found)
                    estoniar = estoniar + c;
            }
            foreach (char c in alphabet)
            {
                foreach (char d in estoniar)
                {
                    if (c == d)
                    {
                        found = false;
                        break;
                    }
                    if (c != d)
                        found = true;
                }
                if (found)
                    estoniar = estoniar + c;
            }
            char[] letters = estoniar.ToCharArray();
            return letters;
        }
        void inputI(out int i1, out int i2)
        {   
            do
            {
                Console.Clear();
                Console.WriteLine("The Straddling Checkerboard Cipher Program\n");
                Console.WriteLine(/*"Keystream: for the king"*/ "Keystream:  decrypt#");
                Console.Write("Key 1: ");
                i1 = Convert.ToInt32(Console.ReadLine());
                Console.Write("Key 2: ");
                i2 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                if (i1 < 0 || i1 > 9)
                    Console.WriteLine("i1 must be greater than 0 and less than 9");
                if (i2 < 0 || i2 > 9)
                    Console.WriteLine("i2 must be greater than 0 and less than 9");
                if ((i1 < 0 || i1 > 9) || (i2 < 0 || i2 > 9))
                { Console.Write("Press any key to continue..."); Console.ReadKey(true); }
            } while ((i1 < 0 || i1 > 9) || (i2 < 0 || i2 > 9));
        }
        char[,] initCheckerboard(char[] letters, int i1, int i2)
        {
            int count = 0;
            char[,] checkerboard = new char[3, 10];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if ((j == i1 || j == i2) && i == 0)
                        ; //empty statement
                    else if (!((j == i1 || j == i2) && i == 0))
                    {
                        checkerboard[i, j] = letters[count];
                        count++;
                    }
                }
            }
            Console.WriteLine("Created table:");
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if ((i == 0 && j == 0) || (i == 1 && j == 0))
                        Console.Write("  ");
                    else if (i == 0 && j > 0)
                        Console.Write(j - 1 + " ");
                    else if (i == 2 && j == 0)
                        Console.Write(i1 + " ");
                    else if (i == 3 && j == 0)
                        Console.Write(i2 + " ");
                    else
                        Console.Write(checkerboard[i - 1, j - 1] + " ");
                }
                Console.WriteLine();
            }
            return checkerboard;
        }
        string encrypt(string plaintext, char[,] tbl, int i1, int i2)
        {
            Console.WriteLine("Encryption in progress...");
            string cipher = "";
            string temp = "";
            int n;
            foreach (char c in plaintext)
            {
                if (int.TryParse(c.ToString(), out n) == false)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        for (int k = 0; k < 10; k++)
                        {
                            if (c == tbl[j, k])
                            {
                                if (j == 1)
                                {
                                    temp = temp + i1.ToString() + k.ToString() + " ";
                                    break;
                                }
                                else if (j == 2)
                                {
                                    temp = temp + i2.ToString() + k.ToString() + " ";
                                    break;
                                }
                                else
                                {
                                    temp = temp + k.ToString() + " ";
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                    temp = temp + c + " ";
            }
            cipher = temp;
            if(cipher.Contains(" "))
                cipher.Replace(" ","");
            Console.WriteLine("\nEncrypted text: " + temp);
            Console.WriteLine("\nPress any key to continue..."); Console.ReadKey(true);
            return cipher;
        }
        void decrypt(string cipher,int i1, int i2, char[,] tbl)
        {
            Console.WriteLine("\nDecryption in progress...");
            char[] dec = cipher.ToCharArray();
            string pTdec = "";
            int t, u;
            for (int i = 0; i < dec.Length; i++)
            {
                t = (int)Char.GetNumericValue(dec[i]);
                if (pTdec.Length > 0)
                {
                    if (string.Compare(pTdec.Substring(pTdec.Length - 1), "#") == 0)
                    {
                        i++;
                        pTdec = pTdec + dec[i].ToString();
                        i++;
                        t = (int)Char.GetNumericValue(dec[i]);
                    }
                }
                if (t == i1)
                {
                    i++;
                    u = (int)Char.GetNumericValue(dec[i]);
                    for (int j = 0; j < 10; j++)
                        if (u == j)
                            pTdec = pTdec + tbl[1, j];
                }
                else if (t == i2)
                {
                    i++;
                    u = (int)Char.GetNumericValue(dec[i]);
                    for (int j = 0; j < 10; j++)
                        if (u == j)
                            pTdec = pTdec + tbl[2, j];
                }
                else
                    for (int j = 0; j < 10; j++)
                        if (t == j)
                            pTdec = pTdec + tbl[0, j];
            }
            Console.WriteLine("Decrypted text: " + pTdec);
            Console.WriteLine("\nPress any key to continue..."); Console.ReadKey(true);
        }
    }
}
