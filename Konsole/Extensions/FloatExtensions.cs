
using System;

namespace Konsole.Extensions
{
    public static class FloatExtensions
    {
        public static float Remap(this float number, float oldFloor, float oldCeiling, float newFloor = 0, float newCeiling = 1)
        {
            return (number - oldFloor) * (newCeiling  - newFloor) / (oldCeiling - oldFloor) + newFloor;
        }

        public static byte ToByte(this float f)
        {
            return (byte)(MathF.Min(f, 1) * 255f);
        }
    }
}
