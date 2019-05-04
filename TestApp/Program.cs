using System;
using System.IO;
using System.Reflection;
using Konsole;
using Konsole.Extensions;
using Konsole.IO;

namespace TestApp
{
    class Program
    {
        static void Main()
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar;

            KonsoleWindow window = new KonsoleWindow();
            Console.ReadLine();
        }
    }
}