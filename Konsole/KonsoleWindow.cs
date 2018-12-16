using Konsole.Graphics;
using Konsole.Vectors;
using System;
using System.Threading.Tasks;

namespace Konsole
{
    public class KonsoleWindow
    {
        public int X;
        public int Y;
        public float RefreshRate;

        public KonsoleWindow(int x, int y, float refreshRate, bool square = false)
        {
            if (square)
                X = x * 2;
            else
                X = x;
            Y = y;
            RefreshRate = refreshRate;
            setSize();
        }
        public KonsoleWindow(int x, int y, bool square = false)
        {
            if (square)
                X = x * 2;
            else
                X = x;
            Y = y;
            RefreshRate = 60;
            setSize();
        }
        public KonsoleWindow(int size, bool square = false)
        {
            if (square)
                X = size * 2;
            else
                X = size;
            Y = size;
            RefreshRate = 60;
            setSize();
        }
        private void setSize()
        {
            if (Console.LargestWindowWidth < X || Console.LargestWindowHeight < Y)
            {            

            }
            Retry:

            try
            {
               
                Console.SetWindowSize(X, Y);
                Console.SetBufferSize(X, Y);
            } 
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("The current game requires the screen to be larger or the font to be smaller, please change the font size and try again by inputting \"Retry\" into the console█");
                ReadAgain:
                string input = Console.ReadLine();
                if (input == "Retry")
                    goto Retry;
                else
                    goto ReadAgain;
                
                
            }           
        }
        public void Start()
        {
            var buffer = new KonsoleBuffer(new Vector2<int>(X, Y));
            buffer.RenderBuffer();
            buffer.PushToConsole();
        }

        private Task Update()
        {
            Task.Delay((int)(1000 / RefreshRate));
            setSize();
            return Task.CompletedTask;
        }

    }
}
