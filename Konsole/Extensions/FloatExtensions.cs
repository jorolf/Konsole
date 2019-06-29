
using System;
using System.Numerics;

namespace Konsole.Extensions
{
    public static class FloatExtensions
    {
        public static float Remap(this float number, float oldFloor, float oldCeiling, float newFloor = 0, float newCeiling = 1)
        {
            return (number - oldFloor) * (newCeiling - newFloor) / (oldCeiling - oldFloor) + newFloor;
        }

        public static byte ToByte(this float f)
        {
            return (byte)(Math.Min(f, 1) * 255f);
        }
        public static int ToInt(this float f)
        {
            if (f % 1 >= 0.5f)
                return (int)f + 1;
            else
                return (int)f;
        }
    }
}
