using System;
using System.Threading.Tasks;
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
                Task task = null;
                var buffer = new FrameworkFrameBuffer(s =>
                {
                    var streamBuffer = Console.OutputEncoding.GetBytes(s);
                    outputStream.Write(streamBuffer, 0, streamBuffer.Length);
                }, host.Window);

                void UpdateFrame()
                {
                    buffer.ReadPixels();

                    task = Task.Run(() =>
                    {
                        buffer.Width = Console.WindowWidth;
                        buffer.Height = Console.WindowHeight;
                        buffer.Render();

                        host.DrawThread.Scheduler.Add(UpdateFrame);
                    });
                }

                host.DrawThread.Scheduler.Add(UpdateFrame);
                host.Activated += HookKonsole;
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
                if (screenBuffer.Length != window.Width * window.Height)
                    screenBuffer = new uint[window.Height, window.Width];

                GL.ReadPixels(0, 0, window.Width, window.Height, PixelFormat.Rgba, PixelType.UnsignedInt8888, screenBuffer);
            }

            protected override void DrawFrame(ref Charsel[,] buffer)
            {
                float xStep = window.Width / (float)Width;
                float yStep = window.Height / (float)Height;
                int screenHeight = window.Height;

                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        buffer[y, x].Char = '█';
                        buffer[y, x].Colour = screenBuffer[(int) (screenHeight - y * yStep - 1), (int) (x * xStep)] >> 8;
                    }
                }
            }

            protected override void ValidateBuffer()
            {
                
            }
        }
    }
}
