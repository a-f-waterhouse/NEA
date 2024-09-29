using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hill_Climbing
{
    internal class Program
    {
        static Random rnd = new Random();
        static string randomKey()
        {            
            string key = "";
            while (key.Length < 26)
            {
                int c = rnd.Next(0, 26) + 97;
                if (!key.Contains((char)c))
                {
                    key += (char)c;
                }
            }
            return key;
        }


        static string[] Climbing(string ciphertext, char method)
        {
            int count = 0;            
            string key = randomKey();
            double x = 0;
            string d = "";
            //Dictionary<string, double> bestKeys = new Dictionary<string, double>();
            string[] bestKeys = new string[5];

            for (int i = 0; i < 5; i++)
            {
                key = randomKey();
                Console.WriteLine(i);
                count = 0;
                while (count < 1000)
                {
                    Dictionary<char, char> encryptionMap = new Dictionary<char, char>();
                    Dictionary<char, char> decryptionMap = new Dictionary<char, char>();
                    Substitution.getKey(key, ref encryptionMap, ref decryptionMap);
                    string decryption1 = Substitution.MapCharacters(ciphertext, encryptionMap);

                    char[] newKey = key.ToCharArray();
                    int index1 = rnd.Next(0, 26), index2 = rnd.Next(0, 26);

                    char temp = newKey[index1];
                    newKey[index1] = newKey[index2];
                    newKey[index2] = temp;

                    Substitution.getKey(new string(newKey), ref encryptionMap, ref decryptionMap);
                    string decryption2 = Substitution.MapCharacters(ciphertext, encryptionMap);

                    if(method == 'B')
                    {
                        if (CryptanalysisFunctions.BigramFitness(decryption2) > CryptanalysisFunctions.BigramFitness(decryption1))
                        //if (CryptanalysisFunctions.X2Stat(CryptanalysisFunctions.CalculateLetterFrequencies(decryption1)) > CryptanalysisFunctions.X2Stat(CryptanalysisFunctions.CalculateLetterFrequencies(decryption2)))
                        {
                            key = new string(newKey);
                            count = 0;
                            x = CryptanalysisFunctions.BigramFitness(decryption2);
                            d = decryption2;

                        }
                    }
                    else if(method == 'C')
                    {                        
                        if (CryptanalysisFunctions.X2Stat(CryptanalysisFunctions.CalculateLetterFrequencies(decryption1)) > CryptanalysisFunctions.X2Stat(CryptanalysisFunctions.CalculateLetterFrequencies(decryption2)))
                        {
                            key = new string(newKey);
                            count = 0;
                            x = CryptanalysisFunctions.X2Stat(CryptanalysisFunctions.CalculateLetterFrequencies(decryption2));
                            d = decryption2;

                        }
                    }

                    count++;
                }
                bestKeys[i] = key;
                Console.WriteLine(key);
            }
            return bestKeys;
        }

        static void Climbing(string ciphertext, char method, string[] keys)
        {
            int count = 0;
            double x = 0;
            string d = "";
            Dictionary<string, double> bestKeys = new Dictionary<string, double>();

            for (int i = 0; i < 3; i++)
            {
                string key = keys[i];
                Console.WriteLine(i);
                count = 0;
                while (count < 1000)
                {
                    Dictionary<char, char> encryptionMap = new Dictionary<char, char>();
                    Dictionary<char, char> decryptionMap = new Dictionary<char, char>();
                    Substitution.getKey(key, ref encryptionMap, ref decryptionMap);
                    string decryption1 = Substitution.MapCharacters(ciphertext, encryptionMap);

                    char[] newKey = key.ToCharArray();
                    int index1 = rnd.Next(0, 26), index2 = rnd.Next(0, 26);

                    char temp = newKey[index1];
                    newKey[index1] = newKey[index2];
                    newKey[index2] = temp;

                    Substitution.getKey(new string(newKey), ref encryptionMap, ref decryptionMap);
                    string decryption2 = Substitution.MapCharacters(ciphertext, encryptionMap);

                    if (method == 'B')
                    {
                        if (CryptanalysisFunctions.BigramFitness(decryption2) > CryptanalysisFunctions.BigramFitness(decryption1))
                        //if (CryptanalysisFunctions.X2Stat(CryptanalysisFunctions.CalculateLetterFrequencies(decryption1)) > CryptanalysisFunctions.X2Stat(CryptanalysisFunctions.CalculateLetterFrequencies(decryption2)))
                        {
                            key = new string(newKey);
                            count = 0;
                            x = CryptanalysisFunctions.BigramFitness(decryption2);
                            d = decryption2;

                        }
                    }
                    else if (method == 'C')
                    {
                        if (CryptanalysisFunctions.X2Stat(CryptanalysisFunctions.CalculateLetterFrequencies(decryption1)) > CryptanalysisFunctions.X2Stat(CryptanalysisFunctions.CalculateLetterFrequencies(decryption2)))
                        {
                            key = new string(newKey);
                            count = 0;
                            x = CryptanalysisFunctions.X2Stat(CryptanalysisFunctions.CalculateLetterFrequencies(decryption2));
                            d = decryption2;

                        }
                    }

                    count++;
                }
                if (!bestKeys.ContainsKey(d))
                {
                    bestKeys.Add(d, x);
                    Console.WriteLine(d);
                }
            }

        }


        static void Main(string[] args)
        {
            string text = "YBY JMTCJYAC YLB AFYPJCQ ZYZZYEC YPC RUM MD RFC KMQR GKNMPRYLR KCKZCPQ MD MSP AMKKSLGRW. RFCW DCYRSPCB GL RFC DGPQR LYRGMLYJ AGNFCP AFYJJCLEC, YLB YEYGL QMKC WCYPQ JYRCP UFCL UC CVNJMPCB RFCGP CYPJW KCCRGLE. GL RFGQ YBTCLRSPC UC YPC PCRSPLGLE RM RFC CLB MD FCP JGDC, YR JCYQR RFC NYPR PCAMPBCB GL RFC YPAFGTCQ YR ZMQQ FCYBOSYPRCPQ. G DMSLB RFGQ QRMPW ZW YAAGBCLR UFCL G UYQ PCQCYPAFGLE RFC PMJC MD RFC PMWYJ DYKGJW GL MSP LYRGMLYJ QCASPGRW. KYLW MD RFCK QCPTCB GL RFC KGJGRYPW YLB YPC DYKGJGYP UGRF AMBCQ YLB AGNFCPQ, YLB G UMLBCPCB FMU DYP ZYAI RFYR GLRCPCQR UCLR. RFYR UYQ FMU G QRSKZJCB YAPMQQ Y LMRC DPMK NPGLAC YJZCPR RM JMPB NYJKCPQRML YLB RFYR QRYPRCB KC BMUL RFC PYZZGR FMJC RFYR JCB SQ FCPC. G FMNC WMS CLHMW GR YQ KSAF YQ G BGB. UC UGJJ KCCR QMKC DYQAGLYRGLE LCU AFYPYARCPQ ML RFC UYW, YLB RFC QRMPW RYICQ Y RUGQR MP RUM. MD AMSPQC, G ILMU FMU GR CLBQ, WMS BML\'R WCR, ZSR GD WMS QRGAI UGRF GR WMS UGJJ, YLB, JGIC KC, WMS UGJJ NPMZYZJW JCYPL QMKCRFGLE ML RFC UYW. G FYTC NMQRCB Y QICRAF MD Y ZSJJCR RFYR UYQ QCLR RM YBY ZYAI GL 1851 ML RFC AYQC DGJCQ NYEC. RFYR UYQ UFCPC RFC QRMPW ZCEYL YLB UFYR BPCU YBY GL. RYIC Y JMMI YLB QCC GD WMS AYL DGESPC MSR UFYR GR KGEFR FYTC KCYLR. GR GQ LMR Y ZYB NJYAC RM QRYPR.";            
            Climbing(text, 'B', Climbing(text, 'C'));
            Console.WriteLine("done");

            Console.ReadLine();


        }
    }
}
