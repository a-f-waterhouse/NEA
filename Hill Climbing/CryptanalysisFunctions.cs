using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Hill_Climbing
{
    public static class CryptanalysisFunctions
    {
        public static double BigramFitness(string text)
        {
            double total = 0;

            Dictionary<string, double> bigrams = new Dictionary<string, double>();
            using (StreamReader sr = new StreamReader("bigramFrequencies.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(' ');
                    bigrams.Add(line[0], Math.Log10(double.Parse(line[1])));
                }
            }

            for (int i = 0; i < text.Length - 1; i++)
            {
                if (CipherMathsFunctions.isLetter(text[i]) && CipherMathsFunctions.isLetter(text[i + 1]))
                {
                    total += bigrams[(text[i] + text[i + 1].ToString()).ToUpper()];
                }
            }
            return total;
        }

        public static int[] CalculateLetterFrequencies(string input)
        {
            input = input.ToLower();
            int[] f = new int[26];
            foreach (char c in input)
            {
                if (c - 97 < 26 && c - 97 >= 0)
                {
                    f[c - 97]++;
                }
            }
            return f;
        }

        public static Dictionary<string, double> getQuadgrams()
        {
            Dictionary<string, double> quadgrams = new Dictionary<string, double>();
            using (StreamReader sr = new StreamReader("quadgramFrequencies.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(' ');
                    quadgrams.Add(line[0], Math.Log10(double.Parse(line[1])));
                }
            }
            return quadgrams;
        }

        public static double QuadgramFitness(string text)
        {
            double total = 0;

            Dictionary<string, double> quadgrams = getQuadgrams();

            for (int i = 0; i < text.Length - 3; i++)
            {
                bool letters = true;
                foreach(char c in text.Substring(i, 4))
                {
                    if(!CipherMathsFunctions.isLetter(c))
                    {
                        letters = false;
                    }
                }
                if (letters)
                {
                    try
                    {
                        total += quadgrams[text.Substring(i, 4).ToUpper()];
                    }
                    catch {
                        total -= 10;
                    }
                                   
                }
            }
            return total;
        }



        public static double[] MonogramFrequencies()
        {
            return File.ReadAllText( "monogramFrequencies.txt").Split(' ').Select(double.Parse).ToArray();
        }


        public static Dictionary<string, int> CalculateBigramFrequencies(string input)
        {            
            Dictionary<string, int> frequencies = new Dictionary<string, int>();
            for (int i = 0; i < input.Length-1; i++)
            {
                if(CipherMathsFunctions.isLetter(input[i])&& CipherMathsFunctions.isLetter(input[i+1]))
                {
                    string bigram = input[i].ToString() + input[i + 1];
                    if (!frequencies.ContainsKey(bigram))
                    {
                        frequencies.Add(bigram, 1);
                    }
                    else
                    {
                        frequencies[bigram]++;
                    }
                }
            }
            return frequencies;
        } //EEEEEEEEEEEEee

        public static double indexOfCoincidence(int[] frequencies) 
        {
            int length = 0;
            foreach (int c in frequencies)
            {
                length += c;
            }

            double a = 0;
            foreach (int f in frequencies)
            {
                a += f * (f - 1);
            }
            try {
                return a / (length * (length - 1));
            }
            catch
            {
                return 0;
            }
            
        }

        public static double X2Stat(int[] CipherText)
        {
            int length = 0;
            foreach(int c in CipherText)
            {
                length += c;
            }
            double X = 0;

            for (int i = 0; i < 26; i++)
            {
                double expected = length * (MonogramFrequencies()[i]);
                X += Math.Pow(CipherText[i] - expected, 2) / expected;
            }
            return X;
        }


        public static double ShannonEntropy(int[] Ciphertext)
        {
            double entropy = 0;
            int length = 0;
            for (int i = 0; i < Ciphertext.Length; i++)
            {
                length += Ciphertext[i];
            }

            foreach (int c in Ciphertext)
            {
                double frequency = c / length;
                entropy -= (frequency * Math.Log(frequency, 2));
            }

            return entropy;
        }

        public static int NumberOfCommonWords(string text)
        {
            int count = 0;
            using (StreamReader sr = new StreamReader(""))
            {
                while (!sr.EndOfStream)
                {
                    string word = sr.ReadLine();
                    string t = text;
                    if(text.Contains(word))
                    {
                        count++;
                    }
                }
            }
            return count;


        }
    }
}
