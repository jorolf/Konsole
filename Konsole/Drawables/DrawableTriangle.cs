using Konsole.Primitives;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Konsole.Drawables
{
    public class DrawableTriangle : Drawable
    {
        public DrawableTriangle()
        {
            Mesh.Triangles = new Triangle[]
            {
                new Triangle(
                    new Vertex(new Vector3(0,0,0), Vector3.Zero),
                    new Vertex(new Vector3(1,0,0), Vector3.Zero),
                    new Vertex(new Vector3(0,1,0), Vector3.Zero))

            };
        }
    }
}
