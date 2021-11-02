using Konsole.Clocks;
using Konsole.Extensions;
using System;
using System.Diagnostics;

namespace Konsole.Graphics.Rendering
{
    public abstract class FrameBuffer
    {
        private Charsel[,] backBuffer;
        private Charsel[,] frontBuffer;

        private readonly Action<string> consoleWriter;

        private readonly Stopwatch watch = new Stopwatch();

        private bool bufferInvalid;

        private int width;

        public int Width
        {
            get => width;
            set
            {
                if (value == width) return;

                width = value;
                bufferInvalid = true;
            }
        }

        private int height;

        public int Height
        {
            get => height;
            set
            {
                if (value == height) return;

                height = value;
                bufferInvalid = true;
            }
        }

        protected FrameBuffer(Action<string> writeAction)
        {
            bufferInvalid = true;
            consoleWriter = writeAction;
        }

        public void Render()
        {
            watch.Restart();

            if (bufferInvalid)
            {
                backBuffer = new Charsel[Height, Width];
                frontBuffer = new Charsel[Height, Width];
                frontBuffer.ClearBuffer(byte.MaxValue);
                consoleWriter("\u001b[?25l");
                ValidateBuffer();
                bufferInvalid = false;
            }

            DrawFrame(ref backBuffer);

            var renderTimeMs = watch.ElapsedMilliseconds;

            var output = KonsoleStringBuilder.CreateConsoleString(backBuffer, frontBuffer);
            consoleWriter(output);

            (frontBuffer, backBuffer) = (backBuffer, frontBuffer);
            watch.Stop();
            Debug.WriteLine($"Render time: {renderTimeMs}ms. Draw time: {watch.ElapsedMilliseconds - renderTimeMs}ms. Total time: {watch.ElapsedMilliseconds}ms FPS: {1000f / watch.ElapsedMilliseconds}. Output length: {output.Length}.");
        }

        protected abstract void DrawFrame(ref Charsel[,] buffer);

        protected abstract void ValidateBuffer();
    }
}
