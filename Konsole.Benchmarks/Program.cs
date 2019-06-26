using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Konsole.Graphics.Colour;
using System;
using System.Numerics;

namespace Konsole.Benchmarks
{
    public class ColourBenchmarks
    {
        [Benchmark]
        public Vector3 VectorAddition() => new Vector3(10, 120f, 29384f) + new Vector3(-122f);
        [Benchmark]      
        public Colour3 ColourAddition() => new Colour3(10, 120f, 29384f) + new Colour3(-122f);
    }




    class Program
    {
        static void Main(string[] args)
        {
            var results = BenchmarkRunner.Run<ColourBenchmarks>();
            Console.ReadLine();
        }
    }
}
