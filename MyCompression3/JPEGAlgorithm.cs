using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MyCompression
{
    public class JPEGAlgorithm
    {
        private const int BLOCK_SIZE = 8;
        private const int IMAGE_SIZE = 512;
        private const float PI = 3.14159265f;
        private const float SQRT = 1.41421356f;
        private readonly Matrix<double> LUMINANCE_TABLE = Matrix<double>.Build.DenseOfArray(new double[,]
        {
            {16,11,10,16,24,40,51,61},
            {12,12,14,19,26,58,60,55},
            {14,13,16,24,40,57,69,56},
            {14,17,22,29,51,87,80,62},
            {18,22,37,56,68,109,103,77},
            {24,36,55,64,81,104,113,92},
            {49,64,78,87,103,121,120,101},
            {72,92,95,96,112,100,103,99}
        });
        private List<Matrix<double>> _blocks;
        private List<Matrix<double>> _dctedBlocks;
        private List<Matrix<double>> _quantizedBlocks;
        private List<List<double>> _zigzagedBlocks;
        private RunLengthEncoder _rlEncoder;

        public JPEGAlgorithm()
        {
            _blocks = new List<Matrix<double>>();
            _dctedBlocks = new List<Matrix<double>>();
            _quantizedBlocks = new List<Matrix<double>>();
            _zigzagedBlocks = new List<List<double>>();
            _rlEncoder = new RunLengthEncoder();
        }

        public bool[] Encode(byte[] source,int qf)
        {
            // divide into 8x8
            Divide8x8Block(source);

            // do dct for every 8x8 block
            _blocks.ForEach(block =>
            {
                var _block = block.Subtract(128);
                _dctedBlocks.Add(DCT(_block));
            });

            // do quantization
            _dctedBlocks.ForEach(dctedBlocks =>
            {
                Quantize(dctedBlocks, qf);
                _quantizedBlocks.Add(dctedBlocks);
            });

            // do RLE
            _quantizedBlocks.ForEach(block =>
            {
                var zigzagedblock = ZigZag(block);
                _zigzagedBlocks.Add(zigzagedblock);
            });
            List<RLEBlock> rleBlocks = _rlEncoder.Encode(_zigzagedBlocks);

            // construct code word


            return CodeWords.ConstructCodeWord(rleBlocks) as bool[];

        }

        private void Divide8x8Block(byte[] source)
        {
            List<double> data = new List<double>();
            for (int i = 0; i < IMAGE_SIZE; i += 8)
            {
                for (int j = 0; j < IMAGE_SIZE; j += 8)
                {
                    for (int startj = 0; startj < BLOCK_SIZE; startj++)
                    {
                        for (int starti = 0; starti < BLOCK_SIZE; starti++)
                        {
                            data.Add(Convert.ToDouble(source[(i + starti) * (IMAGE_SIZE) + (j + startj)]));
                        }
                    }
                    Matrix<double> block = Matrix<double>.Build.DenseOfColumnMajor(BLOCK_SIZE, BLOCK_SIZE, data.ToArray());
                    data.Clear();
                    _blocks.Add(block);
                }
            }
        }

        private float C(int index)
        {
            return index == 0 ? (1 / SQRT) : 1;
        }

        // do dct algorithm
        private Matrix<double> DCT(Matrix<double> source)
        {
            Matrix<double> result = Matrix<double>.Build.Dense(BLOCK_SIZE,BLOCK_SIZE);
            for(int v = 0; v < BLOCK_SIZE; v++)
            {
                for( int u = 0; u <　BLOCK_SIZE; u++)
                {
                    double sourceData = 0;
                    for(int y = 0; y < BLOCK_SIZE; y++)
                    {
                        for (int x = 0; x < BLOCK_SIZE; x++)
                        {
                            sourceData += source[y,x] * Math.Cos(((2 * x + 1) * u * PI) / 16) * Math.Cos(((2 * y + 1) * v * PI) / 16);
                        }
                    }
                    double dctData = (C(v) / 2) * (C(u) / 2) * sourceData;

                    result[v, u] = Math.Round(dctData);
                }
            }

            return result;
        }

        private void Quantize(Matrix<double> source, float qf = 50)
        {
            Matrix<double> quantizationTable = LUMINANCE_TABLE.Multiply(GetFactor(qf));
            for (int i = 0; i < BLOCK_SIZE; i++)
            {
                for(int j = 0; j < BLOCK_SIZE; j++)
                {
                    source[i, j] = Math.Round(source[i, j] / quantizationTable[i, j]);
                }
            }
        }

        private float GetFactor(float qf)
        {
            float factor = 0;
            if(qf < 50)
            {
                factor = 5000 / qf;
            }
            else
            {
                factor = 200 - 2 * qf;
            }
            return factor / 100;
        }

        private List<double> ZigZag(Matrix<double> source)
        {
            List<double> result = new List<double>();
            int step = 0;
            int i = 0, j = 0;
            bool halfTop = true;
            while(step < BLOCK_SIZE * BLOCK_SIZE)
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

                    if (i != BLOCK_SIZE - 1)
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

                    while (j != BLOCK_SIZE - 1)
                    {
                        result.Add(source[i, j]);
                        j++;
                        i--;
                        step++;
                    }

                    result.Add(source[i, j]);
                    step++;


                    if (j == i && j == BLOCK_SIZE - 1)
                    {
                        break;
                    }
                    else
                    {
                        i++;
                    }

                    while (i != BLOCK_SIZE - 1)
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

    }
}
