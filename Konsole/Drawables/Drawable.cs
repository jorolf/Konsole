using System;
using System.Collections.Generic;
using System.Text;

namespace Konsole.Drawables
{
    public class Drawable : IDrawable
    {
        public float DrawScale { get; protected set; }
        public float DrawPosition { get; protected set; }
        public virtual Mesh Mesh { get; protected set; }
    }
}
