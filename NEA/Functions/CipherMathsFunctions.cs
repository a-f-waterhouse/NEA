namespace NEA
{
    public class CipherMathsFunctions
    {
        public static bool isLetter(char c)
        {
            if(c >= 'a' && c <= 'z')
            {
                return true;
            }
            return false;
        }


        public static int modularMultiplicativeInverse(int a, int m)
        {
            for (int i = 1; i < m; i++)
            {
                if (a * i % m == 1)
                {
                    return i;
                }
            }
            return 0;
        }

        public static bool coprime(int a, int b)
        {
            if (a == 0 || b == 0)
            {
                return false;
            }

            for (int i = 2; i < a; i++)
            {
                if (a % i == 0 && b % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
