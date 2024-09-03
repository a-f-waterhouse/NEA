using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace NEA
{
    public static class Railfence
    {
        public static string Encrypt(string input, int key)
        {
            string output = "";
            char[,] matrix = fillMatrix(input, key);
            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < input.Length; j++)
                {
                    if (matrix[j,i] != 0)
                    {
                        output += (matrix[j, i]);
                    }
                }
            }
            return output;
        }

        public static string Decrypt(string input, int key)
        {
            char[,] matrix = fillMatrix((string)input, key);

            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < matrix.Length / key; j++)
                {
                    if (matrix[j, i] != 0)
                    {
                        matrix[j, i] = input[0];
                        input = input.Substring(1);
                    }
                }                
            }
            string output = "";
            foreach(char c in matrix)
            {
                if(c!= 0)
                {
                    output += c;
                }
            }
            return output;
        }

        static char[,] fillMatrix(string input, int key)
        {
            char[,] matrix = new char[input.Length, key];
            int row = 0;
            bool down = false;
            for (int i = 0; i < input.Length; i++)
            {
                matrix[i, row] = input[i];
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
            return matrix;
        }
    }
}
