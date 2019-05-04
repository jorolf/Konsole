using Konsole.Graphics.Colour;
using System;
using System.Collections.Generic;
using System.Text;

namespace Konsole.Graphics
{
    public class TempTexture : Texture
    {
        public TempTexture()
        {
            ColourData = new Colour3[,]
            {
                {new Colour3(1, 1, 1), new Colour3(1, 0, 0), new Colour3(0, 1, 0) },
                {new Colour3(0, 0, 1), new Colour3(1, 1, 0), new Colour3(0, 1, 1) },
                {new Colour3(1, 0, 1), new Colour3(0.6f, 0.6f, 0.6f), new Colour3(0.33f, 0.33f, 0.33f) }
            };
        }
    }
}
