using System.Diagnostics.Eventing.Reader;

namespace NEA
{
    public static class CryptanalysisFunctions
    {
        private static Random rnd = new Random();
        private static Dictionary<string, double> ExpectedBigrams = ExpectedBigramFrequencies();



        public static int Length(int[] a)
        {
            int length = 0;
            foreach (int c in a)
            {
                length += c;
            }
            return length;
        }

        public static double[] ExpectedMonogramFrequencies()
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

        private static Dictionary<string, double> ExpectedBigramFrequencies()
        {
            Dictionary<string, double> bigrams = new Dictionary<string, double>();
            using (StreamReader sr = new StreamReader(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot/bigramFrequencies.txt")))
            {
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(' ');
                    bigrams.Add(line[0], Math.Log10(double.Parse(line[1])));
                }
            }
            return bigrams;
        }       

        public static double BigramFitness(string text)
        {
            text = text.ToLower();
            double total = 0;

            Dictionary<string, double> bigrams = ExpectedBigrams;

            for (int i = 0; i < text.Length - 1; i++)
            {
                if (CipherMathsFunctions.isLetter(text[i]) && CipherMathsFunctions.isLetter(text[i + 1]))
                {
                    total += bigrams[(text[i] + text[i + 1].ToString()).ToUpper()];
                }
            }
            return total;
        }

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

        public static double IndexOfCoincidence(string input)
        {
            return IndexOfCoincidence(CalculateLetterFrequencies(input));
        }

        public static double X2Stat(int[] CipherText)
        {
            int length = Length(CipherText);
            double X = 0;

            for (int i = 0; i < 26; i++)
            {
                double expected = length * (ExpectedMonogramFrequencies()[i]);
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
                expected[i] = length * (ExpectedMonogramFrequencies()[i]);  
            }           
            return VectorMathsFunctions.Angle(new Vector(CipherText.Select(Convert.ToDouble).ToArray()), new Vector(expected)) * 180 /Math.PI;
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
                
            }
            return entropy;
        }

        public static string HillClimbing(string ciphertext, int iterations, int limit)
        {
            Dictionary<double, string> decryptions = new Dictionary<double, string>();

            for (int i = 0; i < iterations; i++)
            {
                Console.WriteLine(i);
                string key = "";
                while (key.Length < 26)
                {
                    int c = rnd.Next(0, 26) + 97;
                    if (!key.Contains((char)c))
                    {
                        key += (char)c;
                    }
                }
                int count = 0;

                Dictionary<char, char> encryptionMap = new Dictionary<char, char>();
                Dictionary<char, char> decryptionMap = new Dictionary<char, char>();
                Substitution.getKey(key, ref encryptionMap, ref decryptionMap);

                string d = Substitution.MapCharacters(ciphertext, encryptionMap);
                double fCurrent = BigramFitness(d), f2;

                while (count < limit)
                {
                    char[] newKey = key.ToCharArray();
                    int index1 = rnd.Next(0, 26), index2 = rnd.Next(0, 26);

                    (newKey[index2], newKey[index1]) = (newKey[index1], newKey[index2]);
                    Substitution.getKey(new string(newKey), ref encryptionMap, ref decryptionMap);
                    string decryption2 = Substitution.MapCharacters(ciphertext, encryptionMap);

                    f2 = BigramFitness(decryption2);
                    if (fCurrent < f2)
                    {
                        key = new string(newKey);
                        count = 0;
                        d = decryption2;
                        fCurrent = f2;
                    }

                    count++;
                }
                decryptions.TryAdd(fCurrent, d);
            }
            return decryptions[decryptions.Keys.Max()];
        }
    }
}
