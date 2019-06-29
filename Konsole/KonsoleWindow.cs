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

            var outputStream = Console.OpenStandardOutput();
            buffer = new RenderBuffer(s =>
            {
                var streamBuffer = Console.OutputEncoding.GetBytes(s);
                outputStream.Write(streamBuffer, 0, streamBuffer.Length);
            });
        }

        public void Render()
        {
            buffer.Width = Console.WindowWidth;
            buffer.Height = Console.WindowHeight;
            buffer.Render();
        }
    }
}
