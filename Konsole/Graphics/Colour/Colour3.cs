using System;
using System.Numerics;

namespace Konsole.Graphics.Colour
{
#pragma warning disable CS0660, CS0661
    /// <summary>
    /// Stores the RGB values in form of a float ranging from 0-1 for 0-255
    /// </summary>
    public struct Colour3 : IEquatable<Colour3>, IEquatable<Vector3>
    {

        private Vector3 colour;
        public float R { get => colour.X; set => colour.X = value; }
        public float G { get => colour.Y; set => colour.X = value; }
        public float B { get => colour.Z; set => colour.Z = value; }

        /// <summary>
        /// Constructs the Colour3 from floats.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public Colour3(float r, float g, float b)
        {
            colour = new Vector3(r, g, b);
        }

        /// <summary>
        /// Sets all three Colour channels to the brightness value.
        /// </summary>
        /// <param name="brightness"></param>
        public Colour3(float brightness)
        {
            colour = new Vector3(brightness);
        }

        public static Colour3 FromBytes(byte r, byte g, byte b)
        {
            return new Colour3(r / 255f, g / 255f, b / 255f);
        }

        #region Operators
        public static Colour3 operator *(Colour3 a, float num)
        {
            return new Colour3(a.R * num, a.G * num, a.B * num);
        }

        public static Colour3 operator /(Colour3 a, float num)
        {
            return new Colour3(a.R / num, a.G / num, a.B / num);
        }

        public static Colour3 operator +(Colour3 a, Colour3 b)
        {
            return new Colour3(a.R + b.R, a.G + b.G, a.B + b.B);
        }

        public static Colour3 operator *(Colour3 a, Colour3 b)
        {
            return new Colour3(a.R * b.R, a.G * b.G, a.B * b.B);
        }

        public static bool operator ==(Colour3 a, Colour3 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Colour3 a, Colour3 b)
        {
            return !a.Equals(b);
        }

        public static bool operator ==(Colour3 a, Vector3 b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Colour3 a, Vector3 b)
        {
            return !a.Equals(b);
        }
        #endregion
        /// <summary>
        /// Returns the Colour information in the byte format as an array of three Bytes.
        /// </summary>
        /// <returns></returns>
        public byte[] ToByte()
        {
            return new byte[]
            {
                (byte)(MathF.Min(R, 1) * 255),
                (byte)(MathF.Min(G, 1) * 255),
                (byte)(MathF.Min(B, 1) * 255)
            };
        }

        public bool Equals(Colour3 other)
        {
            if (colour == other.colour)
                return true;
            else
                return false;
        }

        public bool Equals(Vector3 other)
        {
            if (colour == other)
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            return $"{R}, {G}, {B}";
        }
        public string ToByteString()
        {
            return $"{(byte)(R * 255)}, {(byte)(G * 255)}, {(byte)(B * 255)}";
        }
        #region Colours
        public static Colour3 White => new Colour3(1);
        public static Colour3 LightGrey => new Colour3(0.75f);
        public static Colour3 Grey => new Colour3(0.5f);
        public static Colour3 DarkGrey => new Colour3(0.25f);
        public static Colour3 Black => new Colour3(0);
        public static Colour3 KozianPurple => new Colour3(166 / 255f, 61 / 255f, 198 / 255f);

        #endregion
    }
}