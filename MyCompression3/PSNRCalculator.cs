using System;

namespace MyCompression
{
    public static class PSNRCalculator
    {
        public static double Calculate(byte[] source, byte[] target)
        {
            double mse = CalculateMse(source, target);

            return 20d * Math.Log10(255) - 10d * Math.Log10(mse);
        }

        private static double CalculateMse(byte[] source, byte[] target)
        {
            double result = 0;

            for(int i = 0; i < JPEGAlgorithm.ImageSize; i++)
            {
                for (int j = 0; j < JPEGAlgorithm.ImageSize; j++)
                {
                    result += Math.Pow(source[i * JPEGAlgorithm.ImageSize + j] - target[i * JPEGAlgorithm.ImageSize + j], 2);
                }
            }

            result *= 1d / (JPEGAlgorithm.ImageSize * JPEGAlgorithm.ImageSize);

            return result;
        }
    }
}
