using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace MyCompression
{
    public static class Utility
    {
        public const int ByteSize = 8;

        public static byte[] BoolArrayToByteArray(bool[] source)
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

        public static byte[] BitArrayToByteArray(BitArray bitArray)
        {
            byte[] result = new byte[(bitArray.Length - 1) / ByteSize + 1];
            bitArray.CopyTo(result, 0);
            return result;
        }
    }
}
