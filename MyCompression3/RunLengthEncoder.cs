using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;

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
        public RLEBlock()
        {
            ACs = new List<ACElement>();
        }

        public RLEBlock(DCElement dc, List<ACElement> acs)
        {
            DC = dc;
            ACs = acs;
        }

        public DCElement DC;
        public List<ACElement> ACs;
    }

    public class RunLengthEncoder
    {
        public RunLengthEncoder()
        {

        }

        /// <summary>
        /// Run-length encode , ( origin image blocks -> zigzaged image blocks -> RLE blocks )
        /// </summary>
        /// <param name="source">image blocks</param>
        /// <returns></returns>
        public List<RLEBlock> Encode(List<Matrix<double>> source)
        {
            List<RLEBlock> blocks = new List<RLEBlock>();
            int lastesrDC = 0;
            foreach(Matrix<double> matrix in source)
            {
                List<double> list = ZigZagEncoder.ZigZag(matrix);
                List<ACElement> acs = new List<ACElement>();
                int run = 0;
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
                        if (run >= 16)
                        {
                            int times = run / 16;
                            run %= 16;
                            for (int i = 0; i < times; i++)
                            {
                                acs.Add(new ACElement(15, 0));
                            }
                        }
                        acs.Add(new ACElement(run, (int)data));
                    }
                }
                blocks.Add(new RLEBlock(dc, acs));
            }

            return blocks;
        }

        /// <summary>
        /// Run-length decode ( RLE blocks -> zigzaged image blocks -> inverse zigzag -> origin image block )
        /// </summary>
        /// <param name="source">source data bits</param>
        /// <returns></returns>
        public List<Matrix<double>> Decode(IEnumerable<RLEBlock> source)
        {
            List<Matrix<double>> result = new List<Matrix<double>>();
            int latestDc = 0;
            foreach (RLEBlock rleBlock in source)
            {
                List<double> array = new List<double>();
                DCElement dc = rleBlock.DC;
                array.Add(dc.Diff+latestDc);
                latestDc = dc.Diff;

                foreach (ACElement ac in rleBlock.ACs)
                {
                    for (int i = 0; i < ac.Length; i++)
                    {
                        array.Add(0);
                    }
                    array.Add(ac.Value);
                }

                if (array.Count < 64)
                {
                    int numOfZero = 64 - array.Count;
                    for (int i = 0; i < numOfZero; i++)
                    {
                        array.Add(0);
                    }
                }
                result.Add(ZigZagEncoder.IZigZag(array));
            }

            return result;
        }

      
    }
}
