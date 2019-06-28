using Konsole.Graphics.Rendering;
using Konsole.OS;
using System;

namespace Konsole
{
    public class KonsoleWindow
    {
        private readonly Func<(int, int)> consoleSize;
        private FrameBuffer buffer;

        public KonsoleWindow(Func<(int, int)> consoleSize, bool diff = true)
        {
            this.consoleSize = consoleSize;
            var size = consoleSize();
            Console.WriteLine($"Height: {size.Item2}, Width: {size.Item1}");
            Console.WriteLine("Initializing framebuffer & loading objects");

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                Windows.EnableWindowsColour();

            buffer = new FrameBuffer(Console.Write, diff);
        }

        public void Render()
        {
            (buffer.Width, buffer.Height) = consoleSize();
            buffer.Render();
        }
    }
}
