using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_2_
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter dividend");
            long dividend = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("Enter divisior");
            long divisior = Convert.ToInt64(Console.ReadLine());
            int quot=0;
            long signmask;
            signmask = 1;
            signmask <<= 63;
            divisior <<= 32;
            long signdd = dividend & signmask;
            long signdr = divisior & signmask;
            if (signdd != 0) dividend *= -1;
            if (signdr != 0) divisior *= -1;
            for (int i =0;i<33; i++)
            {
                Console.WriteLine("Iteration #" + i);
                Console.WriteLine("divisior in binary: " + Convert.ToString(divisior, 2));
                Console.WriteLine("dividend in binary: " + Convert.ToString(dividend, 2));
                dividend -=divisior;
                if (dividend>=0)
                {
                    quot <<= 1;
                    quot |= 1;
                }
                else
                {
                    dividend += divisior;
                    quot <<= 1;
                }
                divisior >>= 1;
                Console.WriteLine("quotient in binary: " + Convert.ToString(quot, 2));
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            }
            if (signdd ==1)
            {
                dividend *= -1;
            }
            if (signdd != signdr)
            {
                quot *=-1;
            }
            Console.WriteLine("quot in binary: " + Convert.ToString(quot, 2));
            Console.WriteLine("dividend in binary: "+ Convert.ToString(dividend, 2));
            Console.WriteLine("quot in decimal: " + quot);
            Console.WriteLine("dividend in decimal: " + dividend);
            Console.WriteLine("sign dividend:" + Convert.ToString(signdd, 2));
            Console.WriteLine("sign divisior:" + Convert.ToString(signdr, 2));
            Console.ReadKey();
        }
    }
}
