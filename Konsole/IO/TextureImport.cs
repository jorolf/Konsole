using Konsole.Extensions;
using Konsole.Graphics.Colour;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;

namespace Konsole.IO
{
    public static class TextureImport
    {
        /// <summary>
        /// Loads, converts and caches the image for later use.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static Colour3[,] LoadImage(string file)
        {
            Bitmap img;
            try
            {
                var cached = File.Open(file + ".tex", FileMode.Open);

                byte[] byteWidth = new byte[4];
                byte[] byteHeight = new byte[4];

                cached.Read(byteWidth, 0, 4);
                cached.Read(byteHeight, 0, 4);
                var width = byteWidth.Cast<byte, uint>()[0];
                var height = byteHeight.Cast<byte, uint>()[0];

                Debug.WriteLine($"{width}x{height}");
                var colourData = new byte[width * height * 3];
                cached.Read(colourData, 0, colourData.Length);
                var pixelData = new Colour3[height, width];
                var i = 0;
                for (int y = 0; y < height; y++)
                    for (int x = 0; x < width; x++)
                    {
                        pixelData[y, x] = Colour3.FromBytes(colourData[i], colourData[i + 1], colourData[i + 2]);
                        i += 3;
                    }
                cached.Close();
                return pixelData;
            }
            catch (FileNotFoundException)
            {
                var cached = File.Create(file + ".tex");
                img = new Bitmap(file + ".png");
                Colour3[,] pixelData = new Colour3[img.Size.Height, img.Size.Width];

                cached.Write(img.Width.ToByteArray(), 0, 4);
                cached.Write(img.Height.ToByteArray(), 0, 4);
                for (int y = 0; y < img.Size.Height; y++)
                    for (int x = 0; x < img.Size.Width; x++)
                    {
                        var colour = img.GetPixel(x, y);
                        pixelData[y, x] = Colour3.FromBytes(colour.R, colour.G, colour.B);
                        cached.Write(new byte[] { colour.R, colour.G, colour.B }, 0, 3);
                    }
                cached.Close();
                return pixelData;
            }
        }
    }
}
