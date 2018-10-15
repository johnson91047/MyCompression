using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace MyCompression3
{
    public static class Utility
    {
        public static byte BoolArrayToByte(bool[] source)
        {
            byte result = 0;
            int index = 0;

            foreach (bool b in source)
            {
                if (b)
                    result |= (byte)(1 << (7 - index));

                index++;
            }

            return result;
        }

        public static byte[] BitArrayToByteArray(BitArray bitArray)
        {
            byte[] result = new byte[(bitArray.Length - 1) / 8 + 1];
            bitArray.CopyTo(result, 0);
            return result;
        }
    }
}
