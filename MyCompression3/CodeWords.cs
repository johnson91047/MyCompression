﻿
using System;
using System.Collections.Generic;

namespace MyCompression
{
    public static class CodeWords
    {
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

        private static readonly string[,] acCategory = new [,]
        {
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

        private static readonly string[] dcCategory = new[]
        {
            "00","010","011","100","101","110","1110","11110","111110","1111110","11111110","111111110"
        };

        private static readonly Range[,] diffSSSS = new Range[,]
        {
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
            {new Range(-2047,-1024), new Range(1024,2047) },
        };

        public static IEnumerable<bool> ConstructCodeWord(List<RLEBlock> blocks)
        {
            string codeword = string.Empty;
            foreach(RLEBlock block in blocks)
            {
                DCElement dc = block.DC;
                codeword += GetDCCodeWord(dc.Diff);
                foreach (ACElement ac in block.ACs)
                {
                    codeword += GetACCodeWord(ac.Length, ac.Value);
                }
                if(block.NeedEOB)
                {
                    codeword += "1010";
                }
            }

            return Utility.StringToBoolArray(codeword);
        }

        private static string GetACCodeWord(int run , int size)
        {
            string codeword = string.Empty;
            codeword += acCategory[run, GetDiffCategory(size)];
            codeword += GetDiffCode(size);
            return codeword;
        }

        private static string GetDCCodeWord(int diff)
        {
            string codeword = string.Empty;
            codeword += dcCategory[GetDiffCategory(diff)];
            codeword += GetDiffCode(diff);

            return codeword;
        }

        private static int GetDiffCategory(int value)
        {
            string codeword = string.Empty;
            int length = diffSSSS.GetLength(0);
            for (int i = 0; i < length; i++)
            {
                if (diffSSSS[i, 0].IsInRange(value) || diffSSSS[i, 1].IsInRange(value))
                {
                    return i;
                }
            }

            throw new Exception("invalid category");
        }

        private static string GetDiffCode(int diff)
        {
            string codeword = string.Empty;
            int length = diffSSSS.GetLength(0);
            for (int i = 0; i < length; i++)
            {
                if (diffSSSS[i, 0].IsInRange(diff))
                {
                    int numdiff = diff - diffSSSS[i, 0].Min;
                    codeword += Utility.IntToBinaryString(numdiff, i);
                    break;
                }

                if (diffSSSS[i, 1].IsInRange(diff))
                {
                    int numdiff = diffSSSS[i, 1].Max - diff;
                    codeword += Utility.IntToBinaryString(numdiff, i);
                    break;
                }
            }
            return codeword;
        }
    }
}
