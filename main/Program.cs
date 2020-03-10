using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algoritm_RSA;
using System.IO;

namespace main
{
    class Program
    {
        static void Main(string[] args)
        {
            RSA rsa = new RSA();

            long p = 0;
            long q = 0;

            Console.Write("\nВведите простое число p: ");
            p = Convert.ToInt64(Console.ReadLine());

            Console.Write("\nВведите простое число q: ");
            q = Convert.ToInt64(Console.ReadLine());

            rsa.Initialization(p, q);

            string str = string.Empty;

            string path = Directory.GetCurrentDirectory();

            using (StreamReader sr = new StreamReader(path + "\\test.txt"))
            {
                str = sr.ReadToEnd();
            }

            str = rsa.Encryption(str);

            using (StreamWriter sw = new StreamWriter(path + "\\out1.txt"))
            {
                sw.WriteLine(str);
            }

            using (StreamReader sr = new StreamReader(path + "\\out1.txt"))
            {
                str = sr.ReadToEnd();
            }

            str = rsa.Decryption(str);

            using (StreamWriter sw = new StreamWriter(path + "\\out2.txt"))
            {
                sw.WriteLine(str);
            }

            Console.ReadKey();
        }
    }
}
