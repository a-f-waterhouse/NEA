namespace NEA
{
    public static class Affine
    {
        public static string Encrypt(int a, int b, string plaintext)
        {
            string result = "";
            foreach (char c in plaintext)
            {
                if(CipherMathsFunctions.isLetter(c))
                {
                    result += (char)((a * (c - 97) + b) % 26 + 97);
                }
                else
                {
                    result += c;
                }
                
            }
            return result;
        }

        public static string Decrypt(int a, int b, string ciphertext)
        {
            string result = "";
            int mMI = CipherMathsFunctions.modularMultiplicativeInverse(a, 26);
            foreach (char c in ciphertext)
            {
                if (CipherMathsFunctions.isLetter(c))
                {
                    result += (char)(mMI * (c - 97 - b + 26) % 26 + 97);
                }
                else
                {
                    result += c;
                }                
            }
            return result;
        }
    }
}
