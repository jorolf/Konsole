using System;
using System.Diagnostics;
using Konsole.Graphics.Rendering;
using Konsole.OS;

namespace Konsole
{
    public class KonsoleWindow
    {
        public KonsoleWindow()
        {
            Console.WriteLine($"Height: {Console.WindowHeight}, Width: {Console.WindowWidth}");
            Console.WriteLine("Initializing framebuffer & loading objects");

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                Windows.EnableWindowsColour();

            var buffer = new FrameBuffer(Console.Write);

            while (true)
            {
                buffer.Width = Console.WindowWidth;
                buffer.Height = Console.WindowHeight;
                buffer.Render();
            }
        }
    }
}
