using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_2_
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.OutputEncoding = Encoding.Unicode;

            string folderpath = @"D:\3-й курс\2-й семестр\Комп'ютерні системи\Lab1_Proj\";
            string textFile;
            int AllChar;
            long fileSize;
            double entropy;
            double infoQuant;
            Dictionary<char, double> characters = new Dictionary<char, double>();


            Console.WriteLine("Original file:");
            textFile = Console.ReadLine();
            Console.WriteLine();


            var path = folderpath + textFile + ".txt";


            fileSize = ReadFile(path, characters, out AllChar);
            CountFrequency(characters, AllChar);
            entropy = CountEntropy(characters);
            infoQuant = AmountofInf(entropy, AllChar);
            PrintInfo(infoQuant, fileSize);


            fileSize = ReadFile(folderpath + textFile + "_64.txt", characters, out AllChar);
            CountFrequency(characters, AllChar);
            entropy = CountEntropy(characters);
            infoQuant = AmountofInf(entropy, AllChar);
            PrintInfo(infoQuant, fileSize);

            string encrypttext = Encode(GetTextInBytes(path));



            WriteFile(folderpath + textFile + "_64.txt", encrypttext);


            Console.WriteLine("Encoded File Info:");
            PrintInfo(infoQuant, fileSize);
            Console.WriteLine("After manual Base64 encodeing: ");
            Console.WriteLine(encrypttext);


            Console.WriteLine("Archived File Info:");
            fileSize = ReadFile(folderpath + textFile + "_64.txt.bz2", characters, out AllChar);
            CountFrequency(characters, AllChar);
            entropy = CountEntropy(characters);
            PrintInfo(AmountofInf(entropy, AllChar), fileSize);

            Console.ReadLine();

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
        public static void CountFrequency(Dictionary<char, double> chrcts, int totalCount)
        {
            int countKeysDict = chrcts.Keys.Count;
            char[] keysDict = new char[countKeysDict];
            chrcts.Keys.CopyTo(keysDict, 0);

            for (int i = 0; i < countKeysDict; i++)
            {
                chrcts[keysDict[i]] = chrcts[keysDict[i]] / totalCount;
            }
        }

        public static double CountEntropy(Dictionary<char, double> chrcts)
        {
            int countChrct = chrcts.Keys.Count;
            char[] keysDict = new char[countChrct];
            chrcts.Keys.CopyTo(keysDict, 0);
            double frequency = 0, entropy = 0;

            for (int iter = 0; iter < countChrct; iter++)
            {
                frequency = chrcts[keysDict[iter]];
                entropy += frequency * Math.Log(1/ frequency, 2);
            }
            return entropy;
        }

        public static double AmountofInf(double entrp, int chrctCount)
        {
            return entrp * chrctCount;
        }

        public static void PrintInfo(double infoQuant, long fileSize)
        {
            Console.WriteLine("Size of file = {0} bytes", fileSize);
            Console.WriteLine("Info Quantity = {0} bytes", infoQuant / 8);
            Console.WriteLine("Info Quantity = {0} bits\n", infoQuant);
        }

        public static long ReadFile(string pathFile, Dictionary<char, double> chrcts, out int totalCountChrcts)
        {
            FileInfo fileSize = new FileInfo(pathFile);
            int i = 0;
            double chrctrep;
            totalCountChrcts = 0;

            string text = File.ReadAllText(pathFile);
            while (i < text.Length)
            {
                chrctrep = 1;
                if (!chrcts.ContainsKey(text[i]))
                {
                    chrcts.Add(text[i], chrctrep);
                }
                else
                    if (chrcts.ContainsKey(text[i]))
                {
                    chrcts[text[i]]++;
                }
                i++;
                totalCountChrcts++;
                
            }
            return fileSize.Length;
        }

        public static void WriteFile(string pathFile, string text)
        {
            using (StreamWriter sw = new StreamWriter(pathFile, false, Encoding.UTF8))
            {
                sw.WriteLine(text);
            }
        }
        public static byte[] GetTextInBytes(string pathFile)
        {
            Encoding encode = Encoding.UTF8;

            string allText = File.ReadAllText(pathFile);
            char[] chars = allText.ToCharArray(0, allText.Length);
            byte[] bytes = encode.GetBytes(chars);

            return bytes;
        }

    }
}
