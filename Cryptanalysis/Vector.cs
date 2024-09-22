using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Linq.Expressions;

namespace Cryptanalysis
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
