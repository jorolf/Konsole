using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Konsole.Drawables
{
    public class DTriangle : Drawable
    {
        public DTriangle()
        {
            Mesh.Verticies = new Vector3[]
            {
                new Vector3(0,0,0),
                new Vector3(1,0,0),
                new Vector3(0,1,0)
            };
            Mesh.Indices = new int[] { 0, 1, 2 };
        }
    }
}
