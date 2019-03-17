using Konsole.Graphics.Colour;

namespace Konsole.Graphics.Rendering
{
    /// <summary>
    /// Like a pixel but with characters (and depth information!).
    /// </summary>
    public struct Charsel
    {
        public Colour3 Colour;
        public char Char;
        public float? Depth;
    }
}
