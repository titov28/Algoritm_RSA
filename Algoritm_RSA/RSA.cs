using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Algoritm_RSA
{
    public class RSA
    {
        private long p;
        private long q;

        public long P
        {
            get { return p; }
            set
            {
                if (CheckSimpleNumber(value))
                {
                    p = value;
                }
                else
                {
                    p = -1;
                    Console.WriteLine("Число p непростое");
                }
            }
        }

        public long Q
        {
            get { return q; }
            set
            {
                if (CheckSimpleNumber(value))
                {
                    q = value;
                }
                else
                {
                    q = -1;
                    Console.WriteLine("Число q непростое");
                }
            }
        }

        public long N { get; private set; } // произведение простых чисел
        public long M { get; private set; } // Значение функции Эйлера
        public long E { get; private set; } // Часть открытого ключа, фзаимнопростое с М
        public long D { get; private set; } // Часть закрытого ключа,

        public void Initialization(long p, long q)
        {
            this.P = p;
            this.Q = q;

            N = CalculateN();
            M = CalculateM();
            E = CalculateE();
            D = CalculateD();
        }

        private long CalculateN()
        {
            return P * Q;
        }

        private long CalculateM()
        {
            return (P - 1) * (Q - 1);
        }

        private long CalculateE()
        {
            long e = 2;

            for (long i = 2; i <= M; i++)
                if ((M % i == 0) && (e % i == 0)) //если имеют общие делители
                {
                    e++;
                    i = 1;
                }

            return e;
        }

        private long CalculateD()
        {
            long d = 0;
            long temp = 0;

            for(long i = 2; i < long.MaxValue; i++)
            {
                if (((M * i) + 1) % E == 0)
                {
                    temp = ((M * i) + 1) / E;

                    //if (temp > N)
                    {
                        d = temp;
                        break;
                    }
                }
            }

            return d;

        }

        private bool CheckSimpleNumber(long number)
        {
            for (long i = 2; i < number; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }


        //Шифрование текста
        public string Encryption(string str)
        {
            char[] ch = str.ToCharArray();
            string st = string.Empty;
            BigInteger result = 0;

            for(int i = 0; i < ch.Length; i++)
            {
                result = BigInteger.ModPow(Convert.ToInt16(ch[i]), E, N);
                st += result.ToString() + ' ';
            }

            return st;
        }

        //Дешифрование текста
        public string Decryption(string str)
        {
            string[] st = str.Split(' ');
            string tempStr = string.Empty;
            BigInteger result = 0;
            char symbol;

            for (int i = 0; i < st.Length -1; i++)
            {
                result = BigInteger.ModPow(Convert.ToInt16(st[i]), D, N);
                symbol = Convert.ToChar(Int16.Parse(result.ToString()));
                tempStr += symbol;
            }

            return tempStr;
        }
    }
}
