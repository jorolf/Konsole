using Konsole.Drawables;
using Konsole.Graphics.Colour;
using Konsole.OS;
using Konsole.Primitives;
using Konsole.Rendering;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Konsole
{
    public class FrameBuffer
    {
        StringBuilder output = new StringBuilder();
        public Charsel[,] Buffer { get; private set; }

        public FrameBuffer(int Width, int Height)
        {
            Buffer = new Charsel[Width, Height];
        }
        Drawable[] drawables = new Drawable[]
        {
            new DrawableTriangle()
            {
                Scale = new Vector3(0.2f),
                Position = new Vector3(0.2f, 0.2f, 0),
            }
        };
        public void Render()
        {
            foreach (Drawable d in drawables)            
                foreach (Triangle t in d.Mesh.Triangles)               
                    for (int w = 0; w < Buffer.GetLength(0); w++) 
                        for (int h = 0; h < Buffer.GetLength(1); h++)
                        {
                            float a = Vector3.Dot(t.A.Position * d.Scale + d.Position, new Vector3(w / Buffer.GetLength(0), h / Buffer.GetLength(1), 1f));
                            float b = Vector3.Dot(t.B.Position * d.Scale + d.Position, new Vector3(w / Buffer.GetLength(0), h / Buffer.GetLength(1), 1f));
                            float c = Vector3.Dot(t.C.Position * d.Scale + d.Position, new Vector3(w / Buffer.GetLength(0), h / Buffer.GetLength(1), 1f));
                            if (a >= 0f && b >= 0f && c >= 0f)
                            {
                                //Buffer[w, h].Colour = KonsoleColour.White;
                                Buffer[w, h].Char = '█';
                            }
                            else
                            {
                                //Buffer[w, h].Colour = KonsoleColour.Black;
                                Buffer[w, h].Char = ' ';
                            }
                        }
            foreach (Charsel c in Buffer)
            {
                output.Append(c.Char);
            }
            Console.Write(output);
        }
    }
}

