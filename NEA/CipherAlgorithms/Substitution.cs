namespace NEA
{
    public static class Substitution
    {
        public static void getKey(string key, ref Dictionary<char, char> encryptionMap, ref Dictionary<char, char> decryptionMap)
        {
            for (int i = 0; i < 26; i++)
            {
                if(!key.Contains((char)(i+97)))
                {
                    key += (char)(i + 97);
                }
            }

            for (int i = 0; i < 26; i++)
            {
                encryptionMap.Add((char)(i + 97), key[i]);
                encryptionMap.Add(key[i], (char)(i + 97));
            }
        }
        public static string MapCharacters(string plaintext, Dictionary<char, char> key)
        {
            string output = "";
            foreach (char c in plaintext)
            {
                if (CipherMathsFunctions.isLetter(c))
                {
                    output += key[c];
                }
                else
                {
                    output += c;
                }
            }
            return output;
        }
    }
}
