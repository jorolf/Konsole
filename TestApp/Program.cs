using System;
using System.Threading.Tasks;
using Konsole;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            var window = new KonsoleWindow(30, 30);           
            window.Start();
            Console.ReadLine();
        }

    }
}