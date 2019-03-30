using System;
using System.Text;

namespace Konsole.Extensions
{
    public static class ArrayExtensions
    {
        //Temporary solution until we find a way to write a Method that populates arrays with arbitrary numbers of dimensions.
        public static void Populate<T>(this T[,] array, T obj)
        {
            for (int i = 0; i < array.GetLength(0); i++)
                for (int a = 0; a < array.GetLength(1); a++)
                    array[i, a] = obj;
        }
        public static uint ToUint(this byte[] array)
        {
            if (array.Length != 4)
                throw new ArgumentException("The byte array must contain four bytes");
            else
            {
                uint[] ints = new uint[]
                {
                    array[3],
                    array[2],
                    array[1],
                    array[0]
                };
                byte count = 0;
                for (int i = 0; i < 4; i++)
                {
                    ints[i] = ints[i] << (8 * count);
                    count++;
                }

                uint value = 0;

                foreach (uint u in ints)
                    value = value | u;

                return value;
            }
        }
    }
}