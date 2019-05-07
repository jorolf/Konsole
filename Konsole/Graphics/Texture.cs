using Konsole.Graphics.Colour;
using System;
using System.Collections.Generic;
using System.Text;

namespace Konsole.Graphics
{
    public class Texture
    {
        public Colour3 this[int x, int y]
        {
            get => ColourData[y, x];
            set => ColourData[y, x] = value;
        }

        public int Width { get => ColourData.GetLength(1); }
        public int Height { get => ColourData.GetLength(0); }
        /// <summary>
        /// The colour data of the texture in the Y X order.
        /// </summary>
        public Colour3[,] ColourData;
    }
}
