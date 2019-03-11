using Konsole.Graphics.Primitives;
using System.Numerics;

namespace Konsole.Graphics.Drawables
{
    public class DrawableTriangle : Drawable
    {
        public DrawableTriangle()
        {
            Mesh.Triangles = new Triangle[]
            {
                new Triangle(
                    new Vertex(new Vector3(0,0,0), Vector3.Zero),
                    new Vertex(new Vector3(10,0,0), Vector3.Zero),
                    new Vertex(new Vector3(0,10,0), Vector3.Zero))

            };
        }
    }
}
