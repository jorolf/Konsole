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
            Buffer = new Charsel[Height, Width];
        }
        Drawable[] drawables = new Drawable[]
        {
            new DrawableTriangle()
            {
                Scale = new Vector3(1f),
                Position = new Vector3(4f, 4f, 0),
            }
        };
        public void Render()
        {
            foreach (Drawable d in drawables)
                foreach (Triangle t in d.Mesh.Triangles)
                {

                    Vector3 pos1, pos2, pos3;

                    pos1 = t.A.Position * d.Scale + d.Position;
                    pos2 = t.B.Position * d.Scale + d.Position;
                    pos3 = t.C.Position * d.Scale + d.Position;

                    const int k = 200;
                    var ab = pos2 - pos1;
                    var bc = pos3 - pos2;
                    var ac = pos3 - pos1;

                    //perimeter
                    for (int i = 0; i < k; i++)
                    {
                        Buffer[(int)pos1.Y + (int)(ab.Y * i/k), (int)pos1.X + (int)(ab.X * i/k)].Char = '█';
                        Buffer[(int)pos2.Y + (int)(bc.Y * i/k), (int)pos2.X + (int)(bc.X * i/k)].Char = '█';
                        Buffer[(int)pos1.Y + (int)(ac.Y * i/k), (int)pos1.X + (int)(ac.X * i / k)].Char = '█';
                    }

                    //fill

                    for (int y = 0; y < Buffer.GetLength(0); y++)
                    {
                        int x = 0;
                        bool found = false;
                        while (x < Buffer.GetLength(1) - 1)
                        {
                            found = Buffer[y, x].Char == '█';
                            if (found)
                                break;
                            x++;
                        }
                        if (found)
                        {
                            while (x < Buffer.GetLength(1) - 1)
                            {
                                Buffer[y, x].Char = '█';
                                x++;
                                found = Buffer[y, x].Char == '█';
                                if(found)
                                    break;
                            }
                        }

                    }

                    //Buffer[(uint)pos1.X, (uint)pos1.Y].Char = '%';
                    //Buffer[(uint)pos2.X, (uint)pos2.Y].Char = '%';
                    //Buffer[(uint)pos3.X, (uint)pos3.Y].Char = '%';
                }
            foreach (Charsel c in Buffer)
            {
                output.Append(c.Char);
            }
            Console.Write(output);
        }
    }
}

