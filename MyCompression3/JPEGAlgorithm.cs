using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompression
{
    public class JPEGAlgorithm
    {
        private const int BLOCK_SIZE = 8;
        private const int IMAGE_SIZE = 512;
        private const float PI = 3.14159265f;
        private const float SQRT = 1.41421356f;
        private readonly Matrix<sbyte> QUANTIZATION_TABLE;
        private List<Matrix<byte>> _blocks;
        private List<Matrix<sbyte>> _dctedBlocks;

        public JPEGAlgorithm()
        {
            _blocks = new List<Matrix<byte>>();
            _dctedBlocks = new List<Matrix<sbyte>>();
            QUANTIZATION_TABLE = Matrix<sbyte>.Build.DenseOfArray(new sbyte[,]
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
        }

        public bool[] Encode(List<byte> source)
        {
            // divide into 8x8
            List<byte> data = new List<byte>(); 
            for(int i = 0; i < IMAGE_SIZE; i+= 8)
            {
                for(int j = 0; j < IMAGE_SIZE; j+= 8)
                {
                    for (int starti = 0 ; starti < BLOCK_SIZE; starti++)
                    {
                        for (int startj = 0 ; startj < BLOCK_SIZE; startj++)
                        {
                            data.Add(source[(i+starti) * IMAGE_SIZE + (j+startj)]);
                        }
                    }
                    Matrix<byte> block = Matrix<byte>.Build.DenseOfRowMajor(BLOCK_SIZE, BLOCK_SIZE, data.ToArray());
                    _blocks.Add(block);
                }
            }

            // do dct on every 8x8 block
            _blocks.ForEach(block => _dctedBlocks.Add(DCT(block)));

            // do quantization
            _dctedBlocks.ForEach(block => Quantize(block));

            //TODO: make code word
        }

        private float C(int index)
        {
            return index == 0 ? 1 / SQRT : 1;
        }

        private Matrix<sbyte> DCT(Matrix<byte> source)
        {
            Matrix<sbyte> result = Matrix<sbyte>.Build.Dense(BLOCK_SIZE,BLOCK_SIZE);
            for(int u = 0; u < BLOCK_SIZE; u++)
            {
                for( int v = 0; v <　BLOCK_SIZE; v++)
                {
                    double sourceData = 0;
                    for(int y = 0; y < BLOCK_SIZE; y++)
                    {
                        for (int x = 0; x < BLOCK_SIZE; x++)
                        {
                            sourceData += Math.Cos(((2 * x + 1) * u * PI) / 16) + Math.Cos(((2 * y + 1) * v * PI) / 16);
                        }
                    }
                    int dctData = Convert.ToInt32((C(v) / 2) * (C(u) / 2) * sourceData);

                    result[v, u] = Convert.ToSByte(dctData);
                }
            }

            return result;
        }

        private void Quantize(Matrix<sbyte> source)
        {
            source = source * QUANTIZATION_TABLE.Inverse();
        }
    }
}
