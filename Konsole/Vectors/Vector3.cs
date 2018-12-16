using System;
using System.Collections.Generic;
using System.Text;

namespace Konsole.Vectors
{
    public class Vector3
    {
        public float X;
        public float Y;
        public float Z;
        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static explicit operator Vector3(float x)
        {
            return new Vector3(x, x, x);
        }
    }
    public class Vector3<T> where T : struct
    {
        public T X;
        public T Y;
        public T Z;
        public Vector3(T x, T y, T z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public static explicit operator Vector3<T>(T x)
        {
            return new Vector3<T>(x, x, x);
        }
    }
}
