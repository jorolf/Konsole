﻿using System;
using System.Threading.Tasks;
using Konsole;
using Konsole.Graphics.Colour;

namespace TestApp
{
    class Program
    {
        static void Main()
        {
            var window = new KonsoleWindow(50, 50);
            window.Start();            
            Console.ReadLine();
        }

    }
}