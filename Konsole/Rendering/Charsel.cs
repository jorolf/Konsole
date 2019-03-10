using Konsole.Graphics.Colour;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Konsole.Rendering
{
    /// <summary>
    /// Like a pixel but with characters.
    /// </summary>
    public struct Charsel
    {
        public Color Colour;
        public char Char;
    }
}
