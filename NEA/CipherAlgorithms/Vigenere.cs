namespace NEA
{
    public static class Vigenere
    {
        static string fullKey(string key)
        {
            key = key.ToLower();
            int i = 0;
            while (key.Length < 26)
            {
                int c = 97 + i;
                if (!key.Contains((char)c))
                {
                    key += (char)c;
                }
                i++;
            }
            return key;
        }

        public static string Encrypt(string key, string plaintext)
        {
            
            key = fullKey(key);
            string output = "";
            for (int i = 0; i < plaintext.Length; i++)
            {
                if (CipherMathsFunctions.isLetter(plaintext[i]))
                {
                    output += (char)((plaintext[i] - 'a' + key[i % key.Length] - 'a') % 26 + 'a');                    
                }
                else
                {
                    output += plaintext[i];
                }
                
            }
            
            return output;
        }

        public static string Decrypt(string key, string ciphertext)
        {
            key = fullKey(key);
            string result = "";
            for (int i = 0; i < ciphertext.Length; i++)
            {
                if (CipherMathsFunctions.isLetter(ciphertext[i]))
                {
                    result += (char)((ciphertext[i] - key[i % key.Length] + 26) % 26 + 'a');
                }
                else
                {
                    result += ciphertext[i];
                }
                
            }
            return result;
        }

        public static int KeyLength(string plaintext)
        {
            double[] ioc = new double[26];
            for (int i = 1; i < 26; i++)
            {                
                for (int j = 0; j <i; j++)
                {
                    string chunk = "";
                    for (int k = j; k < plaintext.Length; k+=i)
                    {
                        chunk += plaintext[k];
                    }
                    ioc[i] += CryptanalysisFunctions.IndexOfCoincidence(CryptanalysisFunctions.CalculateLetterFrequencies(chunk));
                }
                ioc[i] /= i;
            }

            for(int i = 0;i < 26;i++)
            {
                Console.WriteLine(ioc[i]);
                if (ioc[i] == ioc.Min())
                {
                    //return i+1;                    
                }
            }
            return 0;
        }
    }
}
