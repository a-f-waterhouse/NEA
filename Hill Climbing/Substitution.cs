using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Hill_Climbing
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

            for (int i = 0; i < 26; i++)
            {
                encryptionMap.Add((char)(i + 97), key[i]);
                decryptionMap.Add(key[i], (char)(i + 97));
            }
        }
        public static string MapCharacters(string plaintext, Dictionary<char, char> key)
        {
            plaintext = plaintext.ToLower();
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
