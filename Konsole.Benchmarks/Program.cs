using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Konsole.Graphics.Colour;
using System;
using System.Numerics;
using Konsole.OS;

namespace Konsole.Benchmarks
{
    public class ColourBenchmarks
    {
        [Benchmark]
        public Vector3 VectorAddition() => new Vector3(10, 120f, 29384f) + new Vector3(-122f);
        [Benchmark]      
        public Colour3 ColourAddition() => new Colour3(10, 120f, 29384f) + new Colour3(-122f);
    }

    [RankColumn]
    public class RenderBenchmarks
    {
        private KonsoleWindow konsoleWithDiff;
        private KonsoleWindow konsoleWithoutDiff;

        private const int WindowWidth = 500, WindowHeight = 500;

        [GlobalSetup]
        public void GlobalSetup() => Windows.CreateConsole();

        [GlobalCleanup]
        public void GlobalCleanup() => Windows.FreeConsole();

        [IterationSetup(Target = nameof(WithDiff))]
        public void SetupWithDiff() => konsoleWithDiff = new KonsoleWindow(() => (WindowWidth, WindowHeight), true);

        [Benchmark]
        public void WithDiff() => konsoleWithDiff.Render();

        [IterationSetup(Target = nameof(WithoutDiff))]
        public void SetupWithoutDiff() => konsoleWithoutDiff = new KonsoleWindow(() => (WindowWidth, WindowHeight), false);

        [Benchmark]
        public void WithoutDiff() => konsoleWithoutDiff.Render();
    }


    class Program
    {
        static void Main(string[] args)
        {
            var results = BenchmarkRunner.Run<RenderBenchmarks>();
            Console.ReadLine();
        }
    }
}
