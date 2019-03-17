using Konsole.Graphics.Primitives;
using System.Globalization;
using System.IO;
using System.Numerics;

namespace Konsole.IO
{
    public static class OBJParser
    {
        public static Triangle[] ParseFile(string path, Properties properties = Properties.Default)
        {
            //Stopwatch watch = new Stopwatch();
            //watch.Start();
            int pIndex = 0;
            int nIndex = 0;
            int fIndex = 0;
            StreamReader file = new StreamReader(path);
            var read = true;
            while (read)
            {
                var t = file.ReadLine();
                if (t == null)
                    read = false;
                else if (t.StartsWith("v "))
                    pIndex++;
                else if (t.StartsWith("vn"))
                    nIndex++;
                else if (t.StartsWith("f"))
                    fIndex++;

            }

            Vector3[] Positions = new Vector3[pIndex];
            Vector3[] Normals = new Vector3[nIndex];
            Triangle[] Triangles = new Triangle[fIndex];
            //Console.WriteLine($"Counting has finished in {watch.ElapsedMilliseconds}ms.");
            pIndex = 0;
            nIndex = 0;
            fIndex = 0;
            file.BaseStream.Position = 0;
            read = true;
            while (read)
            {
                var t = file.ReadLine();
                if (t == null)
                    read = false;
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
                            new Vertex(Positions[Temp[0]], Vector3.Zero, Vector2.Zero),
                            new Vertex(Positions[Temp[3]], Vector3.Zero, Vector2.Zero),
                            new Vertex(Positions[Temp[6]], Vector3.Zero, Vector2.Zero)
                        );
                    else
                        Triangles[fIndex] = new Triangle(
                            new Vertex(Positions[Temp[0]], Vector3.Zero, Vector2.Zero),
                            new Vertex(Positions[Temp[2]], Vector3.Zero, Vector2.Zero),
                            new Vertex(Positions[Temp[4]], Vector3.Zero, Vector2.Zero)
                        );
                    fIndex++;
                    Temp = null;
                }

            }
            file.Close();
            //watch.Stop();
            //Console.WriteLine($"Parsing has finished in {watch.ElapsedMilliseconds}");

            return Triangles;
        }
    }
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
