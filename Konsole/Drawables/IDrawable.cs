using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Konsole.Drawables
{
    public interface IDrawable 
    {
        Vector3 Scale { get; }
        Vector3 Position { get; }
        Vector3 Rotation { get; }
        Mesh Mesh { get; }
    }
}

