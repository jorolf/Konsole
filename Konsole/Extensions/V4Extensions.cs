using System.Numerics;
using System.Linq;
using System;

namespace Konsole.Extensions
{
    public static class V4Extensions
    {
        public static bool Edge(Vector4 v1, Vector4 v2, Vector2 samplePoint, bool Wireframe = false)
        {
            var edge = (samplePoint.X - v1.X) * (v2.Y - v1.Y) - (samplePoint.Y - v1.Y) * (v2.X - v1.X);

            if (Wireframe)
                return edge == 0;
            else
                return edge >= 0;
        }
        public static void Bounds(Vector4 a, Vector4 b, Vector4 c, out Vector2 TopLeft, out Vector2 BottomRight)
        {
            if (a.X < b.X) TopLeft.X = MathF.Floor(a.X);
            else if (b.X < c.X) TopLeft.X = MathF.Floor(b.X);
            else TopLeft.X = MathF.Floor(c.X);

            if (a.Y < b.Y) TopLeft.Y = MathF.Floor(a.Y);
            else if (b.Y < c.Y) TopLeft.Y = MathF.Floor(b.Y);
            else TopLeft.Y = MathF.Floor(c.Y);

            if (a.X > b.X) BottomRight.X = MathF.Ceiling(a.X);
            else if (b.X > c.X) BottomRight.X = MathF.Ceiling(b.X);
            else BottomRight.X = MathF.Ceiling(c.X);

            if (a.Y > b.Y) BottomRight.Y = MathF.Ceiling(a.Y);
            else if (b.Y > c.Y) BottomRight.Y = MathF.Ceiling(b.Y);
            else BottomRight.Y = MathF.Ceiling(c.Y);
        }
        /// <summary>
        /// Remaps the <see cref="Vector4"/>'s X and Y coordinate to Pixel Space
        /// </summary>
        /// <param name="v"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <returns></returns>
        public static Vector4 ToPixel(this Vector4 v, float Width, float Height)
        {
            return v * new Vector4(Width / 2f, Height / 2f, 1, 1) + new Vector4(Width / 2f, Height / 2f, 0, 0);
        }

        /// <summary>
        /// Returns the Barycentric coordinates of the point inside the triangle.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Vector3 Barycentric(Vector4 v1, Vector4 v2, Vector4 v3, Vector2 Point)
        {
            Vector2 a = new Vector2(v2.X, v2.Y) - new Vector2(v3.X, v3.Y);
            Vector2 b = new Vector2(v1.X, v1.Y) - new Vector2(v3.X, v3.Y);
            Vector2 c = Point - new Vector2(v3.X, v3.Y);

            float aLen = a.X * a.X + a.Y * a.Y;
            float bLen = b.X * b.X + b.Y * b.Y;

            float ab = a.X * b.X + a.Y * b.Y;
            float ac = a.X * c.X + a.Y * c.Y;
            float bc = b.X * c.X + b.Y * c.Y;

            float d = aLen * bLen - ab * ab;
            float weight1 = (aLen * bc - ab * ac) / d;
            float weight2 = (bLen * ac - ab * bc) / d;
            float weight3 = 1f - weight1 - weight2;
            return new Vector3(weight1, weight2, weight3);
        }
    }
}
