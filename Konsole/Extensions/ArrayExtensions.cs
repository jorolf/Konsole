using System;
using System.Collections.Generic;
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
                {
                    array[i, a] = obj;
                }
        }
    }
}
