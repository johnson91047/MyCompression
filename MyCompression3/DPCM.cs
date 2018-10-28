using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompression
{
    public static class DPCM
    {
        // initial value
        private static byte _initial = 128;

        // calculate DPCM value
        public static byte[] ConvertToDPCM(byte[] contents)
        {
            byte[] result = new byte[contents.Length];

            result[0] = IgnoreNinthBit(contents[0] - _initial);

            for (int i = 1; i < contents.Length; i++)
            {
                result[i] = IgnoreNinthBit(contents[i] - contents[i - 1]);
            }

            return result;
        }

        // construct data from DPCM
        public static byte[] ConvertFromDPCM(byte[] contents)
        {
            byte[] result = new byte[contents.Length];

            result[0] = IgnoreNinthBit(contents[0] + _initial);

            for (int i = 1; i < contents.Length; i++)
            {
                result[i] = IgnoreNinthBit(contents[i] + result[i - 1]);
            }
            return result;
        }

        // only care about 8 bits, ignore sign bit
        private static byte IgnoreNinthBit(int value)
        {
            return Convert.ToByte(value & 255);
        }
    }
}
