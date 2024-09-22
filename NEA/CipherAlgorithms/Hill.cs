using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace NEA
{
    public static class Hill
    {
        public static string Encrypt(string plaintext, string key)
        {
            Matrix matrixKey = new Matrix(key.ToUpper());
            string output = "";

            for (int i = 0; i < plaintext.Length; i += matrixKey.n)
            {
                int[] chunk = new int[matrixKey.n];
                for (int j = 0; j < matrixKey.n; j++)
                {
                    chunk[j] = plaintext[i + j] - 'A';
                }

                int[] newChunk = new int[matrixKey.n];

                for (int row = 0; row < matrixKey.n; row++)
                {
                    for (int col = 0; col < matrixKey.n; col++)
                    {
                        newChunk[row] += (matrixKey.element(row, col) * chunk[col]);
                    }
                    output += (char)(newChunk[row] % 26 + 'A');

                }
            }
            return output;

        }

        public static string Decrypt(string ciphertext, string key)
        {
            Matrix matrixKey = new Matrix(key.ToUpper());
            string output = "";

            matrixKey = matrixKey.inverse();

            for (int i = 0; i < ciphertext.Length; i += matrixKey.n)
            {
                int[] chunk = new int[matrixKey.n];
                for (int j = 0; j < matrixKey.n; j++)
                {
                    chunk[j] = ciphertext[i + j] - 'A';
                }

                int[] newChunk = new int[matrixKey.n];

                for (int row = 0; row < matrixKey.n; row++)
                {
                    for (int col = 0; col < matrixKey.n; col++)
                    {
                        newChunk[row] += (matrixKey.element(row, col) * chunk[col]);
                    }
                    output += (char)(newChunk[row] % 26 + 'A');

                }
            }
            return output;

        }

    }
}
