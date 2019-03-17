using System;
using System.Collections.Generic;
using System.Text;

namespace Konsole.Graphics.Colour
{
    /// <summary>
    /// Stores the RGB values in form of a float ranging from 0-1 for 0-255
    /// </summary>
    public struct Colour3
    {
        public float R;
        public float G;
        public float B;

        /// <summary>
        /// Constructs the Colour3 from floats.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public Colour3(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
        }

        public static Colour3 FromBytes(byte r, byte g, byte b)
        {
            return new Colour3(r / 255f, g / 255f, b / 255f);
        }

        public static Colour3 operator *(Colour3 a, float num)
        {
            return new Colour3(a.R * num, a.R * num, a.R * num);
        }

        public static Colour3 operator /(Colour3 a, float num)
        {
            return new Colour3(a.R / num, a.R / num, a.R / num);
        }

        public static Colour3 operator +(Colour3 a, Colour3 b)
        {
            return new Colour3(a.R + b.R, a.G + b.G, a.B + b.B);
        }

        /// <summary>
        /// Returns the Colour information in the byte format as an array of three Bytes.
        /// </summary>
        /// <returns></returns>
        public byte[] ToByte()
        {
            return new byte[]
            {
                (byte)(MathF.Min(G, 1) * 255),
                (byte)(MathF.Min(G, 1) * 255),
                (byte)(MathF.Min(B, 1) * 255)
            };
        }
    };
}
