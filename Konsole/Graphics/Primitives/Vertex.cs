using System.Numerics;

namespace Konsole.Graphics.Primitives
{
    public struct Vertex
    {

        public Vector3 Position;
        public Vector3 Normal;

        public Vertex(Vector3 position, Vector3 normal)
        {
            Position = position;
            Normal = normal;
        }
    }
}
