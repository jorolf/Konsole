using Konsole.Clocks;
using Konsole.Extensions;
using Konsole.Graphics.Colour;
using Konsole.Graphics.Drawables;
using Konsole.Graphics.Primitives;
using Konsole.IO;
using System;
using System.Collections.Generic;
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
                    Colour = new Colour3(1f, 1f, 1f)
                },
                Scale = new Vector3(0.4f),
                Position = new Vector3(0,0,1f),
                Origin = new Vector3(0,1.2f,0),
                Rotation = new Vector3(MathF.PI / 2,0,0)
            };
            Drawable s = new Drawable
            {
                Mesh = new Mesh
                {
                    Triangles = OBJParser.ParseFile(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar + "shrek.obj", Properties.FlipY),
                    Colour = Colour3.FromBytes(123, 186, 46)
                },
                Scale = new Vector3(0.66f),
                Position = new Vector3(0, 0, 1f),
                Origin = new Vector3(0, 0.4f, 0),
            };
            //drawables.Add(d);
            drawables.Add(s);
        }

        public void Render(bool Wireframe = false)
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
                Colour = new Colour3(0, 0, 0),
                Depth = null
            });

            drawables[0].Rotation = new Vector3((float)clock.Time * 2, 0, 0);
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

                    pos1 = pos1.ToPixel(Width, Height);
                    pos2 = pos2.ToPixel(Width, Height);
                    pos3 = pos3.ToPixel(Width, Height);

                    V4Extensions.Bounds(pos1, pos2, pos3, out Vector2 BoundsStart, out Vector2 BoundsEnd);

                    for (int y = (int)BoundsStart.Y; y < (int)BoundsEnd.Y; y++)
                        for (int x = (int)BoundsStart.X; x < (int)BoundsEnd.X; x++)
                        {
                            var sample = new Vector2(x + 0.5f, y + 0.5f);
                            if (V4Extensions.Edge(pos1, pos2, sample) &&
                                V4Extensions.Edge(pos2, pos3, sample) &&
                                V4Extensions.Edge(pos3, pos1, sample))
                            {
                                var weights = V4Extensions.Barycentric(pos1, pos2, pos3, new Vector2(x + 0.5f, y + 0.5f));
                                var depth = (pos1.Z * weights.X) + (pos2.Z * weights.Y) + (pos3.Z * weights.Z);

                                if (Buffer[y, x].Depth == null || Buffer[y, x].Depth > depth)
                                {
                                    Buffer[y, x].Char = '█';
                                    Buffer[y, x].Colour = d.Mesh.Colour;
                                    Buffer[y, x].Depth = depth;
                                }
                            }
                        }
                }
            }

            output.Append("\u001b[H");

            Colour3? prevColour = null;

            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    Charsel c = Buffer[i, j];
                    if (!c.Colour.Equals(prevColour))
                    {
                        output.Append($"\u001b[38;2;{c.Colour.R.ToByte()};{c.Colour.G.ToByte()};{c.Colour.B.ToByte()}m");
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