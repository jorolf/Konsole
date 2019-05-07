using Konsole.Clocks;
using Konsole.Extensions;
using Konsole.Graphics.Colour;
using Konsole.Graphics.Drawables;
using Konsole.Graphics.Primitives;
using Konsole.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Reflection;
using System.Text;

namespace Konsole.Graphics.Rendering
{
    public class FrameBuffer
    {
        private readonly string directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar;
        private readonly StringBuilder output = new StringBuilder();

        public Charsel[,] Buffer { get; private set; }

        private readonly Action<string> consoleWriter;

        private readonly List<Drawable> drawables = new List<Drawable>();

        private Matrix4x4 viewMatrix = Matrix4x4.CreateLookAt(Vector3.Zero, new Vector3(0, 0, 1), new Vector3(0, 1, 0));
        private Matrix4x4 projectionMatrix;

        private FrameClock clock = new FrameClock();
        private Stopwatch watch = new Stopwatch();

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

            /*
            Drawable d = new Drawable
            {
                Mesh = new Mesh
                {
                    Triangles = OBJParser.ParseFile(directory + "player.obj", Properties.FlipY),
                    Colour = new Colour3(1f, 1f, 1f),
                    Texture = new Texture()
                    {
                        ColourData = new Colour3[,] { { new Colour3(1,1,1) } }
                    }
                },

                Scale = new Vector3(0.4f),
                Position = new Vector3(0,0,1f),
                Origin = new Vector3(0,1.2f,0),
                //Rotation = new Vector3(MathF.PI / 2,0,0)
            };
            */
            Drawable s = new Drawable
            {

                Meshes = OBJParser.ParseFile(directory + "normalShrek.obj", Properties.FlipY),
                Scale = new Vector3(1f),
                Position = new Vector3(0, 0, 1.3f),
                Origin = new Vector3(0, 0.4f, 0),
                Rotation = new Vector3(MathF.PI, 0, 0)
            };
            s.Meshes[1].Texture = new ImageTexture(directory + "Shrek");
            s.Meshes[0].Texture = new ImageTexture(directory + "shrekshirt");

            //drawables.Add(d);
            drawables.Add(s);

            var m = new Mesh();
            m.Triangles = new Triangle[]
            {
                new Triangle(
                    new Vertex(new Vector3(0), Vector3.Zero, new Vector2(0)),
                    new Vertex(new Vector3(0, 1, 0),  Vector3.Zero, new Vector2(0, 1)),
                    new Vertex(new Vector3(1, 0, 0), Vector3.Zero, new Vector2(1, 0))
                ),

                new Triangle(
                    new Vertex(new Vector3(0, 1, 0),  Vector3.Zero, new Vector2(0, 1)),
                    new Vertex(new Vector3(1, 0, 0), Vector3.Zero, new Vector2(1, 0)),
                    new Vertex(new Vector3(1, 1, 0), Vector3.Zero, new Vector2(1))

                )
            };
            m.Texture = new ImageTexture(directory + "testImage");


            /*
            drawables.Add(new Drawable
            {

                Meshes = new Mesh[] { m },
                Position = new Vector3(0, 0, 1),
                Origin = new Vector3(-0.5f, -0.5f, 0),
                //Rotation = new Vector3(MathF.PI, 0, 0)
            });
            */


        }

