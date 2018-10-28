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
        private static byte _initial = 128;

        public static byte[] ConvertToDPCM(byte[] contents)
        {
            byte[] result = new byte[contents.Length];

            result[0] = Quantize(contents[0] - _initial);

            for (int i = 1; i < contents.Length; i++)
            {
                result[i] = Quantize(contents[i] - contents[i - 1]);
            }

            return result;
        }

        public static byte[] ConvertFromDPCM(byte[] contents)
        {
            byte[] result = new byte[contents.Length];

            result[0] = Convert.ToByte(DeQuantize(contents[0]) + _initial);

            for (int i = 1; i < contents.Length; i++)
            {
                result[i] = Convert.ToByte(DeQuantize(contents[i]) + result[i - 1]);
            }

            return result;
        }

        // make value always be 0~255
        private static byte Quantize(int value)
        {
            return Convert.ToByte(Math.Floor((float)(value + 255) / 2));
        }

        // revert quantification
        private static int DeQuantize(byte value)
        {
            return value * 2 - 255;
        }
    }
}
