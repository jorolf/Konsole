using System;
using System.Threading.Tasks;
using System.Drawing;
using Konsole;
using Konsole.Graphics.Colour;

namespace TestApp
{
    class Program
    {
        static void Main()
        {
            var window = new KonsoleWindow(50, 50);
            window.buffer.Drawables.Add(new TestContainer());
            window.Start();            
            Console.ReadLine();
        }

    }
}