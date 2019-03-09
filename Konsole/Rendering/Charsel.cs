using Konsole.Graphics.Colour;
using System;
using System.Collections.Generic;
using System.Text;

namespace Konsole.Rendering
{
    /// <summary>
    /// Like a pixel but with characters.
    /// </summary>
    public struct Charsel
    {
        public KonsoleColour Colour;
        public char Char;
    }
}
