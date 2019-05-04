using Konsole.Graphics.Colour;
using Konsole.Graphics.Primitives;
using System.Drawing;

namespace Konsole.Graphics.Drawables
{
    public class Mesh
    {
        public Triangle[] Triangles;
        /// <summary>
        /// TEMPORARY WORKAROUND, THIS NEEDS TO BE CHANGED.
        /// </summary>
        public Colour3 Colour;
        public Texture Texture;
    }
}