using Konsole.Vectors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Konsole.Graphics.Drawables
{
    public class Box
    {
        /// <summary>
        /// The positional offset.
        /// </summary>
        public Vector2<int> Position;
        /// <summary>
        /// The size of this Drawable.
        /// </summary>
        public Vector2<int> Size;
        /// <summary>
        /// The character that should be used to fill this Drawable's surface.
        /// </summary>
        public char Fill;
        public ConsoleColor FillColour;        
    }
}
