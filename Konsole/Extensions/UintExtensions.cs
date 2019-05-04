using System;
using System.Collections.Generic;
using System.Text;

namespace Konsole.Extensions
{
    public static class UintExtensions
    {
        public static byte[] ToByteArray(this uint v)
        {
            var arr = new byte[4];

            for (int i = 3; i >= 0; i--)
            {
                arr[i] = (byte)(v >> (i * 8));
            }
            return arr;
        }
    }
}
