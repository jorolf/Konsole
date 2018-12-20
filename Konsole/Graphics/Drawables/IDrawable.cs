using Konsole.Graphics.Colour;
using Konsole.Vectors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Konsole.Graphics.Drawables
{
    public interface IDrawable
    {
        Vector2 DrawSize { get; }
        /// <summary>
        /// The positional offset.
        /// </summary>
        Vector2<int> Position { get; set; }
        /// <summary>
        /// The size of this Drawable.
        /// </summary>
        Vector2<int> Size { get; set; }             
    }
}
