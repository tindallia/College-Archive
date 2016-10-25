using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            int angka1, angka2;
            int[,] mtx = new int[2, 2];
            int[,] tmp = new int[2, 2];
            string pilihan, kalimat, ciper;
            Boolean ulang = true;
            Program cobain = new Program();
            do
            {
                Console.Clear();
                Console.WriteLine("\n Program Hill Cipher");
                Console.WriteLine(" ==========================================");
                Console.WriteLine(" 1. Cari Kunci");
                Console.WriteLine(" 2. Sudah Tahu Kunci (enkrip/dekrip)");
                Console.Write(" Masukkan input: "); angka1 = Convert.ToInt16(Console.ReadLine());
                if (angka1 == 1)
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("\n Program Hill Cipher");
                        Console.WriteLine(" ==========================================");
                        Console.Write(" Masukkan Plain teks: "); kalimat = Console.ReadLine();
                        Console.Write(" Masukkan Cipher teks: "); ciper = Console.ReadLine();
                        if (kalimat.Length < 4 || ciper.Length < 4)
                            Console.WriteLine(" Jumlah huruf harus 4 atau lebih");
                    } while (kalimat.Length < 4 || ciper.Length < 4);
                    mtx = cobain.nyariKunci(kalimat, ciper);
                    Console.WriteLine(" Kuncinya adalah: ");
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            Console.Write(" " + mtx[i, j]);
                        }
                        Console.WriteLine();
                    }
                }
                else
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("\n Program Hill Cipher");
                        Console.WriteLine(" ==========================================");
                        Console.Write(" Masukkan Kata\t: ");
                        kalimat = Console.ReadLine();
                        for (int i = 0; i < 2; i++)
                        {
                            for (int j = 0; j < 2; j++)
                            {
                                Console.Write(" Input Kunci " + (i + 1) + "," + (j + 1) + ": ");
                                mtx[i, j] = Convert.ToInt32(Console.ReadLine());
                            }
                        }
                        if (kalimat.Length % 2 != 0 || cobain.determinan(mtx) == 0 || cobain.GCD(cobain.determinan(mtx), 26) != 1)
                            Console.WriteLine(" Jumlah karakter harus genap\n determinan kunci tidak boleh 0\n GCD determinan dengan 26 harus 1");
                        Console.WriteLine("\n <<<< Tekan Enter >>>");
                        Console.ReadKey();
                    } while (kalimat.Length % 2 != 0 || cobain.determinan(mtx) == 0 || cobain.GCD(cobain.determinan(mtx), 26) != 1);
                    Console.Clear();
                    Console.WriteLine("\n Program Hill Cipher");
                    Console.WriteLine(" ==========================================");
                    Console.WriteLine(" 1. Enkripsi");
                    Console.WriteLine(" 2. Dekripsi");
                    Console.WriteLine(" 3. Exit");
                    Console.Write(" Masukkan Pilihan\t: ");
                    angka2 = Convert.ToInt32(Console.ReadLine());
                    switch (angka2)
                    {
                        case 1:
                            {
                                Console.Clear();
                                Console.Write("\n Hasil\t:");
                                cobain.hillEnkripsi(mtx, kalimat);
                                break;
                            }
                        case 2:
                            {
                                Console.Clear();
                                Console.Write("\n Hasil\t:");
                                cobain.hillDekripsi(mtx, kalimat);
                                break;
                            }
                        default:
                            {
                                Console.Clear();
                                Console.Write("\t <<< TEKAN ENTER >>> ");
                                break;
                            }
                    }
                }
                Console.Write("\n Apa anda ingin mengulangi Program (ketik 'ya' untuk mengulangi program)? ");
                pilihan = Console.ReadLine();
                if (pilihan != "ya")
                    ulang = false;
            } while (ulang);
            Console.WriteLine("\n <<<< Tekan Enter >>>");
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
        public int modPecahan(int a, int b)
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
        public void hillEnkripsi(int[,] kunci, string kata)
        {
            int x1, x2, y1, y2;
            for (int i = 0; i < kata.Length; i++)
            {
                x1 = (int)kata[i];
                x2 = (int)kata[i + 1];
                if (x1 >= 65 && x1 < 91 && x2 >= 65 && x2 < 91)
                {
                    x1 = x1 - 65; x2 = x2 + 65;
                    y1 = x1 * kunci[0, 0] + x2 * kunci[1, 0];
                    y2 = x1 * kunci[0, 1] + x2 * kunci[1, 1];
                    y1 = Mod(y1, 26) + 65; y2 = Mod(y2, 26) + 65;
                    Console.Write((char)y1);
                    Console.Write((char)y2);
                }
                else if (x1 >= 97 && x1 < 123 && x2 >= 65 && x2 < 91)
                {
                    x1 = x1 - 97; x2 = x2 - 65;
                    y1 = x1 * kunci[0, 0] + x2 * kunci[1, 0];
                    y2 = x1 * kunci[0, 1] + x2 * kunci[1, 1];
                    y1 = Mod(y1, 26) + 97; y2 = Mod(y2, 26) + 65;
                    Console.Write((char)y1);
                    Console.Write((char)y2);
                }
                else if (x1 >= 65 && x1 < 91 && x2 >= 97 && x2 < 123)
                {
                    x1 = x1 - 65; x2 = x2 - 97;
                    y1 = x1 * kunci[0, 0] + x2 * kunci[1, 0];
                    y2 = x1 * kunci[0, 1] + x2 * kunci[1, 1];
                    y1 = Mod(y1, 26) + 65; y2 = Mod(y2, 26) + 97;
                    Console.Write((char)y1);
                    Console.Write((char)y2);
                }
                else if (x1 >= 97 && x1 < 123 && x2 >= 97 && x2 < 123)
                {
                    x1 = x1 - 97; x2 = x2 - 97;
                    y1 = x1 * kunci[0, 0] + x2 * kunci[1, 0];
                    y2 = x1 * kunci[0, 1] + x2 * kunci[1, 1];
                    y1 = Mod(y1, 26) + 97; y2 = Mod(y2, 26) + 97;
                    Console.Write((char)y1);
                    Console.Write((char)y2);
                }
                i++;
            }
        }
        public int[,] adjoint(int[,] matriks)
        {
            int[,] hsl = new int[2, 2];
            hsl[0, 0] = matriks[1, 1];
            hsl[1, 1] = matriks[0, 0];
            hsl[1, 0] = -matriks[1, 0];
            hsl[0, 1] = -matriks[0, 1];
            return hsl;
        }
        public int determinan(int[,] matriks)
        {
            return (matriks[0, 0] * matriks[1, 1]) - (matriks[0, 1] * matriks[1, 0]);
        }
        public int[,] perkalianMatriks(int[,] A, int[,] B)
        {
            int[,] mtrks4 = new int[2, 2];
            mtrks4[0, 0] = (A[0, 0] * B[0, 0]) + (A[0, 1] * B[1, 0]);
            mtrks4[0, 1] = (A[0, 0] * B[0, 1]) + (A[0, 1] * B[1, 1]);
            mtrks4[1, 0] = (A[1, 0] * B[0, 0]) + (A[1, 1] * B[1, 0]);
            mtrks4[1, 1] = (A[1, 0] * B[0, 1]) + (A[1, 1] * B[1, 1]);
            return mtrks4;
        }
        public int[,] nyariKunci(string plain, string ciper)
        {
            int[,] kunci = new int[2, 2];
            int[,] invers = new int[2, 2];
            int[,] teks = new int[2, 2];
            int k = 0, l = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    teks[i, j] = (int)plain[l];
                    teks[i, j] = teks[i, j] - 97;
                    l++;
                }
            }
            int[,] cip = new int[2, 2];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    cip[i, j] = (int)ciper[k];
                    cip[i, j] = cip[i, j] - 97;
                    k++;
                }
            }
            int eks = 0;
            int[,] eks2 = new int[2, 2];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    eks = Mod(determinan(teks), 26);
                    eks2[i, j] = Mod(adjoint(teks)[i, j], 26);
                    if (Mod(adjoint(teks)[i, j], eks) != 0)
                    {
                        invers[i, j] = Mod((modPecahan(eks, 26) * eks2[i, j]), 26);
                    }
                    else
                    {
                        invers[i, j] = Mod(eks2[i, j] / eks, 26);
                    }
                }
            }
            kunci = perkalianMatriks(invers, cip);
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    kunci[i, j] = Mod(kunci[i, j], 26);
                }
            }
            return kunci;
        }
        public void hillDekripsi(int[,] kunci, string kata)
        {
            int[,] invers = new int[2, 2];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    invers[i, j] = Mod((modPecahan(determinan(kunci), 26) * adjoint(kunci)[i, j]), 26);
                }
            }
            int x1, x2, y1, y2;
            for (int i = 0; i < kata.Length; i++)
            {
                x1 = (int)kata[i];
                x2 = (int)kata[i + 1];
                if (x1 >= 65 && x1 < 91 && x2 >= 65 && x2 < 91)
                {
                    x1 = x1 - 65; x2 = x2 + 65;
                    y1 = x1 * invers[0, 0] + x2 * invers[1, 0];
                    y2 = x1 * invers[0, 1] + x2 * invers[1, 1];
                    y1 = Mod(y1, 26) + 65; y2 = Mod(y2, 26) + 65;
                    Console.Write((char)y1);
                    Console.Write((char)y2);
                }
                else if (x1 >= 97 && x1 < 123 && x2 >= 65 && x2 < 91)
                {
                    x1 = x1 - 97; x2 = x2 - 65;
                    y1 = x1 * invers[0, 0] + x2 * invers[1, 0];
                    y2 = x1 * invers[0, 1] + x2 * invers[1, 1];
                    y1 = Mod(y1, 26) + 97; y2 = Mod(y2, 26) + 65;
                    Console.Write((char)y1);
                    Console.Write((char)y2);
                }
                else if (x1 >= 65 && x1 < 91 && x2 >= 97 && x2 < 123)
                {
                    x1 = x1 - 65; x2 = x2 - 97;
                    y1 = x1 * invers[0, 0] + x2 * invers[1, 0];
                    y2 = x1 * invers[0, 1] + x2 * invers[1, 1];
                    y1 = Mod(y1, 26) + 65; y2 = Mod(y2, 26) + 97;
                    Console.Write((char)y1);
                    Console.Write((char)y2);
                }
                else if (x1 >= 97 && x1 < 123 && x2 >= 97 && x2 < 123)
                {
                    x1 = x1 - 97; x2 = x2 - 97;
                    y1 = x1 * invers[0, 0] + x2 * invers[1, 0];
                    y2 = x1 * invers[0, 1] + x2 * invers[1, 1];
                    y1 = Mod(y1, 26) + 97; y2 = Mod(y2, 26) + 97;
                    Console.Write((char)y1);
                    Console.Write((char)y2);
                }
                i++;
            }
        }
    }
}
