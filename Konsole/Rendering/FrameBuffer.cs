using Konsole.Drawables;
using Konsole.Graphics.Colour;
using Konsole.IO;
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

        List<Drawable> drawables = new List<Drawable>();

        int width, height;

        public FrameBuffer(int Width, int Height)
        {
            width = Width;
            height = Height;
            Buffer = new Charsel[Height, Width];
            Drawable d = new Drawable();
            d.Mesh = new Mesh();
            d.Mesh.Triangles = OBJParser.ParseFile("player.obj");
            d.Scale = new Vector3(25);
            drawables.Add(d);
        }

        private float time = 0f;

        public void Render()
        {
            output.Clear();
            Console.SetCursorPosition(0, 0);
            time += 0.1f;
            Buffer = new Charsel[width, height];
            foreach (Drawable d in drawables)
                foreach (Triangle t in d.Mesh.Triangles)
                {

                    Vector3 pos1, pos2, pos3;

                    pos1 = t.A.Position;
                    pos2 = t.B.Position;
                    pos3 = t.C.Position;

                    var m = Matrix4x4.Identity;
                    //m *= Matrix4x4.CreateRotationZ(time);
                    m *= Matrix4x4.CreateRotationY(time);
                    m *= Matrix4x4.CreateScale(d.Scale);
                    m *= Matrix4x4.CreateScale(new Vector3(2f, 1, 1));
                    m *= Matrix4x4.CreateTranslation(new Vector3(80, 75, 0));

                    pos1 = Vector3.Transform(pos1, m);
                    pos2 = Vector3.Transform(pos2, m);
                    pos3 = Vector3.Transform(pos3, m);

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
                                //Buffer[y, x].Char = '█';
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
            //output.Append("\u001b[H");
            Console.Write(output);
        }
    }
}

