namespace NEA
{
    public static class Substitution
    {
        public static void getKey(string key, ref Dictionary<char, char> encryptionMap, ref Dictionary<char, char> decryptionMap)
        {
            encryptionMap = new Dictionary<char, char>();
            decryptionMap = new Dictionary<char, char>();
            for (int i = 0; i < 26; i++)
            {
                if(!key.Contains((char)(i+97)))
                {
                    key += (char)(i + 97);
                }
            }
            if(key.Length > 26)
            {
                string newKey = "";
                foreach(char c in key)
                {
                    if(!newKey.Contains(c) && CipherMathsFunctions.isLetter(c))
                    {
                        newKey += c;
                    }
                }
                key = newKey;
            }
            
            for (int i = 0; i < 26; i++)
            {
                encryptionMap.Add((char)(i + 97), key[i]);
                decryptionMap.Add(key[i], (char)(i + 97));
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
