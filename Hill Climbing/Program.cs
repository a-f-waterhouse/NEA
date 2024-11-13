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
        static Random rnd = new Random();
        static string randomKey()
        {            
            string key = "";
            while (key.Length < 26)
            {
                int c = rnd.Next(0, 26) + 97;
                if (!key.Contains((char)c))
                {
                    key += (char)c;
                }
            }
            return key;
        }

        public static int KeyLength(string plaintext)
        {
            plaintext = OnlyLetters(plaintext);
            double[] ioc = new double[25];
            for (int i = 1; i < 26; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    string chunk = "";
                    for (int k = j; k < plaintext.Length; k += i)
                    {                        
                        chunk += plaintext[k];
                    }
                    Console.Write(chunk);
                    if(chunk.Length > 1)
                    {
                        ioc[i-1] += CryptanalysisFunctions.IndexOfCoincidence(CryptanalysisFunctions.CalculateLetterFrequencies(chunk));
                    }                    
                    
                }
                Console.WriteLine();
                ioc[i - 1] /= i;
                           
            }

            for (int i = 0; i < 25; i++)
            {
                Console.WriteLine(ioc[i]);
                if (ioc[i] > 0.06)
                {                    
                    return i+1;
                }
            }

            return (Array.IndexOf(ioc, ioc.Max()));
        }        


        static string Climbing(string ciphertext, int iterations, int limit)
        {
            Dictionary<double, string> decryptions = new Dictionary<double, string>();

            for (int i = 0; i < iterations; i++)
            {
                Console.WriteLine(i);
                string key = randomKey();
                int count = 0;                

                Dictionary<char, char> encryptionMap = new Dictionary<char, char>();
                Dictionary<char, char> decryptionMap = new Dictionary<char, char>();
                Substitution.getKey(key, ref encryptionMap, ref decryptionMap);

                string d = Substitution.MapCharacters(ciphertext, encryptionMap);
                double fCurrent = CryptanalysisFunctions.BigramFitness(d), f2;

                while (count < limit)
                {
                    char[] newKey = key.ToCharArray();
                    int index1 = rnd.Next(0, 26), index2 = rnd.Next(0, 26);

                    char temp = newKey[index1];
                    newKey[index1] = newKey[index2];
                    newKey[index2] = temp;

                    Substitution.getKey(new string(newKey), ref encryptionMap, ref decryptionMap);
                    string decryption2 = Substitution.MapCharacters(ciphertext, encryptionMap);

                    f2 = CryptanalysisFunctions.BigramFitness(decryption2);
                    if (fCurrent < f2)
                    {
                        key = new string(newKey);
                        count = 0;
                        d = decryption2;
                        fCurrent = f2;
                    }                  

                    count++;
                }
                if(!decryptions.ContainsKey(fCurrent))
                {
                    decryptions.Add(fCurrent, d);
                }
            }
            return decryptions[decryptions.Keys.Max()];
        }

        static void Climbing(string ciphertext, char method, string[] keys)
        {
            int count = 0;
            double x = 0;
            string d = "";
            Dictionary<string, double> bestKeys = new Dictionary<string, double>();

            for (int i = 0; i < 10; i++)
            {
                string key = keys[i];
                Console.WriteLine(i);
                count = 0;

                while (count < 500)
                {
                    Dictionary<char, char> encryptionMap = new Dictionary<char, char>();
                    Dictionary<char, char> decryptionMap = new Dictionary<char, char>();
                    Substitution.getKey(key, ref encryptionMap, ref decryptionMap);
                    string decryption1 = Substitution.MapCharacters(ciphertext, encryptionMap);

                    char[] newKey = key.ToCharArray();
                    int index1 = rnd.Next(0, 26), index2 = rnd.Next(0, 26);

                    char temp = newKey[index1];
                    newKey[index1] = newKey[index2];
                    newKey[index2] = temp;

                    Substitution.getKey(new string(newKey), ref encryptionMap, ref decryptionMap);
                    string decryption2 = Substitution.MapCharacters(ciphertext, encryptionMap);

                    if (method == 'B')
                    {
                        if (CryptanalysisFunctions.BigramFitness(decryption2) > CryptanalysisFunctions.BigramFitness(decryption1))
                        //if (CryptanalysisFunctions.X2Stat(CryptanalysisFunctions.CalculateLetterFrequencies(decryption1)) > CryptanalysisFunctions.X2Stat(CryptanalysisFunctions.CalculateLetterFrequencies(decryption2)))
                        {
                            key = new string(newKey);
                            count = 0;
                            x = CryptanalysisFunctions.BigramFitness(decryption2);
                            d = decryption2;

                        }
                    }
                    else if (method == 'C')
                    {

                        if (CryptanalysisFunctions.X2Stat(CryptanalysisFunctions.CalculateLetterFrequencies(decryption1)) > CryptanalysisFunctions.X2Stat(CryptanalysisFunctions.CalculateLetterFrequencies(decryption2)))
                        {
                            key = new string(newKey);
                            count = 0;
                            x = CryptanalysisFunctions.X2Stat(CryptanalysisFunctions.CalculateLetterFrequencies(decryption2));
                            d = decryption2;

                        }
                    }

                    count++;
                }
                if (!bestKeys.ContainsKey(d))
                {
                    bestKeys.Add(d, x);
                    Console.WriteLine(d);
                }
            }

        }

        static string OnlyLetters(string input)
        {
            string output = "";
            foreach (char c in input)
            {
                if (CipherMathsFunctions.isLetter(c))
                {
                    output += c;
                }
            }
            return output.ToUpper();
        }



        static void Main(string[] args)
        {
            string text = "lblxd xikv ou jx wdtjxxdxji ejpxtmg xkubnxps! a fjrc jy elvuphw hvm v ifjm jb pfkq nmqjhwy jdd hxzk cn lsq htcc hxs hvhwbfu ";
 
            Console.WriteLine(KeyLength(text));

            Console.ReadLine();


        }
    }
}
