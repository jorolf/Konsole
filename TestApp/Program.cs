using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Konsole;
using Konsole.Extensions;
using Konsole.Graphics.Colour;
using Konsole.IO;

namespace TestApp
{
    class Program
    {
        static void Main()
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar;

            KonsoleWindow window = new KonsoleWindow();
        }
    }
}