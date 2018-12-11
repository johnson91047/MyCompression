using System.Collections.Generic;
using System.Collections;
using System;

namespace MyCompression
{
    public static class Utility
    {
        public const int ByteSize = 8;

        public static byte[] BoolArrayToByteArrayInversed(bool[] source)
        {
            List<byte> result = new List<byte>();
            int times = source.Length % ByteSize == 0 ? source.Length / ByteSize : source.Length / ByteSize + 1;
            for (int i = 0; i < times ; i++)
            {
                byte temp = 0;
                for (int j = 0; j < ByteSize && i* ByteSize + j < source.Length; j++)
                {

                    if (source[i* ByteSize + j])
                    {
                        temp |= (byte)(1 << (j));
                    }
                }
                result.Add(temp);
            }


            return result.ToArray();
        }

        public static byte[] BoolArrayToByteArray(bool[] source)
        {
            List<byte> result = new List<byte>();
            int times = source.Length % ByteSize == 0 ? source.Length / ByteSize : source.Length / ByteSize + 1;
            for (int i = 0; i < times; i++)
            {
                byte temp = 0;
                for (int j = 0; j < ByteSize && i * ByteSize + j < source.Length; j++)
                {

                    if (source[i * ByteSize + j])
                    {
                        temp |= (byte)(1 << (7 - j));
                    }
                }
                result.Add(temp);
            }


            return result.ToArray();
        }


        public static bool[] ByteToBoolArray(byte source)
        {
            bool[] result = new bool[8];

            for (int i = 0; i < 8; i++)
                result[i] = (source & (1 << i)) == 0 ? false : true;

            Array.Reverse(result);

            return result;
        }

        public static byte BoolArrayToByte(bool[] source)
        {
            byte result = 0;
            int index = 8 - source.Length;

            foreach (bool b in source)
            {
                if (b)
                    result |= (byte)(1 << (7 - index));

                index++;
            }

            return result;
        }

        public static bool[] StringToBoolArray(string source)
        {
            List<bool> result = new List<bool>();

            foreach(char c in source)
            {
                result.Add(c == '1');
            }

            return result.ToArray();
        }

        public static string ByteArrayToString(byte[] source)
        {
            string result = string.Empty;
            foreach (byte b in source)
            {
                string temp = Convert.ToString(b, 2);
                if (temp.Length < ByteSize)
                {
                    temp = temp.PadLeft(ByteSize, '0');
                }

                result += temp;
            }

            return result;
        }


        public static string IntToBinaryString(int number, int numberOfDigit)
        {
            string result = Convert.ToString(number, 2);
            if (result.Length < numberOfDigit)
            {
                result.PadLeft(numberOfDigit, '0');
            }

            return result;
        }
    }
}
