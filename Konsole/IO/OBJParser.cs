using Konsole.Graphics.Drawables;
using Konsole.Graphics.Primitives;
using System;
using System.Globalization;
using System.IO;
using System.Numerics;

namespace Konsole.IO
{
    public static class OBJParser
    {
        public static Mesh[] ParseFile(string path, Properties properties = Properties.Default)
        {
            //Stopwatch watch = new Stopwatch();
            //watch.Start();
            int pIndex = 0;
            int nIndex = 0;
            int uvIndex = 0;
            int fIndex = 0;
            int meshIndex = 0;
            StreamReader file = new StreamReader(path);
            var read = true;
            while (read)
            {
                var t = file.ReadLine();
                if (t == null)
                    read = false;
                else if (t.StartsWith("v "))
                    pIndex++;
                else if (t.StartsWith("s"))
                    meshIndex++;
                else if (t.StartsWith("vn"))
                    nIndex++;
                else if (t.StartsWith("vt"))
                    uvIndex++;
                else if (t.StartsWith("f"))
                    fIndex++;

            }

            Vector3[] Positions = new Vector3[pIndex];
            Vector3[] Normals = new Vector3[nIndex];
            Vector2[] UVs = new Vector2[uvIndex];
            Triangle[] Triangles = new Triangle[fIndex];
            Mesh[] Meshes = new Mesh[meshIndex];
            //Console.WriteLine($"Counting has finished in {watch.ElapsedMilliseconds}ms.");
            pIndex = 0;
            nIndex = 0;
            fIndex = 0;
            uvIndex = 0;
            meshIndex = -1;
            var previousMesh = 0;
            file.BaseStream.Position = 0;
            read = true;
            while (read)
            {

                void meshTriangles()
                {
                    Span<Triangle> tris = new Span<Triangle>(Triangles).Slice(previousMesh, fIndex - previousMesh);
                    Meshes[meshIndex] = new Mesh()
                    {
                        Triangles = tris.ToArray()
                    };
                    previousMesh = fIndex;
                }
                var t = file.ReadLine();
                if (t == null)
                {
                    read = false;
                    meshTriangles();
                }
                else if (t.StartsWith("s"))
                {
                    if (meshIndex != -1)
                        meshTriangles();
                    meshIndex++;
                }
                else if (t.StartsWith("vn"))
                {
                    string[] temp = t.Split(' ');
                    Normals[nIndex] = new Vector3(
                        float.Parse(temp[1], CultureInfo.InvariantCulture),
                        float.Parse(temp[2], CultureInfo.InvariantCulture),
                        float.Parse(temp[3], CultureInfo.InvariantCulture));
                    nIndex++;
                    temp = null;
                }
                else if (t.StartsWith("vt"))
                {
                    string[] temp = t.Split(' ');
                    UVs[uvIndex] = new Vector2(
                        float.Parse(temp[1], CultureInfo.InvariantCulture),
                        float.Parse(temp[2], CultureInfo.InvariantCulture));
                    uvIndex++;
                    temp = null;
                }
                else if (t.StartsWith("v "))
                {
                    string[] temp = t.Split(' ');
                    float X;
                    float Y;
                    float Z;
                    if ((properties & Properties.FlipX) == Properties.FlipX)
                        X = -float.Parse(temp[1], CultureInfo.InvariantCulture);
                    else
                        X = float.Parse(temp[1], CultureInfo.InvariantCulture);
                    if ((properties & Properties.FlipY) == Properties.FlipY)
                        Y = -float.Parse(temp[2], CultureInfo.InvariantCulture);
                    else
                        Y = float.Parse(temp[2], CultureInfo.InvariantCulture);
                    if ((properties & Properties.FlipZ) == Properties.FlipZ)
                        Z = -float.Parse(temp[3], CultureInfo.InvariantCulture);
                    else
                        Z = float.Parse(temp[3], CultureInfo.InvariantCulture);
                    Positions[pIndex] = new Vector3(X, Y, Z);
                    pIndex++;
                    temp = null;
                }

                else if (t.StartsWith("f "))
                {
                    string[] Indices = t.Substring(2).Split(new char[] { ' ', '/' });
                    int[] Temp = new int[Indices.Length];
                    for (int i = 0; i < Temp.Length; i++)
                    {
                        int.TryParse(Indices[i], out Temp[i]);
                        Temp[i]--;
                    }

                    if (Temp.Length == 9)
                        Triangles[fIndex] = new Triangle(
                            new Vertex(Positions[Temp[0]], Normals[Temp[2]], UVs[Temp[1]]),
                            new Vertex(Positions[Temp[3]], Normals[Temp[5]], UVs[Temp[4]]),
                            new Vertex(Positions[Temp[6]], Normals[Temp[8]], UVs[Temp[7]])
                        );
                    else
                        Triangles[fIndex] = new Triangle(
                            new Vertex(Positions[Temp[0]], Vector3.Zero, UVs[Temp[1]]),
                            new Vertex(Positions[Temp[2]], Vector3.Zero, UVs[Temp[3]]),
                            new Vertex(Positions[Temp[4]], Vector3.Zero, UVs[Temp[5]])
                        );
                    if (fIndex != Triangles.Length - 1)
                        fIndex++;
                    Temp = null;
                }

            }
            file.Close();
            //watch.Stop();
            //Console.WriteLine($"Parsing has finished in {watch.ElapsedMilliseconds}");

            return Meshes;
        }
    }
    [Flags]
    public enum Properties
    {
        Default = 0,
        FlipX = 0b100,
        FlipY = 0b010,
        FlipZ = 0b001,
        FlipXY = 0b110,
        FlipXZ = 0b101,
        FlipYZ = 0b011,
        FLipXYZ = 0b111,
    }
}
