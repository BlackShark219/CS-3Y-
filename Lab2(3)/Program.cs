using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Lab2_3_
{
    class Program
    {

        unsafe static int add(int m1, int m2, int* ex3, int* sr)
        {
          
                int result = m1 + m2;
                Console.WriteLine("Addition result: " + Convert.ToString(result, 2));

                if ((result & 0x80000000) != 0)
                {
                    Console.WriteLine("Result is negative, make 2's complement and set sign to 1");
                    result = -result;
                    *sr = 1;
                    Console.WriteLine("2's complement: " + Convert.ToString(result, 2));
                }
                else
                {
                    Console.WriteLine("sign is 0, addition is positive");
                    *sr = 0;
                }
                if ((result & 0xFF000000) != 0)
                {
                    Console.WriteLine("Normilizing by shifting to the right");
                    result >>= 1;
                    Console.WriteLine("Normalized: " + Convert.ToString(result, 2));
                    (*ex3)++;
                }
                else if ((result & 0x00800000) == 0)
                {
                    Console.WriteLine("Normalizing by shifting to the left");
                    while ((result & 0x00800000) == 0)
                    {
                        Console.WriteLine("Shift one bit to left, decrement exp");
                        result <<= 1;
                        (*ex3)--;
                        Console.WriteLine("Result: " + Convert.ToString(result, 2));
                    }
                }
                else Console.WriteLine("The result is already normalized");
                result ^= 0x00800000;
                Console.WriteLine("Result of addition after removing the extra 1: " + Convert.ToString(result, 2));
                return result;
            
        }
        static unsafe void Main(string[] args)
        {
            int power_mask, sign_mask, mantissa_mask, first_mask, difference;
            int sn1, sn2,sr;
            int ex1, ex2, ex3;
            int m1, m2, mr;
            sign_mask = 1;
            sign_mask <<= 31;
            power_mask = 0x7F800000;
            mantissa_mask = 0x007FFFFF;
            first_mask = 0x00800000;
            ADD_VAR n1 = new ADD_VAR();
            ADD_VAR n2 = new ADD_VAR();
            ADD_VAR res = new ADD_VAR();
            Console.WriteLine("Enter first float:");
            n1.fp= float.Parse(Console.ReadLine());
            Console.WriteLine("Enter second float:");
            n2.fp = float.Parse(Console.ReadLine());
            Console.WriteLine("first number binary:" + Convert.ToString(n1.ip,2).PadLeft(32, '0'));
            Console.WriteLine("first number binary:" + Convert.ToString(n2.ip, 2).PadLeft(32,'0'));
            sn1 = n1.ip & sign_mask;
            sn2 = n2.ip & sign_mask;

            ex1 = ((n1.ip & power_mask) >> 23) - 127;
            ex2 = ((n2.ip & power_mask) >> 23) - 127;

            m1 = (n1.ip & mantissa_mask) | first_mask;
            m2 = (n2.ip & mantissa_mask) | first_mask;

            Console.WriteLine("n1's sign: " + sn1);
            Console.WriteLine("n2's sign: " + sn2);

            Console.WriteLine("n1's exp: " + ex1);
            Console.WriteLine("n2's exp: " + ex2);

            Console.WriteLine("n1's mantissa: " + Convert.ToString(m1,2));
            Console.WriteLine("n2's mantissa: " + Convert.ToString(m2,2));

            difference = ex1 - ex2;

            if (difference > 0)
            {
                m2 >>= difference;
                ex2 += difference;
            }
            else if (difference < 0)
            {
                m1 >>= -difference;
                ex1 -= difference;
            }
            else Console.WriteLine( "EXP are equal, no need for binary point alignment");

            ex3 = ex1;
            if (sn1!=0) m1 = -m1;
            if (sn2!=0) m2 = -m2;
            mr = add(m1, m2, &ex3, &sr);
            res.ip = sr;
            res.ip <<= 8;
            res.ip |= ex3 + 127;
            res.ip <<= 23;
            res.ip |= mr;
            Console.WriteLine("Result in binary: "+ Convert.ToString(res.ip,2));
            Console.WriteLine("Result in decimal: " + res.fp);
            Console.WriteLine("Check the algorithm:" + n1.fp + " + " + n2.fp + " = " +(n1.fp + n2.fp));
            Console.ReadKey();
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct ADD_VAR
    {
        [FieldOffset(0)] public int ip;
        [FieldOffset(0)] public float fp;
    }
}
