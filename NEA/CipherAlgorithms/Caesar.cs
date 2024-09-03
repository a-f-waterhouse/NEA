using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace NEA
{
    public static class Caesar
    {
        public static string Encrypt(string plaintext, int key)
        {
            plaintext = plaintext.ToLower();
            string ciphertext = "";
            for (int i = 0; i < plaintext.Length; i++)
            {
                if (CipherMathsFunctions.isLetter(plaintext[i]))
                {
                    ciphertext += (char)((plaintext[i] - 97 + key) % 26 + 97);
                }
                else
                {
                    ciphertext += plaintext[i];
                }
                
            }

            return ciphertext;
        }

        public static string Decrypt(string ciphertext, int key)
        {
            ciphertext = ciphertext.ToLower();
            string plaintext = "";
            for (int i = 0; i < ciphertext.Length; i++)
            {
                if (CipherMathsFunctions.isLetter(ciphertext[i]))
                {
                    int a = ciphertext[i] - 97 - key;
                    if(a <0)
                    {
                        a += 26;
                    }
                    plaintext += (char)( a% 26 + 97);
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
