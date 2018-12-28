using Konsole.Graphics.Colour;
using Konsole.Vectors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Konsole.Graphics
{
    public class GridChar
    {
        public Vector2<int> Position;
        public char Char = ' ';
        public KonsoleColour BackgroundColour = KonsoleColour.Transparent;
        public KonsoleColour ForegroundColour = KonsoleColour.Transparent;

        public GridChar(Vector2<int> position)
        {
            Position = position;
        }
    }
}
