using System.Runtime.CompilerServices;

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

        public static void ClearBuffer<T>(this T[,] array)
        {
            unsafe
            {
                void* ptr = Unsafe.AsPointer(ref array[0,0]);
                Unsafe.InitBlock(ptr, 0, (uint)(array.Length * Unsafe.SizeOf<T>()));
            }
        }

        public static unsafe uint ToUint(this byte[] array)
        {
            fixed (byte* value = &array[0])
                return *(uint*)value;
        }
    }
}