namespace NEA
{
    public class VectorMathsFunctions
    {
        public static int DotProduct(Vector a, Vector b)
        {
            int total = 0;
            for (int i = 0; i < a.Dimension(); i++)
            {
                total += a.Element(i) * b.Element(i);
            }
            return total;
        }

        public static double angle(Vector a, Vector b)
        {
            return DotProduct(a, b) / (a.Magnitude() * b.Magnitude());
        }

    }
}
