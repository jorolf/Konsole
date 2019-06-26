using Konsole;

namespace TestApp
{
    class Program
    {
        static void Main()
        {
            KonsoleWindow window = new KonsoleWindow();

            while (true)
                window.Render();
        }
    }
}