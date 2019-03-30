using Konsole.Graphics.Colour;
using System;
using System.Collections.Generic;
using System.Text;

namespace Konsole.Graphics
{
    public class Texture
    {
        int Width { get => ColourData.GetLength(1); }
        int Height { get => ColourData.GetLength(0); }
        Colour3[,] ColourData;
    }
}
