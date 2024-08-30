namespace NEA
{
    public class CryptanalysisFunctions
    {
        public static double[] MonogramFrequencies()
        {
            return File.ReadAllText(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot/monogramFrequencies.txt")).Split(' ').Select(double.Parse).ToArray();
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

        public static double X2Stat(int[] C)
        {
            int length = 0;
            foreach(int c in C)
            {
                length += c;
            }
            double X = 0;

            for (int i = 0; i < 26; i++)
            {
                double expected = length * (MonogramFrequencies()[i]);
                X += Math.Pow(C[i] - expected, 2) / expected;
            }
            return X;
        }
    }
}
