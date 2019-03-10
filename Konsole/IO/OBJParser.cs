using Konsole.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Konsole.IO
{
    public static class OBJParser
    {
        public static Triangle[] ParseFile(string path)
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
                    Positions[pIndex] = new Vector3(
                        float.Parse(temp[1], CultureInfo.InvariantCulture),
                        -float.Parse(temp[2], CultureInfo.InvariantCulture),
                        float.Parse(temp[3], CultureInfo.InvariantCulture));
                    pIndex++;
                    temp = null;
                }

                else if (t.StartsWith("f "))
                {
                    string[] Indices = t.Substring(2).Split(new char[] { ' ', '/' });
                    int[] Temp = new int[9];
                    for (int i = 0; i < Temp.Length; i++)
                    {
                        int.TryParse(Indices[i], out Temp[i]);
                        Temp[i]--;
                    }

                    Triangles[fIndex] = new Triangle(
                        new Vertex(Positions[Temp[0]], Vector3.Zero),
                        new Vertex(Positions[Temp[3]], Vector3.Zero),
                        new Vertex(Positions[Temp[6]], Vector3.Zero)
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
}
