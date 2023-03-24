using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Практика_1
{
    class Program
    {
        static void Main(string[] args)
        {
            

            //Console.WriteLine("Введите а: ");
            //int a = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("Введите b: ");
            //int b = Convert.ToInt32(Console.ReadLine());

            //Console.WriteLine("Введите path: ");
            string path = @"C:\Users\Yesle\Desktop\_-_\КМЗИ\Практ1\1 фэнтези.txt";
            AffineCipher affine = new AffineCipher();
            //int a = 5;
            //int b = 2;
            //affine.Encrypt(a, b, path);

            affine.Decrypt(27, 4, path);

            //AffineRecurrentChipher affineRecurrent = new AffineRecurrentChipher();
            //int[,] d1 = { { 5, 8 }, { 2, 3 } };
            //int[,] d2 = { { 5, 8 }, { 2, 3 } };

            //affineRecurrent.Encrypt(d1, path);
            //path = @"C:\Users\Yesle\Desktop\_-_\КМЗИ\Практ1\cipher.txt";
            //affineRecurrent.Decrypt(d2, path);
        }
    }
}
