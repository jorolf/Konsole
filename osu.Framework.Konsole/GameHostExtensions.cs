using System;
using System.Threading.Tasks;
using Konsole.Graphics.Colour;
using Konsole.Graphics.Rendering;
using Konsole.OS;
using osu.Framework.Platform;
using osuTK.Graphics.ES30;

namespace osu.Framework.Konsole
{
    public static class GameHostExtensions
    {
        public static void HookKonsole(this GameHost host)
        {
            void HookKonsole()
            {
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    Windows.EnableWindowsColour();

                var outputStream = Console.OpenStandardOutput();
                var buffer = new FrameworkFrameBuffer(s =>
                {
                    var streamBuffer = Console.OutputEncoding.GetBytes(s);
                    outputStream.Write(streamBuffer, 0, streamBuffer.Length);
                }, host.Window);

                void UpdateFrame()
                {
                    buffer.ReadPixels();

                    Task.Run(() =>
                    {
                        buffer.Width = Console.WindowWidth;
                        buffer.Height = Console.WindowHeight;
                        buffer.Render();

                        host.DrawThread.Scheduler.Add(UpdateFrame);
                    });
                }

                host.DrawThread.Scheduler.Add(UpdateFrame);
                //host.Activated += HookKonsole;
            }

            host.Activated += HookKonsole;
        }

        private class FrameworkFrameBuffer : FrameBuffer
        {
            private readonly IWindow window;
            private uint[,] screenBuffer = new uint[0, 0];
            

            public FrameworkFrameBuffer(Action<string> writeAction, IWindow window) : base(writeAction)
            {
                this.window = window;
            }

            public void ReadPixels()
            {
                if (screenBuffer.Length != window.ClientSize.Width * window.ClientSize.Height)
                    screenBuffer = new uint[window.ClientSize.Height, window.ClientSize.Width];

                GL.ReadPixels(0, 0, window.ClientSize.Width, window.ClientSize.Height, PixelFormat.Rgba, PixelType.UnsignedInt8888, screenBuffer);
            }

            protected override void DrawFrame(ref Charsel[,] buffer)
            {
                float xStep = window.ClientSize.Width / (float)Width;
                float yStep = window.ClientSize.Height / (float)Height;

                //int xStepHalf = (int)Math.Round(xStep / 2), yStepHalf = (int)Math.Round(yStep / 2);
                int screenHeight = window.ClientSize.Height;

                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        buffer[y, x].Char = '█';

                        var locY = (int)Math.Round((y - 0.5f) * yStep);
                        var locX = (int)Math.Round((x - 0.5f) * xStep);
                        if (locX < 1) locX += (int)xStep;
                        if (locY < 1) locY += (int)yStep;

                        Colour3 topLeft = screenBuffer[screenHeight - locY - (int)yStep, locX] >> 8;
                        Colour3 topRight = screenBuffer[screenHeight - locY - (int)yStep, locX + (int)xStep] >> 8;
                        Colour3 bottomLeft = screenBuffer[screenHeight - locY, locX] >> 8;
                        Colour3 bottomRight = screenBuffer[screenHeight - locY, locX + (int)xStep] >> 8;

                        var top = Colour3.Lerp(topLeft, topRight, (x * xStep - locX) / xStep);
                        var bottom = Colour3.Lerp(bottomLeft, bottomRight, (x * xStep - locX) / xStep);

                        buffer[y, x].Colour = Colour3.Lerp(top, bottom, (y * yStep - locY) / yStep);
                    }
                }
            }

            protected override void ValidateBuffer()
            {
                
            }
        }
    }
}
