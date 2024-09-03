namespace NEA
{
    public static class CryptanalysisFunctions
    {
        public static double[] MonogramFrequencies()
        {
            return File.ReadAllText(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot/monogramFrequencies.txt")).Split(' ').Select(double.Parse).ToArray();
        }

        public static int[] CalculateLetterFrequencies(string input)
        {
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
        }

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

        public static double VectorAngles(int[] CipherText)
        {
            double[] expected = new double[26];
            int length = 0;
            foreach (int c in CipherText)
            {
                length += c;
            }
            for (int i = 0; i < 26; i++)
            {
                expected[i] = length * (MonogramFrequencies()[i]);
            }
            return VectorMathsFunctions.angle(new Vector(CipherText.Select(Convert.ToDouble).ToArray()), new Vector(expected));
        }
    }
}
