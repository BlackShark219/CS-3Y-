using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter multiplicand");
            int md = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter multiplier");
            int mr = Convert.ToInt32(Console.ReadLine());
            long product;
            long multiplicand;
            product = mr;
            multiplicand = md;
            multiplicand <<= 32;

            for (int i = 0; i < 32; i++)
            {
                Console.WriteLine("Iteration #" + i);
                Console.WriteLine("Product in binary: "+Convert.ToString(product,2));
                Console.WriteLine("Multiplicand in binary: " + Convert.ToString(multiplicand,2));
                long lsb = product & 1;
                if (lsb==1)
                {
                    Console.WriteLine("LSB is 1, add mul to prod");
                    if (i == 31)
                    {
                        Console.WriteLine("31st");
                        multiplicand *= -1;
                        product += 1;
                        Console.WriteLine("prod: " + Convert.ToString(product,2));
                        Console.WriteLine("mul: " + Convert.ToString(multiplicand, 2));
                    }
                    product += multiplicand;
                }
                product >>= 1;
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            }
            Console.WriteLine("Result is in binary: "+Convert.ToString(product,2));
            Console.WriteLine("Result is in decimal: " +product);
            Console.WriteLine(md + " * " + mr + " = " + md * mr);
            Console.ReadKey();
        }

    }
}
