using System;
using System.Collections.Generic;
using System.Text;

namespace Konsole.Drawables
{
    public interface IDrawable 
    {
        float DrawScale { get; }
        float DrawPosition { get; }
        Mesh Mesh { get; }
    }
}
