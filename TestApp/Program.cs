using System;
using System.Threading.Tasks;
using System.Drawing;
using Konsole;
using Konsole.Graphics.Colour;
using Konsole.Vectors;

namespace TestApp
{
    class Program
    {
        static void Main()
        {
            var window = new KonsoleWindow((Vector2<int>)30);
            window.buffer.Drawables.Add(new TestContainer());
            window.Start();            
            Console.ReadLine();
        }

    }
}