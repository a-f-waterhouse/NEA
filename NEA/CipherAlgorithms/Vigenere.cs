namespace NEA
{
    public static class Vigenere
    {
        public static string Encrypt(string key, string plaintext)
        {
            string output = "";
            for (int i = 0; i < plaintext.Length; i++)
            {
                output += (char)((plaintext[i] - 'a' + key[i % key.Length] - 'a') % 26 + 'a');
            }
            return output;
        }

        public static string Decrypt(string key, string ciphertext)
        {
            string result = "";
            for (int i = 0; i < ciphertext.Length; i++)
            {
                result += (char)((ciphertext[i] - key[i % key.Length] + 26) % 26 + 'a');
            }
            return result;
        }
    }
}
