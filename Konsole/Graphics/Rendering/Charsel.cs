using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Konsole.Graphics.Colour;

namespace Konsole.Graphics.Rendering
{
    /// <summary>
    /// Like a pixel but with characters (and depth information!).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Charsel
    {
        public Colour3 Colour;
        public char Char;
        public float? Depth;

        public bool Equals(Charsel other)
        {
            return Colour == other.Colour && Char == other.Char;
        }

        public override bool Equals(object obj)
        {
            return obj is Charsel other && Equals(other);
        }


        public static bool operator ==(Charsel a, Charsel b)
        {
            return a.Colour.Equals(b.Colour) && a.Char == b.Char;
        }
        public static bool operator !=(Charsel a, Charsel b)
        {
            return !a.Equals(b);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
