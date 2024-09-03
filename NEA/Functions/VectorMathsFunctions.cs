namespace NEA
{
    public class VectorMathsFunctions
    {
        public static double DotProduct(Vector a, Vector b)
        {
            double total = 0;
            for (int i = 0; i < a.Dimension(); i++)
            {
                total += a.Element(i) * b.Element(i);
            }
            return total;
        }

        public static double angle(Vector a, Vector b)
        {
            return (DotProduct(a, b) / Math.Sqrt(a.Magnitude() * b.Magnitude()));
        }

    }
}
