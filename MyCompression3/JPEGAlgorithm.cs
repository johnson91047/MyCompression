using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;

namespace MyCompression
{
    public class JPEGAlgorithm
    {
        public static readonly int BlockSize = 8;
        public static readonly int ImageSize = 512;
        public static readonly float Pi = 3.14159265f;
        public static readonly float SqrtInverse = 1 / 1.41421356f;

        private readonly Matrix<double> _luminanceTable = Matrix<double>.Build.DenseOfArray(new double[,]
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

        private readonly Matrix<double> _test = Matrix<double>.Build.DenseOfArray(new double[,]
        {
            {139,144,149,153,155,155,155,155},
            {144, 151, 153, 156, 159, 156, 156, 156},
            {150, 155, 160, 163, 158, 156, 156, 156},
            {159, 161, 162, 160, 160, 159, 159, 159},
            {159, 160, 161, 162, 162, 155, 155, 155},
            {161, 161, 161, 161, 160, 157, 157, 157},
            {162, 162, 161, 163, 162, 157, 157, 157},
            {162, 162, 161, 161, 163, 158, 158, 158}
        });


        private List<Matrix<double>> _blocks;
        private List<Matrix<double>> _dctedBlocks;
        private List<Matrix<double>> _quantizedBlocks;
        private readonly RunLengthEncoder _rlEncoder;

        public JPEGAlgorithm()
        {
            _blocks = new List<Matrix<double>>();
            _dctedBlocks = new List<Matrix<double>>();
            _quantizedBlocks = new List<Matrix<double>>();
            _rlEncoder = new RunLengthEncoder();
        }

        /// <summary>
        /// Main Jpeg encode
        /// </summary>
        /// <param name="source">source data</param>
        /// <param name="qf">quality factor</param>
        /// <returns>encoded bytes</returns>
        public byte[] Encode(byte[] source,int qf)
        {
            // divide into 8x8
            Divide8x8Block(source);

            // do dct for every 8x8 block
            _blocks.ForEach(block =>
            {
                _dctedBlocks.Add(DCT(block));
            });

            // do quantization
            _dctedBlocks.ForEach(dctedBlocks =>
            {
                Quantize(dctedBlocks, qf);
                _quantizedBlocks.Add(dctedBlocks);
            });

            // do RLE
            List<RLEBlock> rleBlocks = _rlEncoder.Encode(_quantizedBlocks);

            // construct code word by RLE
            return Utility.BoolArrayToByteArray(CodeWords.ConstructCodeWord(rleBlocks));

        }

        /// <summary>
        /// Main Jpeg decode
        /// </summary>
        /// <param name="source">source bits</param>
        /// <param name="qf">Quality factor</param>
        /// <returns>decoded bytes</returns>
        public byte[] Decode(string source, int qf)
        {
            _blocks.Clear();
            _dctedBlocks.Clear();
            _quantizedBlocks.Clear();

            // construct RLE blocks
            List<RLEBlock> rleblocks = CodeWords.ReconstructRleBlocks(source);

            // inverse RLE
            _quantizedBlocks = _rlEncoder.Decode(rleblocks);

            // inverse quantization
            _quantizedBlocks.ForEach(quatizedBlock =>
            {
                IQuantize(quatizedBlock,qf);
                _dctedBlocks.Add(quatizedBlock);
            });

            // inverse dct
            _dctedBlocks.ForEach(dctedBlocks =>
            {
                var block = IDCT(dctedBlocks);
                _blocks.Add(block);
            });

            // reconstruct image from blocks
            return Construct8x8Block(_blocks);
        }

        private void Divide8x8Block(byte[] source)
        {
            for (int i = 0; i < ImageSize; i += 8)
            {
                for (int j = 0; j < ImageSize; j += 8)
                {
                    Matrix<double> block = Matrix<double>.Build.Dense(BlockSize, BlockSize);
                    for (int startj = 0; startj < BlockSize; startj++)
                    {
                        for (int starti = 0; starti < BlockSize; starti++)
                        {
                            block[starti, startj] = Convert.ToDouble(source[(i + starti) * (ImageSize) + (j + startj)]);
                        }
                    }
                    _blocks.Add(block);
                }
            }
        }

        private byte[] Construct8x8Block(List<Matrix<double>> source)
        {
            Matrix<double> image = Matrix<double>.Build.Dense(ImageSize, ImageSize);
            List<byte> result = new List<byte>();
            int numOfBlocks = 0;
            for (int i = 0; i < ImageSize; i += 8)
            {
                for (int j = 0; j < ImageSize; j += 8)
                {
                    for (int starti = 0; starti < BlockSize; starti++)
                    {
                        for (int startj = 0; startj < BlockSize; startj++)
                        {
                            image[i + starti, j + startj] = source[numOfBlocks][starti, startj];
                        }
                    }

                    numOfBlocks++;
                }
            }

            foreach (double d in image.ToRowMajorArray())
            {
                result.Add(Convert.ToByte((int)d));
            }

            return result.ToArray();
        }

        private static double C(int index)
        {
            return index == 0 ? SqrtInverse : 1;
        }

        // do dct algorithm
        private Matrix<double> DCT(Matrix<double> source)
        {
            Matrix<double> result = Matrix<double>.Build.Dense(BlockSize,BlockSize);
            source = source.Subtract(128);
            for(int v = 0; v < BlockSize; v++)
            {
                for( int u = 0; u <　BlockSize; u++)
                {
                    double sourceData = 0;
                    for(int y = 0; y < BlockSize; y++)
                    {
                        for (int x = 0; x < BlockSize; x++)
                        {
                            sourceData += source[y,x] * Math.Cos(((2 * x + 1) * u * Pi) / 16) * Math.Cos(((2 * y + 1) * v * Pi) / 16);
                        }
                    }
                    double dctData = C(v) * C(u) * sourceData * 0.25;

                    result[v, u] = Math.Round(dctData);
                }
            }

            return result;
        }

        // do inverse DCT
        private Matrix<double> IDCT(Matrix<double> source)
        {
            Matrix<double> result = Matrix<double>.Build.Dense(BlockSize, BlockSize);
            for (int y = 0; y < BlockSize; y++)
            {
                for (int x = 0; x < BlockSize; x++)
                {
                    double sourceData = 0;
                    for (int v = 0; v < BlockSize; v++)
                    {
                        for (int u = 0; u < BlockSize; u++)
                        {
                            sourceData += 0.25 * C(u) * C(v) * source[v, u] * Math.Cos(((2 * v + 1) * y * Pi) / 16) * Math.Cos(((2 * u + 1) * x * Pi) / 16);
                        }
                    }
                    result[y, x] = Math.Round(sourceData );
                }
            }

            result = result.Add(128);
            ClampMatrix(result);

            return result;
        }

        private void Quantize(Matrix<double> source, float qf = 50)
        {
            Matrix<double> quantizationTable = _luminanceTable.Multiply(GetFactor(qf));
            for (int i = 0; i < BlockSize; i++)
            {
                for(int j = 0; j < BlockSize; j++)
                {
                    source[i, j] = Math.Round(source[i, j] / quantizationTable[i, j]);
                }
            }
        }

        private void IQuantize(Matrix<double> source, float qf = 50)
        {
            Matrix<double> quantizationTable = _luminanceTable.Multiply(GetFactor(qf));
            for (int i = 0; i < BlockSize; i++)
            {
                for (int j = 0; j < BlockSize; j++)
                {
                    source[i, j] = Math.Round(source[i, j] * quantizationTable[i, j]);
                }
            }
        }

        private double GetFactor(float qf)
        {
            double factor;
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

        private void ClampMatrix(Matrix<double> matrix)
        {
            for (int i = 0; i < BlockSize; i++)
            {
                for (int j = 0; j < BlockSize; j++)
                {
                    matrix[i, j] = matrix[i, j] < 0 ? 0 : (matrix[i, j] > 255 ? 255 : matrix[i, j]);
                }
            }
        }
    }
}
