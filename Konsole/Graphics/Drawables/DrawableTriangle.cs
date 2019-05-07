using Konsole.Graphics.Primitives;
using System.Numerics;

namespace Konsole.Graphics.Drawables
{
    public class DrawableTriangle : Drawable
    {
        public DrawableTriangle()
        {
            Meshes[0].Triangles = new Triangle[]
            {
                new Triangle(
                    new Vertex(new Vector3(0,0,0), Vector3.Zero, Vector2.Zero),
                    new Vertex(new Vector3(1,0,0), Vector3.Zero, Vector2.Zero),
                    new Vertex(new Vector3(0,1,0), Vector3.Zero, Vector2.Zero))
            };
        }
    }
}
