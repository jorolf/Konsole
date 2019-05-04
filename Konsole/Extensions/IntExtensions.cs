using System;
using System.Collections.Generic;
using System.Text;

namespace Konsole.Extensions
{
    public static class IntExtensions
    {
        public static byte[] ToByteArray(this int v)
        {
            return UintExtensions.ToByteArray((uint)v);
        }
    }
}
