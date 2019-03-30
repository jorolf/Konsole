using System;
using System.IO;
using System.Reflection;
using Konsole;
using Konsole.IO;

namespace TestApp
{
    class Program
    {
        static void Main()
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar;
            //KonsoleWindow window = new KonsoleWindow();
            //PNGDecoder.Parse(path + "player.obj");
            PNGDecoder.Parse(path + "testImage.png");
            PNGDecoder.Parse(path + "Astolfo.png");
            Console.ReadLine();
        }
    }
}