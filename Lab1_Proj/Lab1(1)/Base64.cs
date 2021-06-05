using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab1_1_
{
    public static class Base64
    {
        public static byte[] GetTextInBytes(string pathFile)
        {
            Encoding encode = Encoding.UTF8;

            string allText = File.ReadAllText(pathFile);
            char[] chars = allText.ToCharArray(0, allText.Length);
            byte[] bytes = encode.GetBytes(chars);

            return bytes;
        }

        public static string Encode(byte[] array)
        {
            string[] alphabet = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "+", "/" };
            string bit_str = "";
            string base64_str = "";

            foreach (byte x in array)
            {
                bit_str += Convert.ToString(x, 2).PadLeft(8, '0');
            }
            int chunkSize = 6;
            int str_length = bit_str.Length;

            for (int i = 0; i < str_length; i += chunkSize)
            {
                if (i + chunkSize > str_length) chunkSize = str_length - i;

                string bits = bit_str.Substring(i, chunkSize).PadRight(6, '0');
                int num = Convert.ToInt32(bits, 2);
                base64_str += alphabet[num];
            }

            int pad = base64_str.Length % 4;
            if (pad == 3)
                base64_str += "=";
            else if (pad == 2)
                base64_str += "==";

            return base64_str;
        }

        public static void WriteFile(string pathFile, string text)
        {
            using (StreamWriter file = new StreamWriter(pathFile, false, Encoding.UTF8))
            {
                file.WriteLine(text);
            }
        }
    }
}
