using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Konsole.Drawables
{
    public class Drawable : IDrawable
    {
        public Vector3 Scale { get; set; } = Vector3.One;
        public Vector3 Position { get; set; } = Vector3.One;
        public Vector3 Rotation { get; set; } = Vector3.One;
        public Mesh Mesh { get; set; } = new Mesh();
    }
}
