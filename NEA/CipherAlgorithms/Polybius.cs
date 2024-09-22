namespace NEA
{
    public static class Polybius
    {
        public static string Encrypt(string plaintext, char[,] key)
        {
            string output = "";
            for(int x = 0; x < plaintext.Length; x++)
            {
                char c = plaintext[x];
                if (CipherMathsFunctions.isLetter(c))
                {
                    if(c == 'j')
                    {
                        c = 'i';
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            if (key[i, j] == c)
                            {
                                output += (j + 1).ToString() + (i+1).ToString();
                            }
                        }
                    }
                }

            }
            return output;
        }

        public static string Decrypt(string ciphertext, char[,] key)
        {
            string plaintext = "";
            for (int i = 0; i < ciphertext.Length-1; i++)
            {
                int a = 0, b = 0;
                
                if (int.TryParse(ciphertext[i].ToString(), out a) && int.TryParse(ciphertext[i+1].ToString(), out b))
                {
                    plaintext += key[b-1, a-1];
                    i++;
                }
                else
                {
                    plaintext += ciphertext[i];
                }
            }
            return plaintext;
        }

    }
}
