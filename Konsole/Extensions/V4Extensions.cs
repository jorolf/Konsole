using System.Numerics;
using System.Linq;

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
                return edge > 0;
        }
        public static void Bounds(Vector4 a, Vector4 b, Vector4 c, out Vector2 TopLeft, out Vector2 BottomRight)
        {
            if (a.X < b.X) TopLeft.X = a.X;
            else if (b.X < c.X) TopLeft.X = b.X;
            else TopLeft.X = c.X;

            if (a.Y < b.Y) TopLeft.Y = a.Y;
            else if (b.Y < c.Y) TopLeft.Y = b.Y;
            else TopLeft.Y = c.Y;

            if (a.X > b.X) BottomRight.X = a.X;
            else if (b.X > c.X) BottomRight.X = b.X;
            else BottomRight.X = c.X;

            if (a.Y > b.Y) BottomRight.Y = a.Y;
            else if (b.Y > c.Y) BottomRight.Y = b.Y;
            else BottomRight.Y = c.Y;
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
        public static Vector3 Barycentric(Vector4 a, Vector4 b, Vector4 c, Vector2 Point)
        {
            float weight1 = (b.Y - c.Y) * (Point.X - c.X) + (c.X - b.X) * (Point.Y - c.Y) / (b.Y - c.Y) * (a.X - c.X) + (c.X - b.X) * (a.Y - c.Y);
            float weight2 = (c.Y - a.Y) * (Point.X - c.X) + (a.X - c.X) * (Point.Y - c.Y) / (b.Y - c.Y) * (a.X - c.X) + (c.X - b.X) * (a.Y - c.Y);
            float weight3 = 1 - weight1 - weight2;
            return new Vector3(weight1, weight2, weight3);
        }
    }
}
