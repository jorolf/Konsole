using System.Drawing;
using System.Numerics;

namespace Konsole.Graphics.Primitives
{
    public struct Vertex
    {
        public Vector3 Position;
        public Vector2 UV;
        public Vector3 Normal;
        public Color Colour;

        public Vertex(Vector3 position, Vector3 normal, Vector2 uv)
        {
            Position = position;
            Normal = normal;
            UV = uv;
            Colour = new Color();
        }
    }
}
