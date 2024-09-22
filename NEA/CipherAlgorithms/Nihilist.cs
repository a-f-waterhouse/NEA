namespace NEA
{
    public static class Nihilist
    {
        public static string Encrypt(string plaintext, char[,] keyGrid, string key)
        {
            string output = "";
            string polybius = Polybius.Encrypt(plaintext, keyGrid);
            string encryptedKey = Polybius.Encrypt(key, keyGrid);
            for (int i = 0; i < polybius.Length-1; i+=2)
            {     
                int c = int.Parse(polybius[i].ToString() + polybius[i+1].ToString()) + int.Parse(encryptedKey[i%key.Length].ToString() + encryptedKey[i%key.Length + 1].ToString());
                output += c + " ";
            }
            return output;
        }

        public static string Decrypt(string ciphertext, char[,] keyGrid, string key)
        {
            ciphertext = ciphertext.Trim();
            int[] splitCiphertext = ciphertext.Split(' ').Select(int.Parse).ToArray();
            string encryptedKey = Polybius.Encrypt(key, keyGrid);
            string polybius = "";
            for (int i = 0; i < splitCiphertext.Length ; i++)
            {
                int c = splitCiphertext[i] - int.Parse(encryptedKey[i*2 % key.Length].ToString() + encryptedKey[i*2 % key.Length + 1].ToString());
                polybius += c + " ";
            }
            return Polybius.Decrypt(polybius, keyGrid);
        }
    }
}
