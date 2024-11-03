using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace NEA
{
    public class Matrix
    {
        private int[,] matrix;
        public int n;        

        public Matrix(int[,] inMatrix)
        {
            matrix = inMatrix;
            n = (int)Math.Sqrt(inMatrix.Length);
        }

        public Matrix(string m)
        {
            n = (int)Math.Sqrt(m.Length);
            matrix = new int[n, n];
            for (int i = 0; i < m.Length; i++)
            {
                matrix[i / n, i % n] = m[i]-'A';
            }
        }     
        
        public int element(int a, int b)
        {
            return matrix[a, b];
        }

        public int det()
        {
            int d = 0;
            if (matrix.Length > 1)
            {
                for (int i = 0; i < Math.Sqrt(matrix.Length); i++)
                {
                    d += (int)(matrix[i, 0] * Math.Pow(-1, i) * calculateMinors(i, 0).det());
                }
            }
            else
            {
                d = matrix[0, 0];
            }

            return d;
        }


        private Matrix calculateMinors(int i, int j)
        {
            int n = (int)Math.Sqrt(matrix.Length);
            int[,] minors = new int[n - 1, n - 1];
            int x = 0;

            for (int mi = 0; mi < n; mi++)
            {
                if (mi != i)
                {
                    int y = 0;
                    for (int mj = 0; mj < n; mj++)
                    {
                        if (mj != j)
                        {
                            minors[x, y] = matrix[mi, mj];
                            y++;
                        }
                    }
                    x++;
                }
            }
            return new Matrix(minors);
        }

        public Matrix inverse()
        {
            int n = (int)Math.Sqrt(matrix.Length);
            int[,] inverse = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Matrix minors = calculateMinors(i, j);
                    inverse[j, i] = (int)Math.Pow((-1), i + j) * minors.det(); //aa
                }
            }

            int d = CipherMathsFunctions.modularMultiplicativeInverse(det(), 26);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    inverse[i, j] *= d;
                    inverse[i, j] %= 26;
                    if (inverse[i, j] < 0)
                    {
                        inverse[i, j] += 26;
                    }
                }
            }

            return new Matrix(inverse);
        }
    }
}
