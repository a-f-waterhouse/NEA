namespace NEA
{
    public static class Baconian
    {
        public static string Encrypt(string input)
        {
            string output = "";
            input = input.ToLower();
            foreach (char c in input)
            {
                if (c >= 'a' && c <= 'z')
                {
                    string binary = Convert.ToString(c - 97, 2);
                    while (binary.Length < 5)
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

        public static string Decrypt(string input)
        {
            string output = "";
            string[] letters = input.ToLower().Split(' ');

            for (int i = 0; i < letters.Length; i++)
            {
                string binary = "";
                foreach (char c in letters[i])
                {
                    binary += (c - 97);
                }

                int denary = Convert.ToInt32(binary, 2);
                output += (char)(denary + 97);
            }
            return output;
        }
    }
}