        public void Render(bool Wireframe = false)
        {
            watch.Restart();

            output.Clear();

            if (bufferInvalid)
            {
                Buffer = new Charsel[Height, Width];
                output.Append("\u001b[?25l");
                projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(1.5708f, Width / (float)Height, 0.1f, float.PositiveInfinity);
                bufferInvalid = false;
            }

            Buffer.Populate(new Charsel
            {
                Char = ' ',
                Colour = new Colour3(0, 0, 0),
                Depth = null
            });
            drawables[0].Rotation = new Vector3((float)clock.Time, 0, 0);
            foreach (Drawable d in drawables)
                foreach (Mesh m in d.Meshes)
                    foreach (Triangle t in m.Triangles)
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

                        for (var y = (int)BoundsStart.Y; y < (int)BoundsEnd.Y; y++)
                            for (var x = (int)BoundsStart.X; x < (int)BoundsEnd.X; x++)
                            {
                                //Buffer[y, x].Char = 'X';
                                //Buffer[y, x].Colour = new Colour3(1, 1, 1);
                                //Buffer[y, x].Depth = null;
                                if (x >= Width || y >= Height || x < 0 || y < 0)
                                    continue;

                                var sample = new Vector2(x + 0.5f, y + 0.5f);

                                bool CW = V4Extensions.Edge(pos1, pos2, sample) && V4Extensions.Edge(pos2, pos3, sample) && V4Extensions.Edge(pos3, pos1, sample);
                                bool CCW = V4Extensions.Edge(pos1, pos3, sample) && V4Extensions.Edge(pos3, pos2, sample) && V4Extensions.Edge(pos2, pos1, sample);

                                if (CW || CCW)
                                {
                                    var weights = V4Extensions.Barycentric(pos1, pos2, pos3, new Vector2(x + 0.5f, y + 0.5f));
                                    var depth = (pos1.Z * weights.X) + (pos2.Z * weights.Y) + (pos3.Z * weights.Z);

                                    if (Buffer[y, x].Depth == null && depth > 0f || Buffer[y, x].Depth > depth && depth > 0f)
                                    {
                                        Vector2 sampleUV = new Vector2(
                                            (t.A.UV.X / pos1.Z * weights.X * depth) + (t.B.UV.X / pos2.Z * weights.Y * depth) + (t.C.UV.X / pos3.Z * weights.Z * depth),
                                            (t.A.UV.Y / pos1.Z * weights.X * depth) + (t.B.UV.Y / pos2.Z * weights.Y * depth) + (t.C.UV.Y / pos3.Z * weights.Z * depth)
                                        );
                                        Vector3 sampleNormal = new Vector3(
                                            (t.A.Normal.X / pos1.Z * weights.X * depth) + (t.B.Normal.X / pos2.Z * weights.Y * depth) + (t.C.Normal.X / pos3.Z * weights.Z * depth),
                                            (t.A.Normal.Y / pos1.Z * weights.X * depth) + (t.B.Normal.Y / pos2.Z * weights.Y * depth) + (t.C.Normal.Y / pos3.Z * weights.Z * depth),
                                            (t.A.Normal.Z / pos1.Z * weights.X * depth) + (t.B.Normal.Z / pos2.Z * weights.Y * depth) + (t.C.Normal.Z / pos3.Z * weights.Z * depth)
                                        );
                                        int SampleX;
                                        int SampleY;

                                        //This needs to be cleaned up, it's horrible.
                                        if (sampleUV.X > 1)
                                            SampleX = (int)((sampleUV.X - (int)sampleUV.X) * (m.Texture.Width - 1));
                                        else if (sampleUV.X < -1)
                                            SampleX = (int)((sampleUV.X + (int)sampleUV.X + 1) * (m.Texture.Width - 1));
                                        else if (sampleUV.X < 0)
                                            SampleX = (int)((sampleUV.X + 1) * (m.Texture.Width - 1));
                                        else
                                            SampleX = (int)(sampleUV.X * (m.Texture.Width - 1));

                                        if (sampleUV.Y > 1)
                                            SampleY = (int)((1 - (sampleUV.Y - (int)sampleUV.Y)) * (m.Texture.Height - 1));
                                        else if (sampleUV.Y < -1)
                                            SampleY = (int)((sampleUV.Y + (int)sampleUV.X) * (m.Texture.Height - 1));
                                        else if (sampleUV.Y < 0)
                                            SampleY = (int)(Math.Abs(sampleUV.Y) * (m.Texture.Height - 1));
                                        else
                                            SampleY = (int)((1 - sampleUV.Y) * (m.Texture.Height - 1));


                                        Buffer[y, x].Char = '█';
                                        Buffer[y, x].Colour = new Colour3(1) * Math.Clamp(Vector3.Dot(sampleNormal, new Vector3(0, 1, 0.8f)), 0, 1) * m.Texture[SampleX, SampleY];
                                        Buffer[y, x].Depth = depth;
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
            var renderTime = watch.ElapsedTicks;
            var renderTimeMs = watch.ElapsedMilliseconds;
            consoleWriter(output.ToString());
            watch.Stop();
            Debug.WriteLine($"Render time: {renderTimeMs}ms. Draw time: {watch.ElapsedMilliseconds - renderTimeMs}ms. Total time: {watch.ElapsedMilliseconds}ms FPS: {1000f / watch.ElapsedMilliseconds}.");
        }
    }
}