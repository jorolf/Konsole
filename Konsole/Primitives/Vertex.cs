using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Konsole.Primitives
{
    public class Vertex
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
