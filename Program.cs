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
            AffineCipher affine = new AffineCipher();

            Console.WriteLine("Введите а: ");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите b: ");
            int b = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введите path: ");
            string path = Console.ReadLine();// @"C:\Users\Yesle\Desktop\_-_\КМЗИ\Практ1\cipher.txt";
            affine.Decrypt(a, b, path);
        }
    }
}
