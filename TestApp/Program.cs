using System;
using Konsole;

namespace TestApp
{
    class Program
    {
        static void Main()
        {
            KonsoleWindow window = new KonsoleWindow(() => (Console.WindowWidth, Console.WindowHeight), false);

            while (true)
                window.Render();
        }
    }
}