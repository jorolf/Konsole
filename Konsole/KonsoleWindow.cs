using Konsole.Graphics;
using Konsole.Vectors;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Konsole
{
    public class KonsoleWindow
    {    
        
        #region EnableWindowsColour
        private const int STD_INPUT_HANDLE = -10;

        private const int STD_OUTPUT_HANDLE = -11;

        private const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;

        private const uint DISABLE_NEWLINE_AUTO_RETURN = 0x0008;

        private const uint ENABLE_VIRTUAL_TERMINAL_INPUT = 0x0200;

        [DllImport("kernel32.dll")]
        private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        public static extern uint GetLastError();

        static void EnableWindowsColour()
        {
            var iStdOut = GetStdHandle(STD_OUTPUT_HANDLE);
            if (!GetConsoleMode(iStdOut, out uint outConsoleMode))
            {
                Console.WriteLine("failed to get output console mode");
                Console.ReadKey();
                return;
            }

            outConsoleMode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING | DISABLE_NEWLINE_AUTO_RETURN;
            if (!SetConsoleMode(iStdOut, outConsoleMode))
            {
                Console.WriteLine($"failed to set output console mode, error code: {GetLastError()}");
                Console.ReadKey();
                return;
            }
        }
        #endregion
        
        public int X;
        public int Y;
        public float RefreshRate;

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
            EnableWindowsColour();
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
