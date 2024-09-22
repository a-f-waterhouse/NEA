using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hill_Climbing
{
    internal class Program
    {
        static void Climbing()
        {
            string ciphertext = "attackatdawn";

            Random rnd = new Random();
            string key = "";
            while (key.Length < 26)
            {
                int c = rnd.Next(0, 26) + 97;
                if (!key.Contains((char)c))
                {
                    key += (char)c;
                }
            }

            while (true)
            {
                Dictionary<char, char> encryptionMap = new Dictionary<char, char>();
                Dictionary<char, char> decryptionMap = new Dictionary<char, char>();
                Substitution.getKey(key, ref encryptionMap, ref decryptionMap);
                string decryption1 = Substitution.MapCharacters(ciphertext, encryptionMap);

                string newKey = key;
                int index1 = rnd.Next(0, 26), index2 = rnd.Next(0, 26);
                if (index1 < index2)
                {
                    newKey = key.Substring(0, index1) + key[index2] + key.Substring(index1 + 1, index2 - index1 - 1) + key[index1] + key.Substring(index2 + 1);
                }
                else
                {
                    newKey = key.Substring(0, index2) + key[index1] + key.Substring(index2 + 1, index1 - index2 - 1) + key[index2] + key.Substring(index2 + 1);
                }

                Substitution.getKey(newKey, ref encryptionMap, ref decryptionMap);
                string decryption2 = Substitution.MapCharacters(ciphertext, encryptionMap);

                if (CryptanalysisFunctions.BigramFitness(decryption2) < CryptanalysisFunctions.BigramFitness(decryption1))
                {
                    key = newKey;
                }

                Console.WriteLine(key);
                Console.WriteLine(decryption1);
                Console.ReadKey();
            }

        }


        static void Main(string[] args)
        {

            Climbing();

            Console.ReadLine();


        }
    }
}
