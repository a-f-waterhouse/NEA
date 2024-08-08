namespace NEA
{
    public class CryptanalysisFunctions
    {
        private static double[] expectedMonogramFrequencies = { };

        public static double indexOfCoincidence(string C) //index of coincidence in English = 0.0667, random = 0.0385, closer to random it is, the more alphabets used.
        {
            int[] frequencies = new int[26];
            foreach (char c in C)
            {
                frequencies[c - 97]++;
            }
            int a = 0;
            foreach (int f in frequencies)
            {
                a += f * (f - 1);
            }
            return a / (C.Length * (C.Length - 1));
        }

        public static double X2Stat(int[] C, int length)
        {
            double X = 0;
            double[] expected = expectedMonogramFrequencies;

            for (int i = 0; i < expected.Length; i++)
            {
                expected[i] = length * (expected[i] / 100);
                X += Math.Pow(C[i] - expected[i], 2) / expected[i];
            }
            return X;
        } //measurement of how closely a data set matches expected values, smaller is better
    }
}
