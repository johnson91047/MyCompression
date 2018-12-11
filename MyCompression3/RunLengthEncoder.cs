using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;

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

        public bool NeedEOB;
        public DCElement DC;
        public List<ACElement> ACs;
    }

    public class RunLengthEncoder
    {
        public RunLengthEncoder()
        {

        }

        public List<RLEBlock> Encode(List<Matrix<double>> source)
        {
            List<RLEBlock> blocks = new List<RLEBlock>();
            int lastesrDC = 0;
            foreach(Matrix<double> matrix in source)
            {
                List<double> list = ZigZag(matrix);
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

        public List<Matrix<double>> Decode(string source)
        {
            List<Matrix<double>> result = new List<Matrix<double>>();
            IEnumerable<RLEBlock> blocks = CodeWords.ReconstructRleBlocks(source);
            int latestDc = 0;
            foreach (RLEBlock rleBlock in blocks)
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
                result.Add(IZigZag(array));
            }

            return result;
        }

        private List<double> ZigZag(Matrix<double> source)
        {
            List<double> result = new List<double>();
            int step = 0;
            int i = 0, j = 0;
            bool halfTop = true;
            while (step < JPEGAlgorithm.BlockSize * JPEGAlgorithm.BlockSize)
            {
                if (halfTop)
                {
                    result.Add(source[i, j]);
                    step++;
                    j++;

                    while (j != 0)
                    {
                        result.Add(source[i, j]);
                        j--;
                        i++;
                        step++;
                    }

                    if (i != JPEGAlgorithm.BlockSize - 1)
                    {
                        result.Add(source[i, j]);
                        step++;
                        i++;

                        while (i != 0)
                        {
                            result.Add(source[i, j]);
                            j++;
                            i--;
                            step++;
                        }

                    }
                    else
                    {
                        halfTop = false;
                    }
                }
                else
                {
                    result.Add(source[i, j]);
                    step++;
                    j++;

                    while (j != JPEGAlgorithm.BlockSize - 1)
                    {
                        result.Add(source[i, j]);
                        j++;
                        i--;
                        step++;
                    }

                    result.Add(source[i, j]);
                    step++;


                    if (j == i && j == JPEGAlgorithm.BlockSize - 1)
                    {
                        break;
                    }
                    else
                    {
                        i++;
                    }

                    while (i != JPEGAlgorithm.BlockSize - 1)
                    {
                        result.Add(source[i, j]);
                        i++;
                        j--;
                        step++;
                    }
                }
            }
            return result;
        }

        private Matrix<double> IZigZag(List<double> source)
        {
            Matrix<double> result = Matrix<double>.Build.Dense(JPEGAlgorithm.BlockSize, JPEGAlgorithm.BlockSize);
            int step = 0;
            int i = 0, j = 0;
            bool halfTop = true;
            while (step < JPEGAlgorithm.BlockSize * JPEGAlgorithm.BlockSize)
            {
                if (halfTop)
                {
                    result[i, j] = source[step];
                    step++;
                    j++;

                    while (j != 0)
                    {
                        result[i, j] = source[step];
                        j--;
                        i++;
                        step++;
                    }

                    if (i != JPEGAlgorithm.BlockSize - 1)
                    {
                        result[i, j] = source[step];
                        step++;
                        i++;

                        while (i != 0)
                        {
                            result[i, j] = source[step];
                            j++;
                            i--;
                            step++;
                        }

                    }
                    else
                    {
                        halfTop = false;
                    }
                }
                else
                {
                    result[i, j] = source[step];
                    step++;
                    j++;

                    while (j != JPEGAlgorithm.BlockSize - 1)
                    {
                        result[i, j] = source[step];
                        j++;
                        i--;
                        step++;
                    }

                    result[i, j] = source[step];
                    step++;


                    if (j == i && j == JPEGAlgorithm.BlockSize - 1)
                    {
                        break;
                    }
                    else
                    {
                        i++;
                    }

                    while (i != JPEGAlgorithm.BlockSize - 1)
                    {
                        result[i, j] = source[step];
                        i++;
                        j--;
                        step++;
                    }
                }
            }
            return result;
        }
    }
}
