﻿using Konsole.Extensions;
using Konsole.Graphics.Drawables;
using Konsole.Graphics.Primitives;
using Konsole.IO;
using System;
using System.Collections.Generic;
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
                    Triangles = OBJParser.ParseFile(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar + "player.obj", Properties.FlipY)
                },
                Scale = new Vector3(0.5f, 0.33f, 0.33f),
                Position = new Vector3(0,0.5f,1)
            };
            drawables.Add(d);
        }

        private float time;

        public void Render()
        {
            output.Clear();
            time += 0.1f;

            if (bufferInvalid)
            {
                Buffer = new Charsel[Height, Width];
                output.Append("\u001b[?25l");
                projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(1.5708f, width / height, 1, float.PositiveInfinity);
                bufferInvalid = false;
            }

            Buffer.Populate(new Charsel
            {
                Char = ' ',
                Colour = Color.White,
            });

            foreach (Drawable d in drawables)
            {
                d.Rotation = new Vector3(time, 0, 0);
                foreach (Triangle t in d.Mesh.Triangles)
                {
                    var pos1 = t.A.Position;
                    var pos2 = t.B.Position;
                    var pos3 = t.C.Position;

                    var m = d.DrawableMatrix;

                    m *= viewMatrix;
                    m *= projectionMatrix;

                    pos1 = Vector3.Transform(pos1, m);
                    pos2 = Vector3.Transform(pos2, m);
                    pos3 = Vector3.Transform(pos3, m);

                    pos1.X = pos1.X.Remap(-1, 1, 0, Width);
                    pos2.X = pos2.X.Remap(-1, 1, 0, Width);
                    pos3.X = pos3.X.Remap(-1, 1, 0, Width);
                    pos1.Y = pos1.Y.Remap(-1, 1, 0, Height);
                    pos2.Y = pos2.Y.Remap(-1, 1, 0, Height);
                    pos3.Y = pos3.Y.Remap(-1, 1, 0, Height);

                    const int k = 200;
                    var ab = pos2 - pos1;
                    var bc = pos3 - pos2;
                    var ac = pos3 - pos1;

                    //perimeter
                    for (int i = 0; i < k; i++)
                    {
                        var y = pos1.Y + (ab.Y * i / k);
                        var x = pos1.X + (ab.X * i / k);
                        if (x >= 0 && x < Width && y > 0 && y < Height)
                            Buffer[(int)y, (int)x].Char = '█';
                        y = pos2.Y + (bc.Y * i / k);
                        x = pos2.X + (bc.X * i / k);
                        if (x >= 0 && x < Width && y > 0 && y < Height)
                            Buffer[(int)y, (int)x].Char = '█';
                        y = pos1.Y + (ac.Y * i / k);
                        x = pos1.X + (ac.X * i / k);
                        if (x >= 0 && x < Width && y > 0 && y < Height)
                            Buffer[(int)y, (int)x].Char = '█';
                    }

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

