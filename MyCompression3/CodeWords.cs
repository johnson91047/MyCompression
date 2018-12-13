using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MyCompression
{
    public static class CodeWords
    {
        static CodeWords()
        {
            // init ac hashtable
            for (int i = 0; i < AcCategory.GetLength(0); i++)
            {
                for (int j = 0; j < AcCategory.GetLength(1); j++)
                {
                    if(string.IsNullOrEmpty(AcCategory[i, j])) continue;
                    AcCategoryHashTable[AcCategory[i,j]] = new Tuple<int,int>(i,j);
                }
            }

            // init dc hashtable
            for (int i = 0; i < DcCategory.Length; i++)
            {
                DcCategoryHashTable[DcCategory[i]] = i;
            }
        }

        public class Range
        {
            public Range(int min, int max)
            {
                Min = min;
                Max = max;
            }

            public bool IsInRange(int number)
            {
                return number <= Max && number >= Min;
            }

            public readonly int Max;
            public readonly int Min;
        }

        public static readonly string[,] AcCategory = {
            {"1010", "00", "01", "100", "1011", "11010", "1111000", "11111000", "1111110110", "1111111110000010", "1111111110000011" },
            {"", "1100", "11011", "1111001", "111110110", "11111110110", "1111111110000100", "1111111110000101", "1111111110000110", "1111111110000111", "1111111110001000"}, 
            {"", "11100", "11111001", "1111110111", "111111110100", "1111111110001001", "1111111110001010", "1111111110001011", "1111111110001100", "1111111110001101", "1111111110001110"},
            {"", "111010", "111110111", "111111110101", "1111111110001111", "1111111110010000", "1111111110010001", "1111111110010010", "1111111110010011", "1111111110010100", "1111111110010101"},
            {"", "111011", "1111111000", "1111111110010110", "1111111110010111", "1111111110011000", "1111111110011001", "1111111110011010", "1111111110011011", "1111111110011100", "1111111110011101"},
            {"", "1111010", "11111110111", "1111111110011110", "1111111110011111", "1111111110100000", "1111111110100001", "1111111110100010", "1111111110100011", "1111111110100100", "1111111110100101"},
            {"", "1111011", "111111110110", "1111111110100110", "1111111110100111", "1111111110101000", "1111111110101001", "1111111110101010", "1111111110101011", "1111111110101100", "1111111110101101"},
            {"", "11111010", "111111110111", "1111111110101110", "1111111110101111", "1111111110110000", "1111111110110001", "1111111110110010", "1111111110110011", "1111111110110100", "1111111110110101"},
            {"", "111111000", "111111111000000", "1111111110110110", "1111111110110111", "1111111110111000", "1111111110111001", "1111111110111010", "1111111110111011", "1111111110111100", "1111111110111101"},
            {"", "111111001", "1111111110111110", "1111111110111111", "1111111111000000", "1111111111000001", "1111111111000010", "1111111111000011", "1111111111000100", "1111111111000101", "1111111111000110"},
            {"", "111111010", "1111111111000111", "1111111111001000", "1111111111001001", "1111111111001010", "1111111111001011", "1111111111001100", "1111111111001101", "1111111111001110", "1111111111001111"},
            {"", "1111111001", "1111111111010000", "1111111111010001", "1111111111010010", "1111111111010011", "1111111111010100", "1111111111010101", "1111111111010110", "1111111111010111", "1111111111011000"},
            {"", "1111111010", "1111111111011001", "1111111111011010", "1111111111011011", "1111111111011100", "1111111111011101", "1111111111011110", "1111111111011111", "1111111111100000", "1111111111100001"},
            {"", "11111111000", "1111111111100010", "1111111111100011", "1111111111100100", "1111111111100101", "1111111111100110", "11111111111001111", "1111111111101000", "1111111111101001", "1111111111101010"},
            {"", "1111111111101011", "1111111111101100", "1111111111101101", "1111111111101110", "1111111111101111", "1111111111110000", "1111111111110001", "1111111111110010", "1111111111110011", "1111111111110100"},
            {"11111111001", "1111111111110101", "1111111111110110", "1111111111110111", "1111111111111000", "1111111111111001", "1111111111111010", "1111111111111011", "1111111111111100", "1111111111111101", "1111111111111110" }
        };

        public static readonly Hashtable AcCategoryHashTable = new Hashtable();

        public static readonly string[] DcCategory= {
            "00",
            "010",
            "011",
            "100",
            "101",
            "110",
            "1110",
            "11110",
            "111110",
            "1111110",
            "11111110",
            "111111110",
        };

        public static readonly Hashtable DcCategoryHashTable = new Hashtable();

        public static readonly Range[,] DiffSSSS = {
            {new Range(0,0), new Range(0,0) },
            {new Range(-1,-1), new Range(1,1) },
            {new Range(-3,-2), new Range(2,3) },
            {new Range(-7,-4), new Range(4,7) },
            {new Range(-15,-8), new Range(8,15) },
            {new Range(-31,-16), new Range(16,31) },
            {new Range(-63,-32), new Range(32,63) },
            {new Range(-127,-64), new Range(64,127) },
            {new Range(-255,-128), new Range(128,255) },
            {new Range(-511,-256), new Range(256,511) },
            {new Range(-1023,-512), new Range(512,1023) },
            {new Range(-2047,-1024), new Range(1024,2047) }
        };

        /// <summary>
        /// Construct RLE from code words
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<RLEBlock> ReconstructRleBlocks (string source)
        {
            List<RLEBlock> result = new List<RLEBlock>();
            RLEBlock block = null;
            string temp = string.Empty;
            bool isDc = true;
            int expectBlockCount = Convert.ToInt32(Math.Pow(JPEGAlgorithm.ImageSize, 2) / Math.Pow(JPEGAlgorithm.BlockSize, 2));
            for (int i = 0; i < source.Length; i++)
            {
                if(result.Count == expectBlockCount)
                {
                    break;
                }

                // dc decode first
                if (isDc)
                {
                    if(block == null)
                    {
                        block = new RLEBlock();
                    }
                    temp += source[i];
                    string diff = string.Empty;
                    if (DcCategoryHashTable.Contains(temp))
                    {
                        int category = Convert.ToInt32(DcCategoryHashTable[temp]);
                        int nextBits = category == 0 ? 1 : category;
                        for (int j = 1; j <= nextBits; j++)
                        {
                            diff += source[i + j];
                        }

                        i += nextBits;

                        DCElement dc;
                        if (category == 0 && diff == "0")
                        {
                            dc = new DCElement(0);
                        }
                        else
                        {
                            dc = new DCElement(FindDiffValue(category,diff));
                        }

                        block.DC = dc;
                        isDc = false;
                        temp = string.Empty;
                    }
                }
                // then ac
                else
                {
                    temp += source[i];
                    if (AcCategoryHashTable.Contains(temp))
                    {
                        string value = string.Empty;
                        Tuple<int, int> runSize = (Tuple<int, int>) AcCategoryHashTable[temp];

                        if (runSize.Item1 == 0 && runSize.Item2 == 0)
                        {
                            isDc = true;
                            result.Add(block);
                            temp = string.Empty;
                            block = null;
                            continue;
                        }

                        int category = runSize.Item2;
                        int nextBits = category == 0 ? 1 : category;
                        for (int j = 1; j <= nextBits; j++)
                        {
                            value += source[i + j];
                        }

                        i += nextBits;

                        ACElement ac = new ACElement(runSize.Item1, FindDiffValue(runSize.Item2, value));
                        block.ACs.Add(ac);
                        temp = string.Empty;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Construct code word from RLE
        /// </summary>
        /// <param name="blocks"></param>
        /// <returns></returns>
        public static bool[] ConstructCodeWord(List<RLEBlock> blocks)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (RLEBlock block in blocks)
            {
                
                DCElement dc = block.DC;
                sb.Append(GetDCCodeWord(dc.Diff));
                foreach (ACElement ac in block.ACs)
                {
                    sb.Append(GetACCodeWord(ac.Length, ac.Value));
                }
                sb.Append("1010");
                i++;
            }

            return Utility.StringToBoolArray(sb.ToString());
        }

        /// <summary>
        /// Construct code word for ac
        /// </summary>
        /// <param name="run"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private static string GetACCodeWord(int run , int size)
        {
            string codeword = string.Empty;
            int category = GetDiffCategory(size);
            codeword += AcCategory[run, category];
            codeword += GetDiffCode(size, category);
            return codeword;
        }

        /// <summary>
        /// Construct code word for dc
        /// </summary>
        /// <param name="diff"></param>
        /// <returns></returns>
        private static string GetDCCodeWord(int diff)
        {
            string codeword = string.Empty;
            int category = GetDiffCategory(diff);
            codeword += DcCategory[category];
            codeword += GetDiffCode(diff, category);
            return codeword;
        }

        /// <summary>
        /// Get category by the number of bits
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static int GetDiffCategory(int value)
        {
            return value == 0 ? 0 : Convert.ToString(Math.Abs(value), 2).Length;
        }

        /// <summary>
        /// Get diff value code word
        /// </summary>
        /// <param name="diff"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        private static string GetDiffCode(int diff, int category)
        {
            string codeword = string.Empty;
            int length = DiffSSSS.GetLength(0);
            if (DiffSSSS[category, 0].IsInRange(diff))
            {
                int numdiff = diff - DiffSSSS[category, 0].Min;
                codeword += Utility.IntToBinaryString(numdiff, category);
                return codeword;
            }

            if (DiffSSSS[category, 1].IsInRange(diff))
            {
                codeword += Utility.IntToBinaryString(diff, category);
                return codeword;
            }

            throw new Exception("wrong category " + diff + " " + category);
        }

        /// <summary>
        /// Reverse diff value from bits
        /// </summary>
        /// <param name="category"></param>
        /// <param name="binaryString"></param>
        /// <returns></returns>
        private static int FindDiffValue(int category, string binaryString)
        {
            int value = Convert.ToInt32(binaryString, 2);
            if (!DiffSSSS[category, 1].IsInRange(value))
            {
                // this means diff value is a negative number
                int baseNumber = (int)(0 - Math.Pow(2,category)) + 1;
                return baseNumber + value;
            }

            // other wise return normal value
            return value;
        }
        
    }
}
