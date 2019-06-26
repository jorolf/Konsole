using Konsole.Graphics.Rendering;
using Konsole.OS;
using System;

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

            buffer = new FrameBuffer(Console.Write);
        }

        public void Render()
        {
            buffer.Width = Console.WindowWidth;
            buffer.Height = Console.WindowHeight;
            buffer.Render();
        }
    }
}
