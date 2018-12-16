using System;
using System.Collections.Generic;
using System.Text;

namespace Konsole.Vectors
{
    public class Vector2
    {
        public float X;
        public float Y;
        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }
        
        public static explicit operator Vector2(float x)
        {
            return new Vector2(x, x);
        }
    }
    public class Vector2<T> where T : struct
    {
        public T X;
        public T Y;
        public Vector2(T x, T y)
        {
            X = x;
            Y = y;
        }
        public static explicit operator Vector2<T>(T x)
        {
            return new Vector2<T>(x, x);
        }
        public static Vector2<int> ToVector2(int index, Vector2<int> bounds)
        {
            return new Vector2<int>(index % bounds.X, index / bounds.X);
        }
           
    }
}
