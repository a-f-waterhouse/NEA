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

        public static int KeyLength(string plaintext)
        {
            plaintext = OnlyLetters(plaintext);
            double[] ioc = new double[25];
            for (int i = 1; i < 26; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    string chunk = "";
                    for (int k = j; k < plaintext.Length; k += i)
                    {
                        chunk += plaintext[k];
                    }
                    if(chunk.Length > 1)
                    {
                        ioc[i-1] += CryptanalysisFunctions.IndexOfCoincidence(CryptanalysisFunctions.CalculateLetterFrequencies(chunk));
                    }                    
                    
                }
                ioc[i - 1] /= i;
                           
            }

            for (int i = 0; i < 25; i++)
            {
                if (ioc[i] > 0.06)
                {
                    return i+1;
                }
            }

            return 0;
        }        


        static string Climbing(string ciphertext, int iterations, int limit)
        {
            Dictionary<double, string> decryptions = new Dictionary<double, string>();

            for (int i = 0; i < iterations; i++)
            {
                string key = randomKey();
                int count = 0;                

                Dictionary<char, char> encryptionMap = new Dictionary<char, char>();
                Dictionary<char, char> decryptionMap = new Dictionary<char, char>();
                Substitution.getKey(key, ref encryptionMap, ref decryptionMap);

                string d = Substitution.MapCharacters(ciphertext, encryptionMap);
                double fCurrent = CryptanalysisFunctions.BigramFitness(d), f2;

                while (count < limit)
                {
                    char[] newKey = key.ToCharArray();
                    int index1 = rnd.Next(0, 26), index2 = rnd.Next(0, 26);

                    char temp = newKey[index1];
                    newKey[index1] = newKey[index2];
                    newKey[index2] = temp;

                    Substitution.getKey(new string(newKey), ref encryptionMap, ref decryptionMap);
                    string decryption2 = Substitution.MapCharacters(ciphertext, encryptionMap);

                    f2 = CryptanalysisFunctions.BigramFitness(decryption2);
                    if (fCurrent < f2)
                    {
                        key = new string(newKey);
                        count = 0;
                        d = decryption2;
                        fCurrent = f2;
                    }                  

                    count++;
                }
                if(!decryptions.ContainsKey(fCurrent))
                {
                    decryptions.Add(fCurrent, d);
                }
            }
            return decryptions[decryptions.Keys.Max()];
        }

        static void Climbing(string ciphertext, char method, string[] keys)
        {
            int count = 0;
            double x = 0;
            string d = "";
            Dictionary<string, double> bestKeys = new Dictionary<string, double>();

            for (int i = 0; i < 10; i++)
            {
                string key = keys[i];
                Console.WriteLine(i);
                count = 0;

                while (count < 500)
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

        static string OnlyLetters(string input)
        {
            string output = "";
            foreach (char c in input)
            {
                if (CipherMathsFunctions.isLetter(c))
                {
                    output += c;
                }
            }
            return output;
        }


        /*static string VigenereDecrypt(int period, string ciphertext)
        {
            for (int j = 0; j < period; j++)
            {
                string chunk = "";
                for (int k = j; k < ciphertext.Length; k += i)
                {
                    chunk += ciphertext[k];
                }
                if (chunk.Length > 1)
                {
                    ioc[i - 1] += CryptanalysisFunctions.IndexOfCoincidence(CryptanalysisFunctions.CalculateLetterFrequencies(chunk));
                }

            }
        }*/


        static void Main(string[] args)
        {
            string text = "YBY JMTCJYAC YLB AFYPJCQ ZYZZYEC YPC RUM MD RFC KMQR GKNMPRYLR KCKZCPQ MD MSP AMKKSLGRW. RFCW DCYRSPCB GL RFC DGPQR LYRGMLYJ AGNFCP AFYJJCLEC, YLB YEYGL QMKC WCYPQ JYRCP UFCL UC CVNJMPCB RFCGP CYPJW KCCRGLE. GL RFGQ YBTCLRSPC UC YPC PCRSPLGLE RM RFC CLB MD FCP JGDC, YR JCYQR RFC NYPR PCAMPBCB GL RFC YPAFGTCQ YR ZMQQ FCYBOSYPRCPQ. G DMSLB RFGQ QRMPW ZW YAAGBCLR UFCL G UYQ PCQCYPAFGLE RFC PMJC MD RFC PMWYJ DYKGJW GL MSP LYRGMLYJ QCASPGRW. KYLW MD RFCK QCPTCB GL RFC KGJGRYPW YLB YPC DYKGJGYP UGRF AMBCQ YLB AGNFCPQ, YLB G UMLBCPCB FMU DYP ZYAI RFYR GLRCPCQR UCLR. RFYR UYQ FMU G QRSKZJCB YAPMQQ Y LMRC DPMK NPGLAC YJZCPR RM JMPB NYJKCPQRML YLB RFYR QRYPRCB KC BMUL RFC PYZZGR FMJC RFYR JCB SQ FCPC. G FMNC WMS CLHMW GR YQ KSAF YQ G BGB. UC UGJJ KCCR QMKC DYQAGLYRGLE LCU AFYPYARCPQ ML RFC UYW, YLB RFC QRMPW RYICQ Y RUGQR MP RUM. MD AMSPQC, G ILMU FMU GR CLBQ, WMS BML\'R WCR, ZSR GD WMS QRGAI UGRF GR WMS UGJJ, YLB, JGIC KC, WMS UGJJ NPMZYZJW JCYPL QMKCRFGLE ML RFC UYW. G FYTC NMQRCB Y QICRAF MD Y ZSJJCR RFYR UYQ QCLR RM YBY ZYAI GL 1851 ML RFC AYQC DGJCQ NYEC. RFYR UYQ UFCPC RFC QRMPW ZCEYL YLB UFYR BPCU YBY GL. RYIC Y JMMI YLB QCC GD WMS AYL DGESPC MSR UFYR GR KGEFR FYTC KCYLR. GR GQ LMR Y ZYB NJYAC RM QRYPR.";
            string test = "AHKLE XRMHP XKLXT LMAHK LEXRL NKKXR XGZET GWFRW XTKFK LEHOX ETVXB PTLNG LNKXA HPMHT WWKXL LRHNL HBAHI XMATM MABLE XMMXK YBGWL RHNTG WYBGW LRHNP XEEBT FPKBM BGZMA BLEXM MXKUX VTNLX BWHGH MDGHP PAHXE LXMHM NKGMH TGWBY BZNKX MATMR HNFBZ AMUXT UEXMH AXEIB TFLHK KRYHK MAXMK HNUEX BFTRA TOXVT NLXWU RVRIA XKBGZ MABLE XMMXK UNMBP TLGHM LNKXM ATMRH NPHNE WKXTW TFXLL TZXLX GMBGF RATGW GHMDG HPBGZ GHMAB GZTUH NMFXB MLXXF XWFHK XEBDX ERRHN PHNEW FTDXM AXXYY HKMBY MAXFX LLTZX PTLAT KWXKM HKXTW BYGHM TGWBT FPKHG ZTUHN MRHNK BGMXK XLMLB GVHWX LTGWV RIAXK LMAXG IXKAT ILBMP HGMFT MMXKP AXMAX KRHNK XTWBM HKGHM BTFTW XMXVM BOXBG MAXLF TEEMH PGHYX KBGGX PRHKD TGWBG TKXVX GMBGO XLMBZ TMBHG BVTFX NIHGT LMKTG ZXGXL LMATM BVTGG HMNGI BVDBP TLTLD XWMHB GOXLM BZTMX TUKXT DBGTM TEHVT EPTKX AHNLX MAXAT LIATW UXXGK XFHOX WTGWV ENFLB ERKXI ETVXW TEXKM BGZMA XHPGX KUNMG HMABG ZLXXF XWMHU XFBLL BGZTG WMAXL ABIIB GZVKT MXLKX FTBGX WLXTE XWUXA BGWMA XFBYH NGWTU NEEXM VTKKR BGZTG NGNLN TEBGL VKBIM BHGTG WATOX BGVEN WXWTL DXMVA BMBLF BZAMR NGNLN TEYHK TVTLX MHUXF TKDXW BGMAB LPTRT GWMAX EXMMX KLFTD XGHLX GLXMH FXBTF AHIBG ZMATM RHNPB EEUXT UEXMH AXEIB GMXKI KXMMA XFPBM AENVD MATMF BZAML AXWLH FXEBZ AMHGF RBGOX LMBZT MBHGZ BOXGM AXUNE EXMPT LYHNG WPBMA TLABI FXGMU HNGWY HKXGZ ETGWM AXKXF TRTEL HUXKX TLHGY HKRHN MHUXP HKKBX WUNMB FHLML BGVXK XERAH IXGHM BYRHN ATOXK XTWMA BLYTK MAXGI EXTLX NGWXK LMTGW AHPFN VABTI IKXVB TMXRH NKTLL BLMTG VXBEH HDYHK PTKWP BMALH FXXTZ XKGXL LMHRH NKKXI ERPBM AFRZK TMBMN WXFBL LDTMX PTKGX ";


            
            //Console.WriteLine(KeyLength(test));

            Console.WriteLine(Climbing(OnlyLetters(text), 5, 1000));

            Console.WriteLine("done");


            Console.ReadLine();


        }
    }
}
