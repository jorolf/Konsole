using Konsole.Graphics.Colour;
using Konsole.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace Konsole.Graphics
{
    public class ImageTexture : Texture
    {
        public ImageTexture(string file)
        {
            ColourData = TextureImport.LoadImage(file);
        }

    }
}
