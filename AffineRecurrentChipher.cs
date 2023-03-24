using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Практика_1
{
    class AffineRecurrentChipher
    {
        int n = 33;
        string RALF = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        string Ralf = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        public AffineRecurrentChipher() { }
        public void Encrypt(int[,] k, string path)
        {
            for(int i = 0; i < 2; i++)
            {
                if (IsCoprime(k[0,i], n) == false)
                {
                    Console.WriteLine($"а={k[0, i]} и n={n} невзаимно простые числа");
                    return;
                }
            }
            string newpath = GetNewPath(path) + "cipher.txt";
            using (StreamWriter newfile = new StreamWriter(newpath, false, Encoding.UTF8))
            {
                newfile.Close();
            }

            using (StreamReader file = new StreamReader(path, Encoding.UTF8))
            {
                int i = 0;
                while (file.Peek() != -1)
                {
                    int index;
                    int newindex;
                    char Char = (char)file.Read();
                    if (Char == ' ')
                    {
                        using (StreamWriter newfile = new StreamWriter(newpath, true, Encoding.UTF8))
                        {
                            newfile.Write(' ');
                        }
                        continue;
                    }
                    if (i >= 2)
                    {
                        int temp1 = Modulo(k[0, 1] * k[0, 0]);
                        int temp2 = Modulo(k[1, 1] + k[1, 0]);
                        k[0, 0] = k[0, 1];
                        k[1, 0] = k[1, 1];
                        k[0, 1] = temp1;
                        k[1, 1] = temp2;
                    }
                    if (RALF.IndexOf(Char) >= 0)
                    {
                        index = RALF.IndexOf(Char);
                        if (i < 2) newindex = k[0, i] * index + k[1, i]; 
                        else newindex = k[0, 1] * index + k[1, 1];
                        newindex = Modulo(newindex);
                        using (StreamWriter newfile = new StreamWriter(newpath, true, Encoding.UTF8))
                        {
                            newfile.Write(RALF[newindex]);
                            Console.WriteLine(RALF[newindex]);
                        }
                    }
                    else if (Ralf.IndexOf(Char) >= 0)
                    {
                        index = Ralf.IndexOf(Char);
                        if (i < 2) newindex = k[0, i] * index + k[1, i];
                        else newindex = k[0, 1] * index + k[1, 1];
                        newindex = Modulo(newindex);
                        using (StreamWriter newfile = new StreamWriter(newpath, true, Encoding.UTF8))
                        {
                            newfile.Write(Ralf[newindex]);
                            Console.WriteLine(Ralf[newindex]);
                        }
                    }
                    i++;
                }
            }
        }
        public void Decrypt(int[,] k, string path)
        {
            for (int i = 0; i < 2; i++)
            {
                if (IsCoprime(k[0, i], n) == false)
                {
                    Console.WriteLine($"а={k[0, i]} и n={n} невзаимно простые числа");
                    return;
                }
            }
            string newpath = GetNewPath(path) + "opentext.txt";
            using (StreamWriter newfile = new StreamWriter(newpath, false, Encoding.UTF8))
            {
                newfile.Close();
            }

            using (StreamReader file = new StreamReader(path, Encoding.UTF8))
            {
                int i = 0;
                while (file.Peek() != -1)
                {
                    int index;
                    int newindex;
                    char Char = (char)file.Read();
                    int a_1 = 0;
                    if (Char == ' ')
                    {
                        using (StreamWriter newfile = new StreamWriter(newpath, true, Encoding.UTF8))
                        {
                            newfile.Write(' ');
                        }
                        continue;
                    }
                    if (i >= 2)
                    {
                        int temp1 = Modulo(k[0, 1] * k[0, 0]);
                        int temp2 = Modulo(k[1, 1] + k[1, 0]);
                        k[0, 0] = k[0, 1];
                        k[1, 0] = k[1, 1];
                        k[0, 1] = temp1;
                        k[1, 1] = temp2;
                    }
                    if (i < 2) a_1 = Evclid(k[0, i]);
                    else a_1 = Evclid(k[0, 1]);
                    if (RALF.IndexOf(Char) >= 0)
                    {
                        index = RALF.IndexOf(Char);
                        if (i < 2) newindex = (index - k[1,i]) * a_1;
                        else newindex = (index - k[1, 1]) * a_1;
                        newindex = Modulo(newindex);
                        using (StreamWriter newfile = new StreamWriter(newpath, true, Encoding.UTF8))
                        {
                            newfile.Write(RALF[newindex]);
                        }
                    }
                    else if (Ralf.IndexOf(Char) >= 0)
                    {
                        index = Ralf.IndexOf(Char);
                        if (i < 2) newindex = (index - k[1, i]) * a_1;
                        else newindex = (index - k[1, 1]) * a_1;
                        newindex = Modulo(newindex);
                        using (StreamWriter newfile = new StreamWriter(newpath, true, Encoding.UTF8))
                        {
                            newfile.Write(Ralf[newindex]);
                        }
                    }
                    i++;
                }
            }
        }
        private int Evclid(int a)
        {
            int q = 0;
            int y = 0;
            int n = this.n;
            int y2 = 0;
            int y1 = 1;
            int r = 1;
            while (r != 0)
            {
                q = n / a;
                r = n % a;
                y = y2 - q * y1;
                n = a;
                a = r;
                y2 = y1;
                y1 = y;
            }
            return Modulo(y2);
        }
        private int Modulo(int a)
        {
            if (a > n || a < 0)
            {
                if (a < 0)
                {
                    a = a % n;
                    a = a + n;
                }
                else a = a % n;
            }
            return a;
        }
        private static bool IsCoprime(int num1, int num2)
        {
            if (num1 == num2)
            {
                return num1 == 1;
            }
            else
            {
                if (num1 > num2)
                {
                    return IsCoprime(num1 - num2, num2);
                }
                else
                {
                    return IsCoprime(num2 - num1, num1);
                }
            }
        }
        private string GetNewPath(string path)
        {
            int index = path.LastIndexOf('\\');
            path = path.Remove(index + 1);
            return path;
        }
    }
}
