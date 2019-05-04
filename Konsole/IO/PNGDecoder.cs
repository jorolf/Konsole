using Konsole.Extensions;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Konsole.IO
{
    /// <summary>
    /// Don't use it because it's not finished nor do I plan on finishing it right now.
    /// </summary>
    [Obsolete]
    public static class PNGDecoder
    {
        public static void Parse(string path)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            bool Read = false;
            var file = File.OpenRead(path);
            Span<byte> compare = stackalloc byte[] { 137, 80, 78, 71, 13, 10, 26, 10 };
            Span<byte> fileHeader = stackalloc byte[8];
            file.Read(fileHeader);
            if (compare.ReverseCast<byte, long>()[0] == fileHeader.ReverseCast<byte, long>()[0])
                Read = true;
            while (Read)
            {

                Span<byte> byteChunkLength = stackalloc byte[4];
                file.Read(byteChunkLength);
                Span<uint> chunkLength = byteChunkLength.ReverseCast<byte, uint>();

                Span<byte> byteChunkType = stackalloc byte[4];
                file.Read(byteChunkType);
                var chunkType = byteChunkType.Cast<byte, uint>();

                Console.WriteLine($"Chunk found! Type: {Encoding.ASCII.GetString(byteChunkType)}, Length: {chunkLength[0]}");
                Span<byte> bitDepth = stackalloc byte[1];
                Span<byte> colourType = stackalloc byte[1];
                Span<byte> interlacing = stackalloc byte[1];
                switch (chunkLength[0])
                {
                    case 0:
                        switch (chunkType[0])
                        {
                            case ChunkTypes.End:
                                Read = false;
                                break;
                        }
                        break;
                    default:
                        Span<byte> byteChunkData = stackalloc byte[(int)chunkLength[0]];
                        file.Read(byteChunkData);
                        int pos;
                        switch (chunkType[0])
                        {
                            case ChunkTypes.Start:
                                pos = 0;
                                Span<uint> width = byteChunkData.Slice(pos, 4).ReverseCast<byte, uint>();
                                pos += 4;

                                Span<uint> height = byteChunkData.Slice(pos, 4).ReverseCast<byte, uint>();
                                pos += 4;
                                Console.WriteLine($"Picture Dimensions:{width[0]}x{height[0]}px");
                                bitDepth[0] = byteChunkData[pos++];
                                colourType[0] = byteChunkData[pos++];
                                pos += 2;
                                interlacing[0] = byteChunkData[pos++];

                                Console.WriteLine(pos);
                                break;
                            case ChunkTypes.Data:
                                pos = 0;
                                var compMethod = byteChunkData.Slice(pos, 1)[0] | ZlibHeader.CM;
                                var compInfo = (byteChunkData.Slice(pos, 1)[0] | ZlibHeader.CI) >> 4;
                                pos++;
                                var fCheck = byteChunkData.Slice(pos, 1)[0] | 0b00001111;
                                var fDict = (byteChunkData.Slice(pos, 1)[0] | 0b00010000) >> 4;
                                var compLevel = (byteChunkData.Slice(pos, 1)[0] | 0b11100000) >> 5;
                                pos++;
                                if (fDict == 1)
                                {
                                    var dictId = byteChunkData.Slice(pos, 4).Cast<byte, uint>();
                                    pos += 4;
                                }

                                break;
                        }
                        break;

                }
                file.Position += 4;
            }
            watch.Stop();
            Console.WriteLine($"Parsing took {watch.ElapsedTicks / 1000}kTicks, {watch.ElapsedMilliseconds}ms");

        }

        private static class ChunkTypes
        {
            public const uint Start = 73 | (72 << (1 * 8)) | (68 << (2 * 8)) | (82 << (3 * 8));
            public const uint Palette = 80 | (76 << (1 * 8)) | (84 << (2 * 8)) | (69 << (3 * 8));
            public const uint Data = 73 | (68 << (1 * 8)) | (65 << (2 * 8)) | (84 << (3 * 8));
            public const uint End = 73 | (69 << (1 * 8)) | (78 << (2 * 8)) | (68 << (3 * 8));
        }

        private static class ColourTypes
        {
            /// <summary>
            /// Each pixel is a greyscale sample.
            /// </summary>
            public const byte Greyscale = 0;
            /// <summary>
            /// Each pixel is an RGB triple.
            /// </summary>
            public const byte RGB = 2;
            /// <summary>
            /// Each pixel is a palette index, a PLTE chunk must appear.
            /// </summary>
            public const byte PLTE = 3;
            /// <summary>
            /// Each pixel is a greyscale sample, followed by an alpha sample.
            /// </summary>
            public const byte GreyScaleAlpha = 4;
            /// <summary>
            /// Each pixel is an RGBA quadruple.
            /// </summary>
            public const byte RGBA = 6;
        }
        private static class FilterTypes
        {
            public const byte None = 0;
            public const byte Sub = 1;
            public const byte Up = 2;
            public const byte Average = 3;
            public const byte Paeth = 4;
        }

        private static class ZlibHeader
        {
            /// <summary>
            /// The value representing the Deflate compression type in Little Endian.
            /// </summary>
            public const byte Deflate = 8;
            public const byte CM = 0b00001111;
            public const byte CI = 0b11110000;
        }
        private static class DeflateBlock
        {
            public const byte Last = 0b00000001;
            public const byte Type = 0b00000110;
            public const byte Uncompressed = 0;
            public const byte Fixed = 0b00000001;
            public const byte Dynamic = 0b00000010;
        }
        private static class CompressionLevel
        {
            public const byte Fastest = 0;
            public const byte Fast = 1;
            public const byte Medium = 2;
            public const byte Slow = 3;

        }

        private static class Flags
        {
            public const byte FDICT = 0b00010000;
        }
    }
}