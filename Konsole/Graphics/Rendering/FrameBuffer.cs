using Konsole.Clocks;
using Konsole.Extensions;
using Konsole.Graphics.Drawables;
using Konsole.Graphics.Primitives;
using Konsole.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Reflection;
using System.Text;

namespace Konsole.Graphics.Rendering
{
    public class FrameBuffer
    {
        private readonly StringBuilder output = new StringBuilder();

        public Charsel[,] Buffer { get; private set; }

        private readonly Action<string> consoleWriter;

        private readonly List<Drawable> drawables = new List<Drawable>();

        private Matrix4x4 viewMatrix = Matrix4x4.CreateLookAt(Vector3.Zero, new Vector3(0, 0, 1), new Vector3(0, 1, 0));
        private Matrix4x4 projectionMatrix;

        private FrameClock clock = new FrameClock();

        private bool bufferInvalid;

        private int width;

        public int Width
        {
            get => width;
            set
            {
                if (value == width) return;

                width = value;
                bufferInvalid = true;
            }
        }

        private int height;

        public int Height
        {
            get => height;
            set
            {
                if (value == height) return;

                height = value;
                bufferInvalid = true;
            }
        }

        public FrameBuffer(Action<string> writeAction)
        {
            bufferInvalid = true;
            consoleWriter = writeAction;

            Drawable d = new Drawable
            {
                Mesh = new Mesh
                {
                    Triangles = OBJParser.ParseFile(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar + "player.obj", Properties.FlipY),
                    Colour = Color.White
                },
                Scale = new Vector3(0.33f),
                Position = new Vector3(0,0,1),
                Origin = new Vector3(0,1.2f,0),
                Rotation = new Vector3(MathF.PI / 2,0,0)
            };
            Drawable s = new Drawable
            {
                Mesh = new Mesh
                {
                    Triangles = OBJParser.ParseFile(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar + "shrek.obj", Properties.FlipY),
                    Colour = Color.FromArgb(123, 186, 46)
                },
                Scale = new Vector3(0.60f),
                Position = new Vector3(0, 0, 1.5f),
                Origin = new Vector3(0, 0.4f, 0),
            };
            drawables.Add(d);
            drawables.Add(s);
        }

        private float time;

        public void Render()
        {
            output.Clear();

            if (bufferInvalid)
            {
                Buffer = new Charsel[Height, Width];
                output.Append("\u001b[?25l");
                projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(1.5708f, Width / (float) Height, 1, float.PositiveInfinity);
                bufferInvalid = false;
            }

            Buffer.Populate(new Charsel
            {
                Char = ' ',
                Colour = Color.White,
                Depth = null
            });

            drawables[1].Position = new Vector3(0, 0, MathF.Sin((float)clock.Time * 1.2f) + 1.2f);
            foreach (Drawable d in drawables)
            {
                foreach (Triangle t in d.Mesh.Triangles)
                {
                    Vector4 pos1 = new Vector4(t.A.Position, 1);
                    Vector4 pos2 = new Vector4(t.B.Position, 1);
                    Vector4 pos3 = new Vector4(t.C.Position, 1);

                    var matrix = d.DrawableMatrix;

                    matrix *= viewMatrix;
                    matrix *= projectionMatrix;

                    pos1 = Vector4.Transform(pos1, matrix);
                    pos2 = Vector4.Transform(pos2, matrix);
                    pos3 = Vector4.Transform(pos3, matrix);

                    pos1 /= pos1.W;
                    pos2 /= pos2.W;
                    pos3 /= pos3.W;

                    pos1 = pos1 * new Vector4(Width, Height / 2f, 1, 1) + new Vector4(Width / 2f, Height / 2f, 0, 0);
                    pos2 = pos2 * new Vector4(Width, Height / 2f, 1, 1) + new Vector4(Width / 2f, Height / 2f, 0, 0);
                    pos3 = pos3 * new Vector4(Width, Height / 2f, 1, 1) + new Vector4(Width / 2f, Height / 2f, 0, 0);

                    void DrawLine(Vector4 a, Vector4 b)
                    {
                        //We need a way to stop this from killing the performance due to the loop running way too many times in cases of long lines that go off-screen.
                        float k = Vector4.Distance(a, b);

                        //perimeter
                        for (int i = 0; i < k; i++)
                        {
                            Vector4 abLerp = Vector4.Lerp(a, b, i / k);

                            bool InsideViewspace(Vector4 vec) => vec.X >= 0 && vec.X < Width && vec.Y >= 0 && vec.Y < Height && vec.Z < 1f;

                            if (InsideViewspace(abLerp))
                            {
                                var depth = Buffer[(int)abLerp.Y, (int)abLerp.X].Depth;
                                if (depth == null || depth > abLerp.Z)
                                {
                                    Buffer[(int)abLerp.Y, (int)abLerp.X].Depth = abLerp.Z;
                                    Buffer[(int)abLerp.Y, (int)abLerp.X].Char = '█';
                                    Buffer[(int)abLerp.Y, (int)abLerp.X].Colour = d.Mesh.Colour;
                                }
                            }
                        }
                    }

                    DrawLine(pos1, pos2);
                    DrawLine(pos2, pos3);
                    DrawLine(pos1, pos3);

                    //fill
                    /*
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

                    }*/

                    //Buffer[(uint)pos1.X, (uint)pos1.Y].Char = '%';
                    //Buffer[(uint)pos2.X, (uint)pos2.Y].Char = '%';
                    //Buffer[(uint)pos3.X, (uint)pos3.Y].Char = '%';
                }
            }

            output.Append("\u001b[H");

            Color? prevColour = null;

            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    Charsel c = Buffer[i, j];
                    if (!c.Colour.Equals(prevColour))
                    {
                        output.Append($"\u001b[38;2;{c.Colour.R};{c.Colour.G};{c.Colour.B}m");
                        prevColour = c.Colour;
                    }
                    output.Append(c.Char);
                }

                if (i != height - 1)
                    output.AppendLine();
            }

            consoleWriter(output.ToString());
        }
    }
}