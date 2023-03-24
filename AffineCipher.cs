using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Практика_1
{
    class AffineCipher
    {
        int n = 32;
        string RALF = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        string Ralf = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        public AffineCipher() { }
        public void Encrypt(int a, int b, string path)
        {
            if (IsCoprime(a, n) == false)
            {
                Console.WriteLine($"а={a} и n={n} невзаимно простые числа");
                return;
            }
            string newpath = GetNewPath(path) + "cipher.txt";
            using (StreamWriter newfile = new StreamWriter(newpath, false, Encoding.UTF8))
            {
                newfile.Close();
            }

            using (StreamReader file = new StreamReader(path, Encoding.UTF8))
            {
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
                    }
                    if (RALF.IndexOf(Char) >= 0)
                    {
                        index = RALF.IndexOf(Char);
                        newindex = a * index + b;
                        newindex = Modulo(newindex);
                        using (StreamWriter newfile = new StreamWriter(newpath, true, Encoding.UTF8))
                        {
                            newfile.Write(RALF[newindex]);
                        }
                    }
                    else if (Ralf.IndexOf(Char) >= 0)
                    {
                        index = Ralf.IndexOf(Char);
                        newindex = a * index + b;
                        newindex = Modulo(newindex);
                        using (StreamWriter newfile = new StreamWriter(newpath, true, Encoding.UTF8))
                        {
                            newfile.Write(Ralf[newindex]);
                        }
                    }
                }
            }
        }
        public void Decrypt(int a, int b, string path)
        {
            if (IsCoprime(a, n) == false)
            {
                Console.WriteLine($"а={a} и n={n} невзаимно простые числа");
                return;
            }
            int a_1 = Evclid(a);
            string newpath = GetNewPath(path) + "opentext.txt";
            using (StreamWriter newfile = new StreamWriter(newpath, false, Encoding.UTF8))
            {
                newfile.Close();
            }

            using (StreamReader file = new StreamReader(path, Encoding.UTF8))
            {
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
                    }
                    else
                    {
                        if (RALF.IndexOf(Char) >= 0)
                        {
                            index = RALF.IndexOf(Char);
                            newindex = (index - b) * a_1;
                            newindex = Modulo(newindex);
                            using (StreamWriter newfile = new StreamWriter(newpath, true, Encoding.UTF8))
                            {
                                newfile.Write(RALF[newindex]);
                            }
                        }
                        else if (Ralf.IndexOf(Char) >= 0)
                        {
                            index = Ralf.IndexOf(Char);
                            newindex = (index - b) * a_1;
                            newindex = Modulo(newindex);
                            using (StreamWriter newfile = new StreamWriter(newpath, true, Encoding.UTF8))
                            {
                                newfile.Write(Ralf[newindex]);
                            }
                        }
                    }
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
