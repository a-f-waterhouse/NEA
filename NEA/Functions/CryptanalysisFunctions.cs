using System.Diagnostics.Eventing.Reader;

namespace NEA
{
    public static class CryptanalysisFunctions
    {
        public static int Length(int[] a)
        {
            int length = 0;
            foreach (int c in a)
            {
                length += c;
            }
            return length;
        }

        public static double[] MonogramFrequencies()
        {
            return File.ReadAllText(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot/monogramFrequencies.txt")).Split(' ').Select(double.Parse).ToArray();
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

        public static Dictionary<string, int> CalculateBigramFrequencies(string input)
        {
            Dictionary<string, int> frequencies = new Dictionary<string, int>();
            for (int i = 0; i < input.Length-1; i++)
            {
                if(CipherMathsFunctions.isLetter(input[i])&& CipherMathsFunctions.isLetter(input[i+1]))
                {
                    string bigram = input[i].ToString() + input[i + 1].ToString();
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

        public static double IndexOfCoincidence(int[] frequencies) 
        {
            int length = Length(frequencies);

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
            int length = Length(CipherText);
            double X = 0;

            for (int i = 0; i < 26; i++)
            {
                double expected = length * (MonogramFrequencies()[i]);
                X += Math.Pow(CipherText[i] - expected, 2) / expected;
            }
            return X;
        }

        public static double VectorAngles(int[] CipherText)
        {
            double[] expected = new double[26];
            int length = Length(CipherText);
            for (int i = 0; i < 26; i++)
            {
                expected[i] = length * (MonogramFrequencies()[i]);
            }           
            return VectorMathsFunctions.Angle(new Vector(CipherText.Select(Convert.ToDouble).ToArray()), new Vector(expected));
        }

        public static double ShannonEntropy(int[] Ciphertext)
        {
            double entropy = 0;
            int length = Length(Ciphertext);

            foreach (int c in Ciphertext)
            {
                double frequency = (double)c / length;
                if(frequency!= 0)
                {
                    entropy -= (frequency * Math.Log2(frequency));
                }                
                Console.WriteLine(Math.Log2(frequency));
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
