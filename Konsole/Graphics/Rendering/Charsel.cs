using System.Drawing;

namespace Konsole.Graphics.Rendering
{
    /// <summary>
    /// Like a pixel but with characters (and depth information!).
    /// </summary>
    public struct Charsel
    {
        public Color Colour;
        public char Char;
        public float? Depth;
    }
}
