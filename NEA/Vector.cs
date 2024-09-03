using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq.Expressions;

namespace NEA
{
    public class Vector
    {
        private double [] vector;

        public Vector(double[] inV)
        {
            vector = inV;
        }

        public double Element(int i)
        {
            return vector[i];
        }

        public double Magnitude()
        {
            double total = 0;
            foreach(int i in vector)
            {
                total += Math.Pow(i, 2);
            }
            return Math.Sqrt(total);
        }

        public int Dimension()
        {
            return vector.Length;
        }
    }
}
