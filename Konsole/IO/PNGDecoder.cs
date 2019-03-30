using Konsole.Extensions;
using Konsole.Graphics.Colour;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Konsole.IO
{
    public static class PNGDecoder
    {
        public static void Parse(string path)
        {
            var file = File.OpenRead(path);
            var compare = new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 };
            var signature = new byte[8];
            file.Read(signature, 0, 8);
            if (signature.SequenceEqual(compare))
            {
                var read = true;
                int counter = 1;
                Console.WriteLine("This file is a PNG! Decoding...");
                while (read)
                {
                    var length = new byte[4];

                    var end = file.Read(length, 0, 4);
                    if (end == 0)
                        read = false;
                    counter++;
                    var chunk = new Chunk(length.ToUint());

                    file.Read(chunk.Type, 0, 4);
                    if (chunk.Type.ToUint() == ChunkType.Start)
                    {
                        var width = new byte[4];
                        var height = new byte[4];
                        file.Read(width, 0, 4);
                        file.Read(height, 0, 4);
                        Console.WriteLine($"Start chunk found! The image dimentions are {width.ToUint()}x{height.ToUint()}");
                        file.Read(new byte[5], 0, 5);
                    }
                    else if (chunk.Type.ToUint() == ChunkType.End)
                    {
                        Console.WriteLine("End chunk found! Stopping the read...");
                        read = false;
                    }
                    else if (chunk.Type.ToUint() == ChunkType.Data)
                    {
                        Console.WriteLine("Data chunk found! Decompressing...");
                        byte[] compFlags = new byte[1];
                        file.Read(compFlags, 0, 1);

                        //Temporary so that it doesn't crash.
                        file.Read(chunk.Data, 0, (int)chunk.Length - 1);

                        if ((compFlags[0] & CompressionMethod.Fastest) == CompressionMethod.Fastest)
                            Console.WriteLine("This Data Chunk is using the Fastest compression!");
                        if ((compFlags[0] & CompressionMethod.Fast) == CompressionMethod.Fast)
                            Console.WriteLine("This Data Chunk is using the Fast compression!");
                        if ((compFlags[0] & CompressionMethod.Medium) == CompressionMethod.Medium)
                            Console.WriteLine("This Data Chunk is using the Medium compression!");
                        if ((compFlags[0] & CompressionMethod.Slow) == CompressionMethod.Slow)
                            Console.WriteLine("This Data Chunk is using the Slow compression!");

                    }
                    else
                        file.Read(chunk.Data, 0, (int)chunk.Length);
                    file.Read(new byte[4], 0, 4);

                    var converted = Encoding.Convert(Encoding.ASCII, Encoding.UTF8, chunk.Type);
                    if (read) Console.WriteLine($"Chunk with the Type of {Encoding.UTF8.GetString(converted)} and the length of {chunk.Length}");
                }
            }
            else
                Console.WriteLine("This file is NOT a PNG!");
            file.Close();
        }

        private struct Chunk
        {
            /// <summary>
            /// Used for checking the <see cref="Type"/>s
            /// </summary>
            const byte typeTest = 32;

            public uint Length;
            public byte[] Type;
            public byte[] Data;
            public byte[] CRC;

            public Chunk(uint length)
            {
                Length = length;
                Data = new byte[Length];

                Type = new byte[4];
                CRC = new byte[4];
            }
        }

        private static class ChunkType
        {
            public const uint Start = 1229472850;
            public const uint Data = 1229209940;
            public const uint End = 1229278788;
        }

        private static class CompressionMethod
        {
            public const byte Fastest = 0b10000000;
            public const byte Fast = 0b01000000;
            public const byte Medium = 0b00100000;
            public const byte Slow = 0b00010000;

        }
    }
}