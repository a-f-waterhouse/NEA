using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ciphers
{
    internal class Program
    {
        static string PolybiusEncrypt(string input)
        {
            input = input.ToLower();
            string output = "";
            for(int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if(c >= 'a' && c <= 'z')
                {
                    if( c >= 'j')
                    {
                        c--;
                    }                    
                    output += ((c - 97) / 5 + 1).ToString();
                    output += ((c - 97) % 5 + 1).ToString();
                }
                else if(c == ' ')
                {
                    output += ' ';
                }
            }
            return output;
        }

        static string RailfenceEncrypt(string input, int key) 
        {
            string output = "";
            char[,] matrix = new char[input.Length, key];
            int row = 0;
            bool down = false;
            for (int i = 0; i < input.Length; i++)
            {                
                matrix[i,row] = input[i];
                if(row%(key-1) == 0)
                {
                    down = !down;
                }
                if(down)
                {
                    row++;
                }
                else 
                {
                    row--;
                }
                               
            }
            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < input.Length; j++)
                {                    
                    output += (matrix[j,i]);
                }               
            }
            return output;
        }

        static string RailfenceDecrypt(string input, int key)
        {
            string output = "";
            char[,] matrix = new char[input.Length, key];

            int row = 0;
            bool down = false;

            for (int i = 0; i < input.Length; i++)
            {
                matrix[i, row] = '*';
                if (row % (key - 1) == 0)
                {
                    down = !down;
                }
                if (down)
                {
                    row++;
                }
                else
                {
                    row--;
                }

            }


            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < matrix.Length/key; j++)
                {
                    if (matrix[j,i] == '*')
                    {
                        matrix[j, i] = input[0];
                        input = input.Substring(1);
                    }
                    Console.Write(matrix[j, i]);                    
                    if(matrix[j, i] == 0)
                    {
                        Console.Write(' ');
                    }
                }
                Console.WriteLine();
            }

            foreach(char c in matrix)
            {
                output += c;
            }

            return output;

        }

        static string encryptBaconian(string input)
        {
            string output = "";
            input = input.ToLower();
            foreach(char c in input)
            {
                if(c >= 'a' && c<= 'z')
                {
                    string binary = Convert.ToString(c - 97, 2);
                    while(binary.Length < 5)
                    {
                        binary = "0" + binary;
                    }
                    foreach (char c2 in binary)
                    {                        
                        output += (char)(int.Parse(c2.ToString()) + 97);
                    }
                    output += " ";
                }
                
            }
            return output.ToUpper();
        }

        static string decryptBaconian(string input)
        {            
            string output = "";
            string[] letters = input.ToLower().Split(' ');

            for (int i = 0; i < letters.Length; i++)
            {
                string binary = "";
                foreach(char c in letters[i])
                {
                    binary += (c - 97);
                }
                
                int denary = Convert.ToInt32(binary, 2);                
                output += (char)(denary + 97);
            }
            return output;
        }

        static string EncryptSubsitution(string input, string key)
        {
            string output = "";
            input = input.ToLower();
            key = RemoveDuplicateLetters(key.ToLower());
            Dictionary<char, char> pairs = new Dictionary<char, char>(); //A -> B
            for (int i = 0; i < key.Length; i++)
            {
                pairs.Add((char)(i + 97), key[i]);
            }
            for (int i = 0; i < 26; i++)
            {
                if(!pairs.ContainsValue((char)(i+97)))
                {

                }
            }
            

            for (int i = 0; i < input.Length; i++)
            {
                output += pairs[input[i]];
            }

            return output;

        }

        static string RemoveDuplicateLetters(string input)
        {
            string output = "";
            foreach(char c in input)
            {
                if (!output.Contains(c))
                {
                    output += c;
                }
            }
            return output;
        }

        static string SubstitutionEncryptDecrypt(string plaintext, Dictionary<char,char> key)
        {
            string output = "";
            foreach (char c in plaintext)
            {
                if(c >= 'a' && c <= 'z')
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

        static void Main(string[] args)
        {
            string plaintext = "hello";
            string key = "anamazingkey";

            Dictionary<char,char> keyPairs = new Dictionary<char,char>();
            Dictionary<char, char> reverseKeyPairs = new Dictionary<char, char>();

            for (int i = 0; i < key.Length; i++)
            {
                reverseKeyPairs.Add(key[i], (char)(i + 97));
                keyPairs.Add((char)(i + 97), key[i]);                 
            }
            for (int i = key.Length; i < 26; i++)
            {
                keyPairs.Add((char)(i+97),  ) 
            }

            while(true)
            {
                Console.Write("Input text: ");
                string input = Console.ReadLine();

                Console.Write("Encrypt or decrypt? E/D  ");
                switch (Console.ReadLine().ToUpper())
                {
                    case "E":
                        Console.WriteLine(EncryptSubsitution(input, "hello"));
                        break;
                    case "D":
                        Console.WriteLine(decryptBaconian(input));
                        break;
                    default:
                        Console.WriteLine("AAAAA NOT AN OPTION!");
                        break;

                }
            }

            
            Console.ReadKey();
        }
    }
}
