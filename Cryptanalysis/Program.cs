using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptanalysis
{
    internal class Program
    {
        static double indexOfCoincidence(string C) //index of coincidence in English = 0.0667, random = 0.0385, closer to random it is, the more alphabets used.
        {
            int[] frequencies = new int[26];
            foreach (char c in C)
            {                 
                frequencies[c - 97]++;                
            }
            int a = 0;
            foreach (int f in frequencies)
            {
                int temp = f * (f - 1);
                a += temp;
            }
            double b = C.Length * (C.Length - 1);
            double index = a / b;
            return (index);
        }

        static void Main(string[] args)
        {


        }
    }
}
