using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompression
{
    public class ACElement
    {
        public ACElement(int length, int value)
        {
            Length = length;
            Value = value;
        }

        public int Length;
        public int Value;
    }
    public class DCElement
    {
        public DCElement(int diff)
        {
            Diff = diff;
        }
        public int Diff;
    }

    public class RLEBlock
    {
        public RLEBlock(DCElement dc, List<ACElement> acs, bool needEOB)
        {
            DC = dc;
            ACs = acs;
            NeedEOB = needEOB;
        }

        public bool NeedEOB;
        public DCElement DC;
        public List<ACElement> ACs;
    }

    public class RunLengthEncoder
    {
        public RunLengthEncoder()
        {

        }

        public List<RLEBlock> Encode(List<List<double>> source)
        {
            List<RLEBlock> blocks = new List<RLEBlock>();
            int lastesrDC = 0;
            foreach(List<double> list in source)
            {
                List<ACElement> acs = new List<ACElement>();
                int run = 0;
                bool needEOB = true;
                int diff = (int)list[0] - lastesrDC;
                lastesrDC = (int)list[0];
                DCElement dc = new DCElement(diff);
                List<double> listac = list.Skip(1).ToList();
                foreach (double data in listac)
                {
                    if(data == 0)
                    {
                        run++;
                    }
                    else
                    {
                        acs.Add(new ACElement(run, (int)data));
                        if(listac.Last() == data)
                        {
                            needEOB = false;
                        }
                    }

                    if(run == 15 )
                    {
                        acs.Add(new ACElement(run, 0));
                        run = 0;
                    }
                }
                blocks.Add(new RLEBlock(dc, acs, needEOB));
            }

            return blocks;
        }
    }
}
