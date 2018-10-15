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
        public static byte[] BoolArrayToByteArray(bool[] source)
        {
            List<byte> result = new List<byte>();
            int times = source.Length % 8 == 0 ? source.Length / 8 : source.Length / 8 + 1;
            for (int i = 0; i < times ; i++)
            {
                byte temp = 0;
                for (int j = 0; j < 8 && i*8+j < source.Length; j++)
                {

                    if (source[i*8+j])
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
            byte[] result = new byte[(bitArray.Length - 1) / 8 + 1];
            bitArray.CopyTo(result, 0);
            return result;
        }
    }
}
