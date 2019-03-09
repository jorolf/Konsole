using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Konsole
{
    public class KonsoleWindow
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        private FrameBuffer buffer;
        public KonsoleWindow(int width, int height)
        {
            Width = width;
            Height = height;
            Console.SetWindowSize(Width, Height);
            Console.SetBufferSize(Width, Height);
            buffer = new FrameBuffer(Width, Height);
            while (true)
            {
                buffer.Render();
                Thread.Sleep(150);
                //Console.Beep(440, 5);
            }
        }
    }
}
