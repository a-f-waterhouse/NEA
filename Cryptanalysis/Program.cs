using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptanalysis
{
    internal class Program
    {
        static double[] expectedMonogramFrequencies = { };
        static double indexOfCoincidence(string C) //index of coincidence in English = 0.0667, random = 0.0385, closer to random it is, the more alphabets used.
        {
            int[] frequencies = new int[26];
            foreach (char c in C)
            {
                if (c - 97 < 26 && c - 97 >= 0)
                {
                    frequencies[c - 97]++;
                }
            }
            double a = 0;
            foreach (int f in frequencies)
            {                
                a += f * (f - 1);
                Console.WriteLine(a);
            }
            Console.WriteLine((C.Length * (C.Length - 1)));
            return a / (C.Length * (C.Length - 1));
        }

        static double X2Stat(int[] C, int length)
        {
            double X = 0;
            double[] expected = expectedMonogramFrequencies;

            for (int i = 0; i < expected.Length; i++)
            {
                expected[i] = length * (expected[i] / 100);
                X += Math.Pow(C[i] - expected[i], 2) / expected[i];
            }
            return X;
        } //measurement of how closely a data set matches expected values, smaller is better


        public static double ShanonEntropy(int[] Ciphertext)
        {
            double entropy = 0;
            int length = 0;
            for (int i = 0; i < Ciphertext.Length; i++)
            {
                length += Ciphertext[i];
            }

            foreach (int c in Ciphertext)
            {
                double frequency = (double)c / length;
                if(frequency!= 0)
                {
                    entropy -= (frequency * Math.Log(frequency, 2));
                }                
                Console.WriteLine(entropy + " " + frequency);
            }

            return entropy;
        }

        static void Main(string[] args)
        {
            string C = "abcdasdfghjkl;fdnksv :) hhhh";
            int[] frequencies = new int[26];
            foreach (char c in C)
            {
                if (c - 97 < 26 && c - 97 >= 0)
                {
                    frequencies[c - 97]++;
                }
            }
            Console.WriteLine(ShanonEntropy(frequencies));
            Console.ReadLine();

        }
    }
}
