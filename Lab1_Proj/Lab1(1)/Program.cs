using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_1_
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<char, double> alph = new Dictionary<char, double>();
            string[] archives = new string[] { ".rar", ".zip", ".7z", ".bz2", ".xz" };
            string path = @"D:\3-й курс\2-й семестр\Комп'ютерні системи\Lab1_Proj\";// шлях до текстових файлів
            Console.WriteLine("Name of file: ");
            path = path + Console.ReadLine() + ".txt";
            //path = path + Console.ReadLine() + ".txt_64.txt";
            //path = path + Console.ReadLine() + "_64.txt.bz2";
            //Console.WriteLine("================================");
            string text = ReadFile(path);
            TextCheck(text, alph);
            double entropy = CountEntropy(alph);
            int length = text.Length;
            Console.WriteLine(text);
            GetInfo(path, entropy, length);
            ShowDict(alph);
            //CompareArchives(path, archives);
            //Part 2
            //string encrypttext = Base64.Encode(Base64.GetTextInBytes(path));
            //Base64.WriteFile(path + "_64.txt", encrypttext);

            Console.ReadLine();
           
        }

        static string ReadFile(string pathFile)
        {
            using (StreamReader file = new StreamReader(pathFile, Encoding.GetEncoding(1251)))
            {
                string ln;
                string text = "";

                while ((ln = file.ReadLine()) != null)
                {
                    text += ln + "\n";
                }
                return text;
            }
        }

        static void TextCheck(string text, Dictionary<char,double> dict)
        {
            var alphabet = text.ToCharArray().Distinct().ToList();
            var textlength = text.Length;
            for (int i = 0; i < alphabet.Count(); i++)
            {   
                double quant= text.Count(x => x == alphabet[i]);
                double freq = quant / textlength;
                dict.Add(alphabet[i], freq);
            }
        }

        static void ShowDict(Dictionary<char,double> dict)
        {
            Console.WriteLine("Letter      Frequency");
            foreach(var x in dict)
            {
                Console.WriteLine("{0}      {1,23}",x.Key,x.Value);
            }
        }

        static double CountEntropy(Dictionary<char,double> dict)
        {
            double entropy = 0;
            foreach(var x in dict)
            {
                entropy += x.Value * Math.Log(1 / x.Value, 2);
            }
            return entropy;
        }

        static double GetInfo(string path,double entropy,int length)
        {   
            
            FileInfo file = new FileInfo(path);
            double amountOfInformation;
            Console.WriteLine("Number of letters in text: {0}", length);
            Console.WriteLine("Entropy (bits): {0:F4}", entropy);
            Console.WriteLine("Ammount of information (bits): {0:F4}", entropy * length);
            Console.WriteLine("Ammount of information (bytes): {0:F4}\n", amountOfInformation = entropy * length / 8);
            Console.WriteLine("Filesize: {0} bytes", file.Length);
            return amountOfInformation;
        }


        static void CompareArchives(string path,string [] archives)
        {
            foreach (string end in archives)
            {
                FileInfo file = new FileInfo(path + end);
                Console.WriteLine("{0}: {1}", end, file.Length);
            }

        }

    }
}
