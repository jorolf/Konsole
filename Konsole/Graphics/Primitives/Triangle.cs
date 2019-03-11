using System.Numerics;

namespace Konsole.Graphics.Primitives
{
    public struct Triangle
    {
        public Vertex A { get; private set; }
        public Vertex B { get; private set; }
        public Vertex C { get; private set; }

        public Triangle(Vertex a, Vertex b, Vertex c)
        {
            A = a;
            B = b;
            C = c;
        }

        /// <summary>
        /// The normal of this <see cref="Triangle"/>
        /// </summary>
        public Vector3 Normal
        {
            get
            {
                return Vector3.Cross(B.Position - A.Position, C.Position - A.Position);
            }
        }
    }
}
