using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompression
{
    public static class EntropyCalculator
    {
        private static Dictionary<byte, int> _timesOfByte = new Dictionary<byte, int>();

        public static double Calculate(byte[] contents)
        {
            _timesOfByte.Clear();

            double entropy = 0;

            foreach(byte b in contents)
            {
                if(_timesOfByte.ContainsKey(b))
                {
                    _timesOfByte[b]++;
                }
                else
                {
                    _timesOfByte[b] = 1;
                }
            }

            foreach(KeyValuePair<byte,int> pair in _timesOfByte)
            {
                double p = pair.Value / (double)contents.Length;
                entropy += p  * Math.Log(p,2);
            }


            return -entropy;
        }
    }
}
