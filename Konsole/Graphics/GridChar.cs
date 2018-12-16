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
        public KonsoleColour BackgroundColour = KonsoleColour.Black;
        public KonsoleColour ForegroundColour = KonsoleColour.Black;

        public GridChar(Vector2<int> position)
        {
            Position = position;
        }
    }
}
