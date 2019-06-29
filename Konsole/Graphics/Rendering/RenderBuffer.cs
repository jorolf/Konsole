using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Konsole.Clocks;
using Konsole.Extensions;
using Konsole.Graphics.Colour;
using Konsole.Graphics.Drawables;
using Konsole.Graphics.Primitives;
using Konsole.IO;
using static Konsole.Globals;

namespace Konsole.Graphics.Rendering
{
    public class RenderBuffer : FrameBuffer
    {

        private readonly List<Drawable> drawables = new List<Drawable>();

        private Matrix4x4 viewMatrix = Matrix4x4.CreateLookAt(Vector3.Zero, new Vector3(0, 0, 1), new Vector3(0, 1, 0));
        private Matrix4x4 projectionMatrix;
        private Matrix4x4 pixelMatrix;
        private FrameClock clock = new FrameClock();

        public RenderBuffer(Action<string> writeAction) : base(writeAction)
        {
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
                Meshes = OBJParser.ParseFile(GameDirectory + "Assets/Core/normalShrek.obj", Properties.FlipY),
                Scale = new Vector3(1.05f),
                Position = new Vector3(0, 0, 1f),
                Origin = new Vector3(0, 0.5f, 0),
                Rotation = new Vector3((float)Math.PI, 0, 0)
            };
            s.Meshes[1].Texture = new ImageTexture(GameDirectory + "Assets/Core/Shrek");
            s.Meshes[0].Texture = new ImageTexture(GameDirectory + "Assets/Core/shrekshirt");

            //drawables.Add(d);
            drawables.Add(s);

            var m = new Mesh();
            m.Triangles = new[]
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
            //m.Texture = new ImageTexture(GameDirectory + "testImage");

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

        protected override void DrawFrame(ref Charsel[,] buffer)
        {
            float Clamp(float value, float min, float max) => Math.Min(max, Math.Max(min, value));

            buffer.ClearBuffer();
            drawables[0].Rotation = new Vector3((float)clock.Time, 0, 0);
            foreach (Drawable d in drawables)
            {
                var matrix = d.DrawableMatrix;
                matrix *= viewMatrix;
                matrix *= projectionMatrix;
                matrix *= pixelMatrix;

                foreach (Mesh m in d.Meshes)
                    foreach (Triangle t in m.Triangles)
                    {
                        Vector4 pos1 = new Vector4(t.A.Position, 1);
                        Vector4 pos2 = new Vector4(t.B.Position, 1);
                        Vector4 pos3 = new Vector4(t.C.Position, 1);

                        pos1 = Vector4.Transform(pos1, matrix);
                        pos2 = Vector4.Transform(pos2, matrix);
                        pos3 = Vector4.Transform(pos3, matrix);

                        pos1 /= pos1.W;
                        pos2 /= pos2.W;
                        pos3 /= pos3.W;

                        V4Extensions.Bounds(pos1, pos2, pos3, out Vector2 BoundsStart, out Vector2 BoundsEnd);

                        for (var y = (int)BoundsStart.Y; y < (int)BoundsEnd.Y; y++)
                            for (var x = (int)BoundsStart.X; x < (int)BoundsEnd.X; x++)
                            {
                                if (x >= Width || y >= Height || x < 0 || y < 0)
                                    continue;

                                var sample = new Vector2(x + 0.5f, y + 0.5f);

                                bool CW = V4Extensions.Edge(pos1, pos2, sample) && V4Extensions.Edge(pos2, pos3, sample) && V4Extensions.Edge(pos3, pos1, sample);
                                bool CCW = V4Extensions.Edge(pos1, pos3, sample) && V4Extensions.Edge(pos3, pos2, sample) && V4Extensions.Edge(pos2, pos1, sample);

                                if (CW || CCW)
                                {
                                    var weights = V4Extensions.Barycentric(pos1, pos2, pos3, new Vector2(x + 0.5f, y + 0.5f));
                                    var depth = (pos1.Z * weights.X) + (pos2.Z * weights.Y) + (pos3.Z * weights.Z);

                                    if (buffer[y, x].Depth == null && depth > 0f || buffer[y, x].Depth > depth && depth > 0f)
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

                                        SampleX = (int)((sampleUV.X % 1 + 1) % 1 * (m.Texture.Width - 1));
                                        SampleY = (int)((-sampleUV.Y % 1 + 1) % 1 * (m.Texture.Height - 1));

                                        buffer[y, x].Char = '█';
                                        buffer[y, x].Colour = new Colour3(1) * Clamp(Vector3.Dot(sampleNormal, new Vector3(0, 1, 0.8f)), 0, 1) * m.Texture[SampleX, SampleY];
                                        buffer[y, x].Depth = depth;
                                    }
                                }
                            }
                    }
            }
        }

        protected override void ValidateBuffer()
        {
            projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(1.5708f, Width / (float)Height, 0.1f, float.PositiveInfinity);
            pixelMatrix = Matrix4x4.CreateScale(Width / 2f, Height / 2f, 1) * Matrix4x4.CreateTranslation(Width / 2f, Height / 2f, 0);
        }
    }
}
