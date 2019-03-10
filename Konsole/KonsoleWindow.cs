using System;
using Konsole.OS;

namespace Konsole
{
    public class KonsoleWindow
    {
        private FrameBuffer buffer;
        public KonsoleWindow()
        {
            Console.WriteLine($"Height: {Console.WindowHeight}, Width: {Console.WindowWidth}");
            Console.WriteLine("Initializing framebuffer & loading objects");

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                Windows.EnableWindowsColour();

            buffer = new FrameBuffer(Console.Out);

            while (true)
            {
                buffer.Width = Console.WindowWidth;
                buffer.Height = Console.WindowHeight;
                buffer.Render();
            }
        }
    }
}
