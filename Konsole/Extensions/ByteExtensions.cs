using System;
using System.Collections.Generic;
using System.Text;

namespace Konsole.Extensions
{
    public static class ByteExtensions
    {
        /// <summary>
        /// Changes the bite from Big to Small endian and vice versa.
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static byte SwapEndian(this byte b)
        {
            Console.WriteLine($"Starting value: {b.ToBits()}");
            byte rightShift = 0b10000000;
            byte val = 0;
            for (int i = 7; i >= 0; i--)
            {
                byte v;
                var comparedBit = (byte)((rightShift >> (7 - i)) & b);

                var offset = i * 2 - 7;
                if (offset >= 0)
                    v = (byte)(comparedBit >> offset);
                else
                    v = (byte)(comparedBit << -offset);

                val = (byte)(val | v);
            }
            return val;
        }

        public static string ToBits(this byte b)
        {
            return Convert.ToString(b, 2).PadLeft(8, '0');
        }
    }
}
