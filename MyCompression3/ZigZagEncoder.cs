using MathNet.Numerics.LinearAlgebra;
using System.Collections.Generic;

namespace MyCompression
{
    public static class ZigZagEncoder
    {
        public static List<double> ZigZag(Matrix<double> source)
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

        public static Matrix<double> IZigZag(List<double> source)
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
